using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace TestMvcApplication2.Models
{
    
    class ConexionBD
    {
        private SqlConnection conexion = null;
        //private List<Usuario> listaUsuarios = new List<Usuario>();

        #region Metodos de inicializacion

        // constructor
        public ConexionBD()
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

        // conecto a la base de datos
        public void Conectar()
        {
            try
            {
                string datosConexion = "Data Source=192.168.1.3; Initial Catalog=test1; Integrated Security=false; UID=erikp; PWD=Wx123";
                //string datosConexion = "Data Source=(local)\\SQLEXPRESS; Initial Catalog=test1; Integrated Security=true";
                conexion.ConnectionString = datosConexion;
                conexion.Open();
            }
            catch (SqlException) 
            {
                Console.WriteLine("No se ha podido establecer conexion con la BD.");
            }
            
        }

        // desconecto de la base de datos
        public void Desconectar()
        {
            conexion.Close();
        }
        
        #endregion

        #region Metodos de consultas

        public List<Usuario> EjecutarConsultaDetalle()
        {
            string textoConsulta = "SELECT Usuarios.id_usuario, Usuarios.dni, Usuarios.nombre, Usuarios.apellido, Usuarios.fecha_nacimiento, Usuarios.domicilio, Localidades.nombre as localidad "
                                    + " FROM Usuarios INNER JOIN Localidades"
                                    + " ON Usuarios.localidad = Localidades.id_localidad ;";
            
            SqlCommand consulta = new SqlCommand(textoConsulta, conexion);
            SqlDataReader resultado = consulta.ExecuteReader();
            List<Usuario> listaResultado = new List<Usuario>();

            while (resultado.Read())
            {
                Usuario usuarioLeido = new Usuario();
                usuarioLeido.IdUsuario = resultado.GetInt32(0);
                usuarioLeido.Dni = resultado.GetInt32(1);
                usuarioLeido.Nombre = resultado.GetString(2);
                usuarioLeido.Apellido = resultado.GetString(3);
                usuarioLeido.FechaNacimiento = resultado.GetDateTime(4);
                usuarioLeido.Domicilio = resultado.GetString(5);
                usuarioLeido.Localidad = resultado.GetString(6);
                listaResultado.Add(usuarioLeido);
            }

            return listaResultado;
        }

        public Usuario EjecutarConsultaDetalleUsuario(int idUsuario)
        {
            Usuario usuarioRes = new Usuario();
            
            string textoConsultaTelefonos = "SELECT telefono, tipo FROM Telefonos WHERE id_usuario=" + idUsuario;
            SqlCommand consulta = new SqlCommand(textoConsultaTelefonos, conexion);
            SqlDataReader resultadoTelefonos = consulta.ExecuteReader();

            while (resultadoTelefonos.Read())
            {
                usuarioRes.AgregarTelefono(new Telefono(resultadoTelefonos.GetString(0), resultadoTelefonos.GetString(1)));
            }
            resultadoTelefonos.Dispose();

            string textoConsultaMails = "SELECT mail, tipo FROM Mails WHERE id_usuario=" + idUsuario;
            consulta.CommandText = textoConsultaMails;
            SqlDataReader resultadoMails = consulta.ExecuteReader();

            while (resultadoMails.Read())
            {
                usuarioRes.AgregarMail(new Mail(resultadoMails.GetString(0), resultadoMails.GetString(1)));
            }
            resultadoMails.Dispose();

            return usuarioRes;
        }

        public void EjecutarUpdate(Usuario usuarioUpdate)
        {
            string textoTransaccion = "DECLARE @ultimo_usuario INT " +
                                        "BEGIN TRAN " +
                                        "BEGIN TRY ";

            textoTransaccion += "INSERT INTO Usuarios VALUES ("
                + usuarioUpdate.Dni + ",'"
                + usuarioUpdate.Nombre + "','"
                + usuarioUpdate.Apellido + "','"
                + usuarioUpdate.FechaNacimiento.ToString() + "','"
                + usuarioUpdate.Domicilio + "',"
                + usuarioUpdate.Localidad + ",null);";
            textoTransaccion += "SET @ultimo_usuario = (SELECT MAX(id_usuario) FROM Usuarios) ";
            
            for (int i = 0; i < 3; i++ )
            {
                if ((usuarioUpdate.ListaTelefonos[i].NumeroTel != "") && (usuarioUpdate.ListaTelefonos[i].TipoTel != ""))
                {
                    textoTransaccion += "INSERT INTO Telefonos VALUES (@ultimo_usuario, '" + 
                        usuarioUpdate.ListaTelefonos[i].NumeroTel + "','" +
                        usuarioUpdate.ListaTelefonos[i].TipoTel + "') ";
                }
                if ((usuarioUpdate.ListaMails[i].DireccionMail != "") && (usuarioUpdate.ListaMails[i].TipoMail != ""))
                {
                    textoTransaccion += "INSERT INTO Mails VALUES (@ultimo_usuario, '" +
                        usuarioUpdate.ListaMails[i].DireccionMail + "','" +
                        usuarioUpdate.ListaMails[i].TipoMail + "') ";
                }
            }

            textoTransaccion += "COMMIT " +
                                "END TRY " +
                                "BEGIN CATCH " +
                                "   ROLLBACK TRAN " +
                                "END CATCH ";
            
            SqlCommand consulta = new SqlCommand(textoTransaccion, conexion);
            consulta.ExecuteNonQuery();
        }

        public void EjecutarDelete(int id)
        {
            string textoDelete = "DELETE FROM Usuarios WHERE id_usuario=" + id;
            SqlCommand delete = new SqlCommand(textoDelete, conexion);
            delete.ExecuteNonQuery();
        }

        public Usuario EjecutarInfoUsuario(int id)
        {
            Usuario usuarioRes = new Usuario();

            usuarioRes = EjecutarConsultaDetalleUsuario(id);

            string textoConsulta = "SELECT Usuarios.id_usuario, Usuarios.dni, Usuarios.nombre, Usuarios.apellido, Usuarios.fecha_nacimiento, Usuarios.domicilio, Localidades.nombre as localidad "
                                    + " FROM Usuarios INNER JOIN Localidades"
                                    + " ON Usuarios.localidad = Localidades.id_localidad"
                                    + " AND Usuarios.id_usuario = " + id;
            SqlCommand consulta = new SqlCommand(textoConsulta, conexion);
            SqlDataReader resultado = consulta.ExecuteReader();

            while (resultado.Read())
            {
                usuarioRes.IdUsuario = resultado.GetInt32(0);
                usuarioRes.Dni = resultado.GetInt32(1);
                usuarioRes.Nombre = resultado.GetString(2);
                usuarioRes.Apellido = resultado.GetString(3);
                usuarioRes.FechaNacimiento = resultado.GetDateTime(4);
                usuarioRes.Domicilio = resultado.GetString(5);
                usuarioRes.Localidad = resultado.GetString(6);
            }
            return usuarioRes;
        }

        #endregion

        #region partes comentadas
        
        #endregion
    }
}
