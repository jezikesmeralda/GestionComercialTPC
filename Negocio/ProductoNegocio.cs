using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarProductos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.NombreProducto = (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] as string;
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] as string;
                    aux.PrecioCosto = (decimal)datos.Lector["PrecioCosto"];
                    aux.StockActual = (int)datos.Lector["StockActual"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
