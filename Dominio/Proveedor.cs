using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Email { get; set; } // Aclarar cual es el campo obligatorio, email o telefono
        public string? Telefono { get; set; }
        public bool Activo { get; set; }
    }
}
