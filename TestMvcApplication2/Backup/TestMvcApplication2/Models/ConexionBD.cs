using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace TestMvcApplication2
{

    public class Datos
    {
        public Int32 dni = new Int32();
        public string nombre;
        public string apellido;
        public DateTime fecha_nacimiento;
        public string domicilio;
        public string localidad;
    }

    
    class ConexionBDSingleton
    {
        private SqlConnection conexion = null;

        #region Metodos de inicializacion

        public ConexionBDSingleton()
        {
            conexion = new SqlConnection();
        }

        public void Conectar(string host, string bd, string user, string pwd)
        {
            string datosConexion = "Data Source=" + host + ";" + "Initial Catalog=" 
                + bd + ";Integrated Security=false; UID=" + user + ";" + "PWD=" + pwd;
            conexion.ConnectionString = datosConexion;
            conexion.Open();
        }

        public void Conectar()
        {
            try
            {
                //string datosConexion = "Data Source=192.168.1.3; Initial Catalog=test1; Integrated Security=false; UID=erikp; PWD=Wx123";
                string datosConexion = "Data Source=(local)\\SQLEXPRESS; Initial Catalog=test1; Integrated Security=true";
                conexion.ConnectionString = datosConexion;
                conexion.Open();
            }
            catch (SqlException ex) 
            {
                Console.WriteLine("No se ha podido establecer conexion con la BD.");
            }
            
        }

        public void Desconectar()
        {
            conexion.Close();
        }
        #endregion

        #region Metodos de consultas

        public List<Datos> EjecutarConsultaIndex(string textoConsulta)
        {
            SqlCommand consulta = new SqlCommand(textoConsulta, conexion);
            SqlDataReader resultado = consulta.ExecuteReader();
            List<Datos> listaResultado = new List<Datos>();

            while (resultado.Read())
            {
                Datos lectura = new Datos();
                lectura.nombre = resultado.GetString(0);
                listaResultado.Add(lectura);
            }

            return listaResultado;
        }

        public List<Datos> EjecutarConsultaDetalle(string textoConsulta)
        {
            SqlCommand consulta = new SqlCommand(textoConsulta, conexion);
            SqlDataReader resultado = consulta.ExecuteReader();
            List<Datos> listaResultado = new List<Datos>();

            while (resultado.Read())
            {
                Datos lectura = new Datos();
                lectura.dni = resultado.GetInt32(0);
                lectura.nombre = resultado.GetString(1);
                lectura.apellido = resultado.GetString(2);
                lectura.fecha_nacimiento = resultado.GetDateTime(3);
                lectura.domicilio = resultado.GetString(4);
                lectura.localidad = resultado.GetString(5);
                listaResultado.Add(lectura);
            }

            return listaResultado;
        }

        public void EjecutarUpdate(string textoConsulta)
        {
            SqlCommand consulta = new SqlCommand(textoConsulta, conexion);
            consulta.ExecuteNonQuery();
        }

        #endregion

        #region partes comentadas
        /*     
        private List<Datos> armarListaIndex(SqlDataReader resultado) 
        {
            List<Datos> listaADevolver = new List<Datos>();
            while (resultado.Read())
            {
                Datos lectura = new Datos();
                lectura.nombre = resultado.GetString(0);
                listaADevolver.Add(lectura);
            }

            return listaADevolver;
        }

        private List<Datos> armarListaDetalle(SqlDataReader resultado)
        {
            List<Datos> listaADevolver = new List<Datos>();
            while (resultado.Read())
            {
                Datos lectura = new Datos();
                lectura.dni = resultado.GetInt64(0);
                //Ver la conversion del tipo de SQL a un int de C#
                lectura.nombre = resultado.GetString(1);
                lectura.apellido = resultado.GetString(2);
                lectura.fecha_nacimiento = resultado.GetDateTime(3);
                lectura.domicilio = resultado.GetString(4);
                lectura.localidad = resultado.GetString(5);
                listaADevolver.Add(lectura);
            }

            return listaADevolver;
        }
*/
        #endregion
    }
}
