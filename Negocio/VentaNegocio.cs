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
        public Venta Alta(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaVenta");
                datos.SetearParametro("@IdCliente", venta.Cliente.Id);
                datos.SetearParametro("@IdUsuario", venta.Vendedor.Id);
                datos.SetearParametro("@Total", venta.Total);

                System.Data.DataRow fila = datos.EjecutarFila();
                int idVenta = Convert.ToInt32(fila["Id"]);
                string numeroFactura = fila["NumeroFactura"].ToString();


                ProductoNegocio productoNegocio = new ProductoNegocio();

                foreach (DetalleVenta item in venta.Detalles)
                {
                    GuardarDetalle(idVenta, item);

                    productoNegocio.DescontarStock(item.Producto.Id, item.Cantidad);
                }
                venta.Id = idVenta;
                venta.NumeroFactura = numeroFactura;
                venta.FechaVenta = DateTime.Now;
                return venta;
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

        public Venta ObtenerPorId(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            Venta venta = null;

            try
            {
                datos.SetearProcedimiento("sp_ObtenerVentaPorId");
                datos.SetearParametro("@IdVenta", idVenta);

                System.Data.DataSet ds = datos.EjecutarDataSet(); 

                if (ds.Tables.Count < 2 || ds.Tables[0].Rows.Count == 0)
                    return null;

                System.Data.DataRow cabecera = ds.Tables[0].Rows[0];

                venta = new Venta
                {
                    Id = Convert.ToInt32(cabecera["Id"]),
                    NumeroFactura = cabecera["NumeroFactura"].ToString(),
                    FechaVenta = Convert.ToDateTime(cabecera["FechaVenta"]),
                    Total = Convert.ToDecimal(cabecera["Total"]),
                    Cliente = new Cliente
                    {
                        Id = Convert.ToInt32(cabecera["IdCliente"]),
                        Nombre = cabecera["NombreCliente"].ToString(),
                        Apellido = cabecera["ApellidoCliente"].ToString()
                    },
                    Vendedor = new Usuario
                    {
                        Id = Convert.ToInt32(cabecera["IdUsuario"]),
                        Nombre = cabecera["NombreUsuario"].ToString()
                    },
                    Detalles = new List<DetalleVenta>()
                };

                foreach (System.Data.DataRow fila in ds.Tables[1].Rows)
                {
                    venta.Detalles.Add(new DetalleVenta
                    {
                        Id = Convert.ToInt32(fila["Id"]),
                        Producto = new Producto
                        {
                            Id = Convert.ToInt32(fila["IdProducto"]),
                            NombreProducto = fila["NombreProducto"].ToString()
                        },
                        Cantidad = Convert.ToInt32(fila["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(fila["PrecioUnitario"])
                    });
                }

                return venta;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public string GenerarNumeroFactura(int idVenta)
        {
            return $"FAC-{DateTime.Now.Year}-{idVenta:D6}";
        }
    }
}

    }
}
