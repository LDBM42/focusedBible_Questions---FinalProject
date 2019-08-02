using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace capaDatos
{
    public class D_Login
    {
        SqlConnection cn = new SqlConnection(E_ConnectionString.conectionString);

        //Conexion local a base de datos para guardar el autologin
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conectar"].ConnectionString);


        public DataSet ValidarLogin(string sUsuario, string sPassword)
        {
            SqlCommand cmd = new SqlCommand("sp_Data_FLogin_ValidarLogin", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", sUsuario);  
            cmd.Parameters.AddWithValue("@Password", sPassword);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }

        public DataSet Login(string sUsuario)
        {
            SqlCommand cmd = new SqlCommand("sp_Data_FUsuario_Login", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", sUsuario);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }


        //Detectar si el autologin está activo
        public DataSet AutoLoginGetLocal()
        {
            SqlCommand cmd = new SqlCommand("Select Usuario From Usuario Where Logged=1", Conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);

            string NombreLogeado = "";
            try
            {
                NombreLogeado = dt.Tables[0].Rows[0]["Usuario"].ToString();
            }
            catch (Exception)
            {
                return Login(NombreLogeado);
            }

            return Login(NombreLogeado);
        }


        //Establecer el autologin activo o desactivo
        public int AutoLoginSetLocal(string sUsuario, int sLogged)
        {
            object retVal = null;

            /*Consulta si existe el usuario y en caso de que no, agrega el nombre del usuario a la db Local
            para poder recordar el autologin */
            #region Agregar Usuario Local 
            SqlCommand cmd = new SqlCommand(string.Format("Select Usuario From Usuario Where Usuario='{0}'",
                sUsuario), Conn);

            if (Conn.State == ConnectionState.Open) Conn.Close();
            Conn.Open();
            retVal = cmd.ExecuteScalar();

            if (Convert.ToString(retVal) != sUsuario)
            {

                cmd = new SqlCommand(string.Format("Insert Into Usuario(Usuario) Values('{0}')",
                sUsuario), Conn);
                cmd.ExecuteScalar();
            }
            #endregion

            //modificar el estatus del autologin
            #region Modificar Stado del Autologin
            cmd = new SqlCommand(string.Format("Update Usuario Set Logged={0} Where Usuario='{1}'  Select @@ROWCOUNT as cantidad",
                sLogged, sUsuario), Conn);

            try
            {
                retVal = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != Conn)
                    Conn.Close();
            }
            #endregion


            return Convert.ToInt32(retVal);
        }




        //Detectar si el autologin está activo
        public void GetRemoteCredentials()
        {
            SqlCommand cmd = new SqlCommand("SELECT remoteHostName, remoteUserName, remotePassword FROM Credenciales", Conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);

            try
            {
                E_ConnectionString.remoteHostName = dt.Tables[0].Rows[0]["remoteHostName"].ToString();
                E_ConnectionString.remoteUserName = dt.Tables[0].Rows[0]["remoteUserName"].ToString();
                E_ConnectionString.remotePassword = dt.Tables[0].Rows[0]["remotePassword"].ToString();
            }
            catch (Exception)
            {
            }
        }


        //Agregar credenciales de base de datos externa
        public int SetRemoteCredentials(string hostName, string userName, string password)
        {
            object retVal = null;

            DeleteRemoteCredentials(); // borrar todos los registros antes de insertar otro
            SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO Credenciales VALUES('{0}', '{1}', '{2}')",
                password, userName, hostName), Conn);

            if (Conn.State == ConnectionState.Open) Conn.Close();
            Conn.Open();

            try
            {
                retVal = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != Conn)
                    Conn.Close();
            }

            return Convert.ToInt32(retVal);
        }


        //Borrar Credenciales conexion remota
        public void DeleteRemoteCredentials()
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Credenciales", Conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
        }
    }
}
