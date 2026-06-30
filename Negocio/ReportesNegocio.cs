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
    
    public List<ReporteCliente> ClientesMasCompraron()
        {
            List<ReporteCliente> lista = new List<ReporteCliente>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("sp_ReporteClientes");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add(new ReporteCliente
                    {
                        Id = (int)datos.Lector["Id"],
                        NombreCliente = (string)datos.Lector["NombreCliente"],
                        CantidadCompras = (int)datos.Lector["CantidadCompras"],
                        MontoTotal = (decimal)datos.Lector["MontoTotal"]
                    });
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }
    
    public List<ReporteVendedor> Vendedores()
        {
            List<ReporteVendedor> lista = new List<ReporteVendedor>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("sp_ReporteVendedores");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add(new ReporteVendedor
                    {
                        Id = (int)datos.Lector["Id"],
                        NombreVendedor = (string)datos.Lector["NombreVendedor"],
                        CantidadVentas = (int)datos.Lector["CantidadVentas"],
                        MontoTotal = (decimal)datos.Lector["MontoTotal"]
                    });
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }

        public List<ReporteStockBajo> StockBajo()
        {
            List<ReporteStockBajo> lista = new List<ReporteStockBajo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("sp_ReporteStockBajo");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add(new ReporteStockBajo
                    {
                        Id = (int)datos.Lector["Id"],
                        NombreProducto = (string)datos.Lector["NombreProducto"],
                        StockActual = (int)datos.Lector["StockActual"],
                        StockMinimo = (int)datos.Lector["StockMinimo"],
                        NombreMarca = (string)datos.Lector["NombreMarca"]
                    });
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.CerrarConexion(); }
        }

    }

}