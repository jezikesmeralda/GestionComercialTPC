using System;
using System.Collections.Generic;
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
                    aux.NombreProducto = (string)datos.Lector["NombreProducto"];
                    aux.Descripcion = datos.Lector["Descripcion"] as string;
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] as string;
                    aux.PrecioCosto = (decimal)datos.Lector["PrecioCosto"];
                    aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Nombre = (string)datos.Lector["NombreMarca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Nombre = (string)datos.Lector["NombreCategoria"];

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

        public int ContarActivos()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Productos WHERE Activo = 1");
                return Convert.ToInt32(datos.EjecutarScalar());
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

        public void Alta(Producto producto, int idMarca, int idCategoria)
        {
            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
                throw new Exception("El nombre del producto es obligatorio.");

            if (producto.PrecioCosto <= 0)
                throw new Exception("El precio de costo debe ser mayor a cero.");

            if (producto.PorcentajeGanancia < 0)
                throw new Exception("El porcentaje de ganancia no puede ser negativo.");

            if (producto.StockActual < 0 || producto.StockMinimo < 0)
                throw new Exception("El stock no puede ser negativo.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaProducto");
                datos.SetearParametro("@Nombre", producto.NombreProducto);
                datos.SetearParametro("@Descripcion", (object)producto.Descripcion ?? DBNull.Value);
                datos.SetearParametro("@ImagenUrl", (object)producto.ImagenUrl ?? DBNull.Value);
                datos.SetearParametro("@IdMarca", idMarca);
                datos.SetearParametro("@IdCategoria", idCategoria);
                datos.SetearParametro("@PrecioCosto", producto.PrecioCosto);
                datos.SetearParametro("@PorcentajeGanancia", producto.PorcentajeGanancia);
                datos.SetearParametro("@StockActual", producto.StockActual);
                datos.SetearParametro("@StockMinimo", producto.StockMinimo);
                datos.EjecutarAccion();
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

        public void Modificar(Producto producto, int idMarca, int idCategoria)
        {
            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
                throw new Exception("El nombre del producto es obligatorio.");

            if (producto.PrecioCosto <= 0)
                throw new Exception("El precio de costo debe ser mayor a cero.");

            if (producto.PorcentajeGanancia < 0)
                throw new Exception("El porcentaje de ganancia no puede ser negativo.");

            if (producto.StockActual < 0 || producto.StockMinimo < 0)
                throw new Exception("El stock no puede ser negativo.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ModificarProducto");
                datos.SetearParametro("@Id", producto.Id);
                datos.SetearParametro("@Nombre", producto.NombreProducto);
                datos.SetearParametro("@Descripcion", (object)producto.Descripcion ?? DBNull.Value);
                datos.SetearParametro("@ImagenUrl", (object)producto.ImagenUrl ?? DBNull.Value);
                datos.SetearParametro("@IdMarca", idMarca);
                datos.SetearParametro("@IdCategoria", idCategoria);
                datos.SetearParametro("@PrecioCosto", producto.PrecioCosto);
                datos.SetearParametro("@PorcentajeGanancia", producto.PorcentajeGanancia);
                datos.SetearParametro("@StockActual", producto.StockActual);
                datos.SetearParametro("@StockMinimo", producto.StockMinimo);
                datos.EjecutarAccion();
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

        public void Baja(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_BajaProducto");
                datos.SetearParametro("@Id", id);
                datos.EjecutarAccion();
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
<<<<<<< HEAD
        public void ActualizarStock( int idProducto, int cantidad, decimal? nuevoCosto = null)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (nuevoCosto.HasValue)
                {
                    datos.SetearProcedimiento("sp_ActualizarStockCompra");

                    datos.SetearParametro("@IdProducto", idProducto);
                    datos.SetearParametro("@Cantidad", cantidad);
                    datos.SetearParametro("@PrecioCosto", nuevoCosto.Value);
                }
                else
                {
                    datos.SetearConsulta(@" UPDATE Productos SET StockActual = StockActual + @Cantidad WHERE Id = @IdProducto");

                    datos.SetearParametro("@Cantidad", cantidad);
                    datos.SetearParametro("@IdProducto", idProducto);
                }

                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
=======
        
>>>>>>> da514344bcc0508c439e162bcd5b69da2961d28f
        public Producto BusquedaNombre(string nombre)
        {
            Producto producto = null;

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@"SELECT TOP 1 Id, NombreProducto, StockActual, PrecioCosto FROM Productos WHERE NombreProducto LIKE @Nombre");

                datos.SetearParametro("@Nombre", "%" + nombre + "%");

                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    producto = new Producto();

                    producto.Id =
                        (int)datos.Lector["Id"];

                    producto.NombreProducto =
                        (string)datos.Lector["NombreProducto"];

                    producto.StockActual =
                        (int)datos.Lector["StockActual"];

                    producto.PrecioCosto =
                        (decimal)datos.Lector["PrecioCosto"];
                }

                return producto;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        
        
    }
}