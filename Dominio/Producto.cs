using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }

        public decimal PrecioCosto{ get; set; }
        public decimal PorcentajeGanancia { get; set; } // para calcular el precio de venta

        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public bool Activo { get; set; }
    }
}
