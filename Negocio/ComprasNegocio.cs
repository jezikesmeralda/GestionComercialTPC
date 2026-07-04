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

        public Compra ObtenerPorId(int idCompra)
        {
            AccesoDatos datos = new AccesoDatos();
            Compra compra = null;

            try
            {
                datos.SetearProcedimiento("sp_ObtenerCompraPorId");
                datos.SetearParametro("@IdCompra", idCompra);

                System.Data.DataSet ds = datos.EjecutarDataSet();

                if (ds.Tables.Count < 2 || ds.Tables[0].Rows.Count == 0)
                    return null;

                System.Data.DataRow cabecera = ds.Tables[0].Rows[0];

                compra = new Compra
                {
                    Id = Convert.ToInt32(cabecera["Id"]),
                    FechaCompra = Convert.ToDateTime(cabecera["FechaCompra"]),
                    Total = Convert.ToDecimal(cabecera["Total"]),
                    Proveedor = new Proveedor
                    {
                        Id = Convert.ToInt32(cabecera["IdProveedor"]),
                        Nombre = cabecera["NombreProveedor"].ToString()
                    },
                    Detalles = new List<DetalleCompra>()
                };

                foreach (System.Data.DataRow fila in ds.Tables[1].Rows)
                {
                    compra.Detalles.Add(new DetalleCompra
                    {
                        Id = Convert.ToInt32(fila["Id"]),
                        Producto = new Producto
                        {
                            Id = Convert.ToInt32(fila["IdProducto"]),
                            NombreProducto = fila["NombreProducto"].ToString()
                        },
                        Cantidad = Convert.ToInt32(fila["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(fila["PrecioUnitario"]),
                        Subtotal = Convert.ToDecimal(fila["Subtotal"])
                    });
                }

                return compra;
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

                int idCompra = Convert.ToInt32(datos.EjecutarScalar());

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