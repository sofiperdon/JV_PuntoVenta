namespace JV_PuntoVenta.Models
{
    public class Ingreso
    {
        public int Id { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public List<IngresoProducto> IngresoProductos { get; set; }
    }
}
