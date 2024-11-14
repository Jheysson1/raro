namespace PruebaTecnica.Models
{
    public class Sale
    {
        public Guid SaleId { get; set; }  // Clave primaria (UUID)
        public DateTime SaleDate { get; set; }  // Fecha de la venta

        // Clave foránea a User (usuario que realiza la venta)
        public Guid UserId { get; set; }
        public User User { get; set; }  // Relación con la entidad User

        // Clave foránea a Commerce (comercio asociado a la venta)
        public Guid CommerceId { get; set; }
        public Commerce Commerce { get; set; }  // Relación con la entidad Commerce
        public required ICollection<SaleDetail> SaleDetails { get; set; }

        // Clave foránea a State (estado de la venta: pendiente, completada, cancelada)
        public Guid StateId { get; set; }
        public State State { get; set; }  // Relación con la entidad State
    }
}
