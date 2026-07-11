using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Vendedor { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string MedioPago { get; set; }
        public int Cuotas { get; set; }
        public decimal Interes { get; set; }
        public decimal TotalConInteres { get; set; }
        public List<DetalleVenta> Detalles { get; set; }
    }
}
