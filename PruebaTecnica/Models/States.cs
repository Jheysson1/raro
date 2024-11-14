namespace PruebaTecnica.Models
{
    public class State
    {
        public Guid StateId { get; set; }  // Clave primaria (UUID)

        public string StateName { get; set; }  // Nombre descriptivo del estado (por ejemplo, "Active", "Inactive", "Pending", "Completed", "Canceled")

        // Relación con Users
        public ICollection<User> Users { get; set; }  // Relación con los usuarios que tienen este estado

        // Relación con Commerces
        public ICollection<Commerce> Commerces { get; set; }  // Relación con los comercios que tienen este estado

        // Relación con Sales
        public ICollection<Sale> Sales { get; set; }  // Relación con las ventas que tienen este estado
    }
}
