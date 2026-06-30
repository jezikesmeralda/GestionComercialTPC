using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ReportesProductoVendido
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public int CantidadVendida { get; set; }
        public decimal MontoTotal { get; set; }
        
    }
}
