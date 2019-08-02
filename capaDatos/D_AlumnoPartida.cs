
using capaEntidad;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace capaDatos
{
    public class D_AlumnoPartida
    {
        SqlConnection cn = new SqlConnection(E_ConnectionString.conectionString);
        E_Alumnos Alumno = new E_Alumnos();

        public DataTable D_listado()
        {
            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_listarTodo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable D_ListarPuntuacionFinal()
        {
            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_listarPuntuaciónFinal", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        public void D_insertar(E_Alumnos Alumno, E_focusedBible objEntidad)
        {
            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_Insertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            //llenado de los parametros del procedimiento almacenado
            cmd.Parameters.AddWithValue("@Alumno", Alumno.NombreUsuario);
            cmd.Parameters.AddWithValue("@Estado", Alumno.Estado);
            cmd.Parameters.AddWithValue("@Terminado", Alumno.Terminado);
            cmd.Parameters.AddWithValue("@Correctas", Convert.ToInt32(objEntidad.finalResultsSOLO[0]));
            cmd.Parameters.AddWithValue("@Incorrectas", Convert.ToInt32(objEntidad.finalResultsSOLO[1]));
            cmd.Parameters.AddWithValue("@Tiempo", Convert.ToInt32(objEntidad.finalResultsSOLO[2]));
            cmd.Parameters.AddWithValue("@Comodines", Convert.ToInt32(objEntidad.finalResultsSOLO[3]));

            if (cn.State == ConnectionState.Open) cn.Close();

            cn.Open();
            cmd.ExecuteNonQuery(); // Ejecutar la consulta 
            cn.Close();
        }


        public int D_Editar(E_Alumnos Alumno, E_focusedBible objEntidad)
        {
            // evitar que se almacene un valor vacío
            if (objEntidad.finalResultsSOLO[0] == "")
            {
                objEntidad.finalResultsSOLO[0] = "0";
            }
            if (objEntidad.finalResultsSOLO[1] == "")
            {
                objEntidad.finalResultsSOLO[1] = "0";
            }
            if (objEntidad.finalResultsSOLO[2] == "")
            {
                objEntidad.finalResultsSOLO[2] = "0";
            }
            if (objEntidad.finalResultsSOLO[3] == "")
            {
                objEntidad.finalResultsSOLO[3] = "0";
            }

            object retVal = null;

            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_Actualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            //llenado de los parametros del procedimiento almacenado
            cmd.Parameters.AddWithValue("@Alumno", Alumno.NombreUsuario);
            cmd.Parameters.AddWithValue("@Estado", Alumno.Estado);
            cmd.Parameters.AddWithValue("@Terminado", Alumno.Terminado);
            cmd.Parameters.AddWithValue("@Correctas", Convert.ToInt32(objEntidad.finalResultsSOLO[0]));
            cmd.Parameters.AddWithValue("@Incorrectas", Convert.ToInt32(objEntidad.finalResultsSOLO[1]));
            cmd.Parameters.AddWithValue("@Tiempo", Convert.ToInt32(objEntidad.finalResultsSOLO[2]));
            cmd.Parameters.AddWithValue("@Comodines", Convert.ToInt32(objEntidad.finalResultsSOLO[3]));

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

        public DataTable D_EliminarAlumno(E_Alumnos Alumno)
        {
            SqlCommand cmd = new SqlCommand("sp_AlumnoPartida_EliminarAlumno", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Alumno.Id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }


}
