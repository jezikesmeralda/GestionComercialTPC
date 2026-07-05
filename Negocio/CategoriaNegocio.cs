using System;
using System.Collections.Generic;
using System.Text;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarCategorias");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Activo = (bool)datos.Lector["Activo"];

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

        public void Alta(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new Exception("El nombre de la categoría es obligatorio.");
            if (ExisteCategoria(categoria.Nombre))
                throw new Exception("Ya existe una categoria con ese nombre.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaCategoria");
                datos.SetearParametro("@Nombre", categoria.Nombre);
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

        public void Modificar(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new Exception("El nombre de la categoría es obligatorio.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ModificarCategoria");
                datos.SetearParametro("@Id", categoria.Id);
                datos.SetearParametro("@Nombre", categoria.Nombre);
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
                datos.SetearProcedimiento("sp_BajaCategoria");
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

        public bool ExisteCategoria(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Categorias WHERE Nombre = @Nombre");
                datos.SetearParametro("@Nombre", nombre);

                int cantidad = Convert.ToInt32(datos.EjecutarScalar());

                return cantidad > 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public List<Categoria> ListarInactivos()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarCategoriasInactivas");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Activo = (bool)datos.Lector["Activo"];
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

        public void Reactivar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ReactivarCategoria");
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
    }
}