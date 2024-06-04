namespace JV_PuntoVenta.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
