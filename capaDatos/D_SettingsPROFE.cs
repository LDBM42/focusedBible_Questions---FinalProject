using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaEntidad;

namespace capaDatos
{
    public class D_SettingsPROFE
    {
        SqlConnection cn = new SqlConnection(E_ConnectionString.conectionString);


        /*INSERT*/

        public void D_InsertarSettingsProfe(E_focusedBible objEntidad)
        {
            SqlCommand cmd = new SqlCommand("sp_SettingsPROFE_Insertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@difficulty", objEntidad.difficulty);
            cmd.Parameters.AddWithValue("@catTestamentoChecked", objEntidad.catNuevoAntiguoChecked);
            cmd.Parameters.AddWithValue("@catLibroChecked", objEntidad.catLibroChecked);
            cmd.Parameters.AddWithValue("@catTipoLibroChecked", objEntidad.catEvangelios_yOtrosChecked);
            cmd.Parameters.AddWithValue("@opportunitiesChecked", objEntidad.opportunitiesBoolean);
            cmd.Parameters.AddWithValue("@rebound", objEntidad.rebound);
            cmd.Parameters.AddWithValue("@numRounds", objEntidad.numRounds);
            cmd.Parameters.AddWithValue("@time2Answer", objEntidad.time2Answer);
            cmd.Parameters.AddWithValue("@opportunities", objEntidad.opportunities);
            cmd.Parameters.AddWithValue("@questions2Answer", objEntidad.questions2Answer);
            cmd.Parameters.AddWithValue("@catTestamento", objEntidad.catNuevoAntiguo);
            cmd.Parameters.AddWithValue("@queryListarPreguntas", objEntidad.queryListarPreguntas);

            if (cn.State == ConnectionState.Open) cn.Close();

            cn.Open();
            cmd.ExecuteNonQuery(); // Ejecutar la consulta 
            cn.Close();
        }

        public void D_InsertarSettingTipoLibro(E_focusedBible objEntidad)
        {
            for (int i = 0; i < objEntidad.catEvangelios_yOtros.Length; i++)
            {
                SqlCommand cmd = new SqlCommand("sp_SettingPROFETipoLibro_Insertar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipoLibro", objEntidad.catEvangelios_yOtros[i]);

                if (cn.State == ConnectionState.Open) cn.Close();

                cn.Open();
                cmd.ExecuteNonQuery(); // Ejecutar la consulta 
            }
                

            cn.Close();
        }


        public void D_InsertarSettingLibro(E_focusedBible objEntidad)
        {
            for (int i = 0; i < objEntidad.catLibro.Length; i++)
            {
                SqlCommand cmd = new SqlCommand("sp_SettingPROFELibro_Insertar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@libro", objEntidad.catLibro[i]);

                if (cn.State == ConnectionState.Open) cn.Close();

                cn.Open();
                cmd.ExecuteNonQuery(); // Ejecutar la consulta 
            }

            cn.Close();
        }




        /*SELECT*/

        public DataSet D_SettingsPROFE_listarTodo()
        {
            SqlCommand cmd = new SqlCommand("sp_SettingsPROFE_listarTodo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }

        public DataSet D_SettingsTipoLibro_listarTodo()
        {
            SqlCommand cmd = new SqlCommand("sp_SettingsTipoLibro_listarTodo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }

        public DataSet D_SettingsLibro_listarTodo()
        {
            SqlCommand cmd = new SqlCommand("sp_SettingsLibro_listarTodo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }



        /*BORRAR*/

        public DataSet D_sp_GameSettingsPROFE_BorrarTodo()
        {
            SqlCommand cmd = new SqlCommand("sp_GameSettingsPROFE_BorrarTodo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }
    }
}
