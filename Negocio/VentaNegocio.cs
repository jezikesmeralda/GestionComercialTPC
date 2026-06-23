using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VentaNegocio
    {
        public void Alta(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaVenta");
                datos.SetearParametro("@IdCliente", venta.Cliente.Id);
                datos.SetearParametro("@IdUsuario", venta.Vendedor.Id);
                datos.SetearParametro("@Total", venta.Total);

                int idVenta = Convert.ToInt32(datos.EjecutarScalar());

                datos.CerrarConexion();

                ProductoNegocio productoNegocio = new ProductoNegocio();

                foreach (DetalleVenta item in venta.Detalles)
                {
                    GuardarDetalle(idVenta, item);

                    productoNegocio.DescontarStock(item.Producto.Id, item.Cantidad);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuardarDetalle(int idVenta, DetalleVenta detalle)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaDetalleVenta");
                datos.SetearParametro("@IdVenta", idVenta);
                datos.SetearParametro("@IdProducto", detalle.Producto.Id);
                datos.SetearParametro("@Cantidad", detalle.Cantidad);
                datos.SetearParametro("@PrecioUnitario", detalle.PrecioUnitario);
                datos.SetearParametro("@Subtotal",detalle.Subtotal);

                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
