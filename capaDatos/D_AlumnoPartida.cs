
using capaEntidad;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace capaDatos
{
    public class D_AlumnoPartida
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);
        E_Alumnos Alumno = new E_Alumnos();

        public DataTable D_listado(E_Alumnos AlumnoPartida)
        {
            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_listarTodo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        public void D_insertar(E_Alumnos Alumno)
        {
            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_Insertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            //llenado de los parametros del procedimiento almacenado
            cmd.Parameters.AddWithValue("@Alumno", Alumno.NombreUsuario);
            cmd.Parameters.AddWithValue("@Estado", Alumno.Estado);
            cmd.Parameters.AddWithValue("@Terminado", Alumno.Terminado);

            if (cn.State == ConnectionState.Open) cn.Close();

            cn.Open();
            cmd.ExecuteNonQuery(); // Ejecutar la consulta 
            cn.Close();
        }


        public int D_Editar(E_Alumnos Alumno)
        {
            object retVal = null;

            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_Actualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            //llenado de los parametros del procedimiento almacenado
            cmd.Parameters.AddWithValue("@Alumno", Alumno.NombreUsuario);
            cmd.Parameters.AddWithValue("@Estado", Alumno.Estado);
            cmd.Parameters.AddWithValue("@Terminado", Alumno.Terminado);

            if (cn.State == ConnectionState.Open) cn.Close();
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



        public void D_EliminarTodo()
        {
            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_EliminarTodos", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (cn.State == ConnectionState.Open) cn.Close();

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }



    }


}
