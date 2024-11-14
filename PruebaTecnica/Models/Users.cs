namespace PruebaTecnica.Models
{
    public class User
    {
        public Guid UserId { get; set; }  // Clave primaria (UUID)
        public string Username { get; set; }  // Nombre de usuario
        public string Password { get; set; }  // Contraseña del usuario
        public string Role { get; set; }  // Rol del usuario, como "Owner" o "Employee"

        // Clave foránea a Commerce
        public Guid CommerceId { get; set; }
        public Commerce Commerce { get; set; }  // Relación con la entidad Commerce

        // Clave foránea a State
        public Guid StateId { get; set; }
        public State State { get; set; }  // Relación con la entidad State

        // Relación con Sales
        public ICollection<Sale> Sales { get; set; }  // Los usuarios pueden tener muchas ventas
    }
}
