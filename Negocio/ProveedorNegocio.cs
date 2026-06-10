using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarProveedores");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Email = datos.Lector["Email"] as string;
                    aux.Telefono = datos.Lector["Telefono"] as string;
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

        public void Alta(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaProveedor");
                datos.SetearParametro("@Nombre", proveedor.Nombre);
                datos.SetearParametro("@Email", (object)proveedor.Email ?? DBNull.Value);
                datos.SetearParametro("@Telefono", (object)proveedor.Telefono ?? DBNull.Value);
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

        public void Modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ModificarProveedor");
                datos.SetearParametro("@Id", proveedor.Id);
                datos.SetearParametro("@Nombre", proveedor.Nombre);
                datos.SetearParametro("@Email", (object)proveedor.Email ?? DBNull.Value);
                datos.SetearParametro("@Telefono", (object)proveedor.Telefono ?? DBNull.Value);
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
                datos.SetearProcedimiento("sp_BajaProveedor");
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