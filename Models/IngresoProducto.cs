namespace JV_PuntoVenta.Models
{
    public class IngresoProducto
    {
        public int Id { get; set; }
        public int IngresoId { get; set; }
        public Ingreso Ingreso { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
