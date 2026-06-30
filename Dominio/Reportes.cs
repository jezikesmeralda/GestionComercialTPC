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

    public class ReporteCliente
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public int CantidadCompras { get; set; }
        public decimal MontoTotal { get; set; }
    }
    public class ReporteVendedor
    {
        public int Id { get; set; }
        public string NombreVendedor { get; set; }
        public int CantidadVentas { get; set; }
        public decimal MontoTotal { get; set; }
    }


    public class ReporteStockBajo
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public string NombreMarca { get; set; }
    }

}
