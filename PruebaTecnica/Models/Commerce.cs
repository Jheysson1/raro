namespace PruebaTecnica.Models
{
    public class Commerce
    {
        public Guid CommerceId { get; set; }  // Clave primaria (UUID)
        public string CommerceName { get; set; }  // Nombre del comercio
        public string Address { get; set; }  // Dirección física del comercio
        public string? RUC { get; set; }  // Registro Único del Contribuyente (opcional)

        // Clave foránea a State
        public Guid StateId { get; set; }
        public State State { get; set; }  // Relación con la entidad State

        // Relación con Users
        public ICollection<User> Users { get; set; }  // Los comercios pueden tener muchos usuarios (empleados, propietarios)

        // Relación con Sales
        public ICollection<Sale> Sales { get; set; }  // Los comercios pueden tener muchas ventas
    }
}
