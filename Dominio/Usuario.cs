namespace Dominio
{
    public class Usuario
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Rol Rol { get; set; }
        public bool Activo { get; set; }
    }
}
