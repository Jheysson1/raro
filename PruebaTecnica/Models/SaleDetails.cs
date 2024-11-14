namespace PruebaTecnica.Models
{
    public class SaleDetail
    {
        public Guid DetailId { get; set; }  // Clave primaria (UUID)

        // Clave foránea a Sale (venta correspondiente)
        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }  // Relación con la entidad Sale

        public string Product { get; set; }  // Descripción del producto vendido
        public int Quantity { get; set; }  // Cantidad del producto vendido
        public decimal Price { get; set; }  // Precio del producto en la venta registrada
    }
}
