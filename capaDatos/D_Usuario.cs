using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace capaDatos
{
    public class D_Usuario
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);

        public int ExistUser(string sUsuario)
        {
            object retVal = null;
            SqlCommand cmd = new SqlCommand("sp_Data_FUsuario_ExistUser", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", sUsuario);
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

        public DataTable UsuarioGeneroYTipo(string sUsuario)
        {
            SqlCommand cmd = new SqlCommand("sp_Usuario_Genero_yTipo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", sUsuario);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        public int Insertar(string sUsuario, string sPassword, string sTipo, string sGenero)
        {
            object retVal = null;
            SqlCommand cmd = new SqlCommand("sp_Data_FUsuario_Insertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", sUsuario);
            cmd.Parameters.AddWithValue("@Password", sPassword);
            cmd.Parameters.AddWithValue("@Tipo", sTipo);
            cmd.Parameters.AddWithValue("@Genero", sGenero);
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

        public int Actualizar(string sUsuario, string sPassword, string sTipo, string sGenero)
        {
            object retVal = null;
            SqlCommand cmd = new SqlCommand("sp_Data_FUsuario_Actualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Usuario", sUsuario);
            cmd.Parameters.AddWithValue("@Password", sPassword);
            cmd.Parameters.AddWithValue("@Tipo", sTipo);
            cmd.Parameters.AddWithValue("@Genero", sGenero);
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



        //public static int Eliminar(RUsuario Rusuario)
        //{
        //    SqlParameter[] dbParams = new SqlParameter[]
        //        {
        //            FDBHelper.MakeParam("@Id",SqlDbType.Int, 0, Rusuario.Id),
        //        };
        //    return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FUsuario_Eliminar", dbParams));
        //}
    }
}
