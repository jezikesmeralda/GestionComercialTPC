using System;
using System.Data.SqlClient;
using System.Data;

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

                conexion = new SqlConnection(@"Server=localhost\SQLEXPRESS01;Database=Comercio;Trusted_Connection=True;TrustServerCertificate=True;");
                conexion.Open();
            }
            catch
            {
                try
                {
                    conexion = new SqlConnection(@"Server=localhost;Database=Comercio;Trusted_Connection=True;TrustServerCertificate=True;");
                    conexion.Open();
                }
                catch
                {
                    try
                    {
                        conexion = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=Comercio;Trusted_Connection=True;TrustServerCertificate=True;");
                        conexion.Open();
                    }
                    catch
                    {
                        try
                        {
                            conexion = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Comercio;Integrated Security=False;User=sa;Password=Passw0rd2025!;TrustServerCertificate=True;");
                            conexion.Open();
                        }
                        catch
                        {
                            conexion = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=Comercio;Integrated Security=True;TrustServerCertificate=True;");
                            conexion.Open();
                        }
                    }
                }
                }

            conexion.Close();
            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.Parameters.Clear();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void SetearProcedimiento(string nombreSP)
        {
            comando.Parameters.Clear();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
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

        public DataRow EjecutarFila()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);
                    return tabla.Rows.Count > 0 ? tabla.Rows[0] : null;
                }
            }
            finally
            {
                conexion.Close();
            }
        }

        public DataSet EjecutarDataSet()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(comando))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}