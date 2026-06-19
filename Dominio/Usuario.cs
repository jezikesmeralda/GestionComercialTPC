namespace Dominio
{
    public enum Rol
    {
        Vendedor,
        Administrador
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Rol Rol { get; set; }
        public bool Activo { get; set; }
    }
}