namespace Dominio
{
    // Eliminamos rol.cs y dejamos asi como indica el profe en el video
    // Son valores que siempre van a ser 2. Vendedor y Administrador
    public enum Rol
    {
        Vendedor,
        Administrador
    }
    public class Usuario
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Rol Rol { get; set; }
        public bool Activo { get; set; }
    }
}
