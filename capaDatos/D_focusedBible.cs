
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


        public void D_insertar(E_focusedBible preg) //insertar Datos (recibe la clase E_Empleados como parametro)
        {
            SqlCommand cmd = new SqlCommand("sp_insert01", cn);
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

            if (cn.State == ConnectionState.Open) cn.Close();

            cn.Open();
            cmd.ExecuteNonQuery(); // Ejecutar la consulta 
            cn.Close();
        }
        
        public DataTable D_listarCategorias()
        {
            SqlCommand cmd = new SqlCommand("SELECT nombreCat FROM Categorias", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable D_listarLibros()
        {
            SqlCommand cmd = new SqlCommand("SELECT nombreLibro FROM Libros", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        
        public DataTable D_listarCategoriasXTestamento(E_focusedBible preg)
        {
            SqlCommand cmd = new SqlCommand("sp_ListarCategoriasxTestamento", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@testamento", preg.catNuevoAntiguo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable D_listarLibrosXCategoria(string [] catEvangelios_yOtros)
        {
            string[] arregloTemporal = new string[10] {"","","","","","","","","",""};
            for (int index = 0; index < catEvangelios_yOtros.Length; index++)
            {
                arregloTemporal[index] = catEvangelios_yOtros[index];
            }

            SqlCommand cmd = new SqlCommand("sp_ListarLibrosxCategorias", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombreCat1", arregloTemporal[0]);
            cmd.Parameters.AddWithValue("@nombreCat2", arregloTemporal[1]);
            cmd.Parameters.AddWithValue("@nombreCat3", arregloTemporal[2]);
            cmd.Parameters.AddWithValue("@nombreCat4", arregloTemporal[3]);
            cmd.Parameters.AddWithValue("@nombreCat5", arregloTemporal[4]);
            cmd.Parameters.AddWithValue("@nombreCat6", arregloTemporal[5]);
            cmd.Parameters.AddWithValue("@nombreCat7", arregloTemporal[6]);
            cmd.Parameters.AddWithValue("@nombreCat8", arregloTemporal[7]);
            cmd.Parameters.AddWithValue("@nombreCat9", arregloTemporal[8]);
            cmd.Parameters.AddWithValue("@nombreCat10", arregloTemporal[9]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
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


        /*
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
        }*/
    }


}
