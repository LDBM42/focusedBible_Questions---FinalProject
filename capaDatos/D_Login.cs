using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace capaDatos
{
    public class D_Login
    {

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);


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



        public DataSet AutoLoginGet()
        {
            SqlCommand cmd = new SqlCommand("sp_Data_FUsuario_Logged", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }



        public int AutoLoginSet(string sUsuario, string sPassword, int sLogged)
        {
            object retVal = null;
            SqlCommand cmd = new SqlCommand("sp_Data_FUsuario_Logged_Actualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", sUsuario);
            cmd.Parameters.AddWithValue("@Password", sPassword);
            cmd.Parameters.AddWithValue("@Logged", sLogged);
            cn.Open();

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
                if (null != cn)
                    cn.Close();
            }

            return Convert.ToInt32(retVal);
        }
    }
}
