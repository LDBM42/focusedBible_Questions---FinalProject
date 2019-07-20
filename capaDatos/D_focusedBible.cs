
using capaEntidad;
using capaDatos;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace capaDatos
{
    public class D_focusedBible
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);
        E_focusedBible preg = new E_focusedBible();

        public DataTable D_listado(E_focusedBible preg)
        {
            SqlCommand cmd = new SqlCommand("sp_listar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codPreg", preg.codPreg);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable D_listadoPreguntasConfiguracion() //este es el metodo para listar las preguntas en el dgv de configuracion
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PregCategoriaDificultad ", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void D_EditarPreguntas(E_focusedBible preg)
        {
            SqlCommand cmd = new SqlCommand("sp_editar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codPreg", preg.codPreg);
            cmd.Parameters.AddWithValue("@preg", preg.preg);
            cmd.Parameters.AddWithValue("@a", preg.a);
            cmd.Parameters.AddWithValue("@b", preg.b);
            cmd.Parameters.AddWithValue("@c", preg.c);
            cmd.Parameters.AddWithValue("@d", preg.d);
            cmd.Parameters.AddWithValue("@resp", preg.resp);
            cmd.Parameters.AddWithValue("@pasage", preg.pasage);
            cmd.Parameters.AddWithValue("@dificultad", preg.difficulty);
            cmd.Parameters.AddWithValue("@Libro", preg.Libros);

            if (cn.State == ConnectionState.Open) cn.Close();

            cn.Open();
            cmd.ExecuteNonQuery(); // Ejecutar la consulta 
            cn.Close();
        }

        public void D_eliminarPreguntas(int codPreg) //int codPreg
        {
            SqlCommand cmd = new SqlCommand("sp_eliminar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codPreg", codPreg);
            if (cn.State == ConnectionState.Open) cn.Close();
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

               
        public void D_InsertarPreguntas(E_focusedBible preg) //insertar Datos (recibe la clase E_Empleados como parametro)
        {
            SqlCommand cmd = new SqlCommand("sp_insert", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            //llenado de los parametros del procedimiento almacenado (estos se colocan con "@" 
            //y se llenan con el valor ingresado despues de la coma.  "@preg", preg.a
            cmd.Parameters.AddWithValue("@preg", preg.preg);
            cmd.Parameters.AddWithValue("@a", preg.a);
            cmd.Parameters.AddWithValue("@b", preg.b);
            cmd.Parameters.AddWithValue("@c", preg.c);
            cmd.Parameters.AddWithValue("@d", preg.d);
            cmd.Parameters.AddWithValue("@resp", preg.resp);
            cmd.Parameters.AddWithValue("@pasage", preg.pasage);
            cmd.Parameters.AddWithValue("@dificultad", preg.difficulty);
            cmd.Parameters.AddWithValue("@Libro", preg.Libros);

            if (cn.State == ConnectionState.Open) cn.Close();

            cn.Open();
            cmd.ExecuteNonQuery(); // Ejecutar la consulta 
            cn.Close();
        }
        
        public int D_NumFilas_PorDificultadYCategoria(E_focusedBible preg)
        {
            SqlCommand cmd = new SqlCommand(preg.queryListarPreguntas, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count;//Devuelve el total de filas
        }

        public DataTable D_listadoPor_DificultadYCategoría(E_focusedBible preg)
        {
            SqlCommand cmd = new SqlCommand(preg.queryListarPreguntas, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }


}
