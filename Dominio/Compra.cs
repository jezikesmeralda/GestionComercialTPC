using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompra> Detalles { get; set; }                                                           
                                                          
    }
}
