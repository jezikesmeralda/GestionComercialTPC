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
        public List<DetalleCompra> Detalles { get; set; } // Ya tenemos el detalle compra, saco compra 
                                                          // de DetalleCompra porque estaria "yendo y viniendo".
                                                          //Ambas clases se apuntan entre si con ese campo
    }
}
