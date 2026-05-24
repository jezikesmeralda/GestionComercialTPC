using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class ProductoProveedor
    {
        public int Id { get; set; }
        public Producto Producto { get; set; }
        public Proveedor Proveedor { get; set; }
        public decimal Precio { get; set; }
    }
}
