using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ListarUsuarios");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.UserName = (string)datos.Lector["Nombre"];
                    aux.Password = (string)datos.Lector["Password"];
                    aux.Rol = (Rol)(int)datos.Lector["Rol"];
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

        public void Alta(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.UserName))
                throw new Exception("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new Exception("La contraseña es obligatoria.");

            if (usuario.Password.Length < 4)
                throw new Exception("La contraseña debe tener al menos 4 caracteres.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_AltaUsuario");
                datos.SetearParametro("@Nombre", usuario.UserName);
                datos.SetearParametro("@Password", usuario.Password);
                datos.SetearParametro("@Rol", (int)usuario.Rol);
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

        public void Modificar(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.UserName))
                throw new Exception("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new Exception("La contraseña es obligatoria.");

            if (usuario.Password.Length < 4)
                throw new Exception("La contraseña debe tener al menos 4 caracteres.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("sp_ModificarUsuario");
                datos.SetearParametro("@Id", usuario.Id);
                datos.SetearParametro("@Nombre", usuario.UserName);
                datos.SetearParametro("@Password", usuario.Password);
                datos.SetearParametro("@Rol", (int)usuario.Rol);
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
                datos.SetearProcedimiento("sp_BajaUsuario");
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

        public Usuario ValidarLogin(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Debe ingresar usuario y contraseña.");

            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = null;

            try
            {
                datos.SetearProcedimiento("sp_ValidarLogin");
                datos.SetearParametro("@Nombre", userName);
                datos.SetearParametro("@Password", password);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.UserName = (string)datos.Lector["Nombre"];
                    usuario.Rol = (Rol)(int)datos.Lector["Rol"];
                }

                return usuario;
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