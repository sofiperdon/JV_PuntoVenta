using JV_PuntoVenta.Data;
using JV_PuntoVenta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JV_PuntoVenta.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas.ToListAsync();
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.VentaProductos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (venta == null)
            {
                return NotFound();
            }

            foreach (var ventaProducto in venta.VentaProductos)
            {
                ventaProducto.Producto = await _context.Productos.FindAsync(ventaProducto.ProductoId);
            }

            return View(venta);
        }


        // GET: Ventas/Create
        public IActionResult Create()
        {
            var productos = _context.Productos
                .Select(p => new { p.ID, p.Nombre, p.Precio })
                .ToList();

            ViewBag.Productos = new SelectList(productos, "ID", "Nombre");
            ViewBag.ProductosPrecios = productos.ToDictionary(p => p.ID, p => p.Precio);

            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransactionDateTime,Total,VentaProductos")] Venta venta, List<int> ProductoId, List<int> Cantidad)
        {
                _context.Add(venta);
                await _context.SaveChangesAsync();

                for (int i = 0; i < ProductoId.Count; i++)
                {
                    var ventaProducto = new VentaProducto
                    {
                        VentaId = venta.Id,
                        ProductoId = ProductoId[i],
                        Cantidad = Cantidad[i]
                    };
                    _context.Add(ventaProducto);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ID", "Nombre");
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.VentaProductos)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta != null)
            {
                
                _context.VentaProductos.RemoveRange(venta.VentaProductos);

               
                _context.Ventas.Remove(venta);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.Id == id);
        }
    }
}

