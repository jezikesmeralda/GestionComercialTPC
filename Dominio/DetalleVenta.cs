using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
