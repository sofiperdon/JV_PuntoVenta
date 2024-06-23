using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JV_PuntoVenta.Data;
using JV_PuntoVenta.Models;
using Microsoft.AspNetCore.Authorization;

namespace JV_PuntoVenta.Controllers
{
    public class IngresosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngresosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingresos
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingreso.ToListAsync());
        }

        // GET: Ingresos/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingreso
                .Include(v => v.IngresoProductos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ingreso == null)
            {
                return NotFound();
            }

            foreach (var ingresoProducto in ingreso.IngresoProductos)
            {
                ingresoProducto.Producto = await _context.Productos.FindAsync(ingresoProducto.ProductoId);
            }

            return View(ingreso);
        }

        // GET: Ingresos/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var productos = _context.Productos
                .Select(p => new { p.ID, p.Nombre, p.Precio })
                .ToList();

            ViewBag.Productos = new SelectList(productos, "ID", "Nombre");
            ViewBag.ProductosPrecios = productos.ToDictionary(p => p.ID, p => p.Precio);
            return View();
        }

        // POST: Ingresos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,TransactionDateTime,IngresoProductos")] Ingreso Ingreso, List<int> ProductoId, List<int> Cantidad)
        {
            _context.Add(Ingreso);
            await _context.SaveChangesAsync();

            for (int i = 0; i < ProductoId.Count; i++)
            {
                var ingresoProducto = new IngresoProducto
                {                
                    IngresoId = Ingreso.Id,
                    ProductoId = ProductoId[i],
                    Cantidad = Cantidad[i]
                };
                _context.Add(ingresoProducto);
            }
            foreach (var item in Ingreso.IngresoProductos)
            {
                var producto = await _context.Productos.FindAsync(item.ProductoId);
                if (producto != null)
                {
                    producto.Stock += item.Cantidad;

                    if (producto.Stock < 0) { producto.Stock = 0; }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Ingresos/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingreso.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }
            return View(ingreso);
        }

        // POST: Ingresos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransactionDateTime")] Ingreso ingreso)
        {
            if (id != ingreso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoExists(ingreso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingreso);
        }

        // GET: Ingresos/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingreso
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingreso == null)
            {
                return NotFound();
            }

            return View(ingreso);
        }

        // POST: Ingresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingreso = await _context.Ingreso.FindAsync(id);
            if (ingreso != null)
            {
                _context.Ingreso.Remove(ingreso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngresoExists(int id)
        {
            return _context.Ingreso.Any(e => e.Id == id);
        }
    }
}
