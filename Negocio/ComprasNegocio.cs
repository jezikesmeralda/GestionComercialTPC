using Dominio;
using System;
using System.Collections.Generic;


namespace Negocio
{
    public class ComprasNegocio
    {
        public List<Compra> Listar()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarCompras");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.FechaCompra = (DateTime)datos.Lector["FechaCompra"];
                    aux.Total = (decimal)datos.Lector["Total"];

                    aux.Proveedor = new Proveedor();
                    aux.Proveedor.Id = (int)datos.Lector["IdProveedor"];
                    aux.Proveedor.Nombre = (string)datos.Lector["Proveedor"];

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
        public void Alta(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaCompra");

                datos.SetearParametro("@IdProveedor", compra.Proveedor.Id);
                datos.SetearParametro("@Total", compra.Total);

                int idCompra =
                    Convert.ToInt32(datos.EjecutarScalar());

                datos.CerrarConexion();
                ProductoNegocio productoNegocio = new ProductoNegocio();

                foreach (DetalleCompra item in compra.Detalles)
                {
                    GuardarDetalle(idCompra, item);
                    productoNegocio.ActualizarStock(item.Producto.Id, item.Cantidad, item.PrecioUnitario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuardarDetalle(int idCompra, DetalleCompra detalle)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaDetalleCompra");

                datos.SetearParametro("@IdCompra", idCompra);
                datos.SetearParametro("@IdProducto", detalle.Producto.Id);
                datos.SetearParametro("@Cantidad", detalle.Cantidad);
                datos.SetearParametro("@PrecioUnitario", detalle.PrecioUnitario);
                datos.SetearParametro("@Subtotal", detalle.Subtotal);

                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


    }
}
    