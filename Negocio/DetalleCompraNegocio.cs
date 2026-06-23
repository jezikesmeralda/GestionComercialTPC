using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetalleCompraNegocio
    {
        public List<DetalleCompra> Listar()
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarDetalleCompras");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra aux = new DetalleCompra();

                    aux.Id = (int)datos.Lector["Id"];

                    aux.Producto = new Producto();
                    aux.Producto.Id = (int)datos.Lector["IdProducto"];
                    aux.Producto.NombreProducto = (string)datos.Lector["NombreProducto"];

                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.Subtotal = (decimal)datos.Lector["Subtotal"];

                    lista.Add(aux);

                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
    

