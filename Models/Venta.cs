namespace JV_PuntoVenta.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal Total { get; set; }
        public List<VentaProducto> VentaProductos { get; set; }
    }
}
