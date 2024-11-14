namespace PruebaTecnica.Request
{
    public class UserEcoomeDTO
    {
        public string CommerceName { get; set; }  // Nombre del comercio
        public string Address { get; set; }  // Dirección física del comercio
        public string? RUC { get; set; }  // Registro Único del Contribuyente (opcional)
        public string Username { get; set; }  // Nombre de usuario
        public string Password { get; set; }  // Contraseña del usuario
    }
}
