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
        public decimal PrecioCompra { get; set; }//precio de referencia que el proveedor le da al producto, no es el precio de venta, es el precio de compra
    }
}
