using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    internal class Venta
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
       public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }

        public List<DetalleVenta> Detalles { get; set; }
    }
}
