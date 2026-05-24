using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    internal class Cliente
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public int Dni { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }

    }
}
