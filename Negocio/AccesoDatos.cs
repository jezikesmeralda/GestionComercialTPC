using Microsoft.Data.SqlClient;
using System.Data;
using Dominio;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            try
            {
                conexion = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=Comercio;Trusted_Connection=True;TrustServerCertificate=True;");
                conexion.Open();
            }
            catch
            {
                conexion = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Comercio;Integrated Security=False;User=sa;Password=Passw0rd2025!;TrustServerCertificate=True;");
                conexion.Open();
            }

            conexion.Close();
            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public object EjecutarScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}