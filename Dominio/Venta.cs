using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Vendedor { get; set; } // La consigna dice "con un perfil vendedor que registra ventas"
                                              // Si no guardamos el vendedor, como sabemos desp quien hizo la venta ?

        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }

        public List<DetalleVenta> Detalles { get; set; } // Aca pasa lo mismo que con Compra, saco de detalleVenta
    }
}
