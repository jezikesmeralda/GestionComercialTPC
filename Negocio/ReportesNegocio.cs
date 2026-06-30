using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ReportesNegocio
    {
        public List<ReportesProductoVendido> ProductosMasVendidos()
        {
            List<ReportesProductoVendido> lista = new List<ReportesProductoVendido>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("sp_ReporteProductosMasVendidos");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add(new ReportesProductoVendido
                    {
                        Id = (int)datos.Lector["Id"],
                        NombreProducto = (string)datos.Lector["NombreProducto"],
                        CantidadVendida = (int)datos.Lector["CantidadVendida"],
                        MontoTotal = (decimal)datos.Lector["MontoTotal"]
                    });
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }
    }
}
