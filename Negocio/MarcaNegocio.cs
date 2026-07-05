using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarMarcas");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();

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

        public void Alta(Marca marca)
        {
            if (string.IsNullOrWhiteSpace(marca.Nombre))
                throw new Exception("El nombre de la marca es obligatorio.");
            if (ExisteMarca(marca.Nombre))
                throw new Exception("Ya existe una marca con ese nombre.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaMarca");
                datos.SetearParametro("@Nombre", marca.Nombre);
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

        public void Modificar(Marca marca)
        {
            if (string.IsNullOrWhiteSpace(marca.Nombre))
                throw new Exception("El nombre de la marca es obligatorio.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ModificarMarca");
                datos.SetearParametro("@Id", marca.Id);
                datos.SetearParametro("@Nombre", marca.Nombre);
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
                datos.SetearProcedimiento("sp_BajaMarca");
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
        public bool ExisteMarca(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Marcas WHERE Nombre = @Nombre");
                datos.SetearParametro("@Nombre", nombre);

                int cantidad = Convert.ToInt32(datos.EjecutarScalar());

                return cantidad > 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public List<Marca> ListarInactivos()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarMarcasInactivas");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
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
                datos.SetearProcedimiento("sp_ReactivarMarca");
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