using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace capaDatos
{
    public class D_Listener
    {
        SqlConnection sqlConnection = new SqlConnection(E_ConnectionString.conectionString);
        SqlCommand sqlCommand;

        public DataSet D_Listener_Comando(int ID) // Empezar el juego en todas las maquinas conectadas u otro comando indicado.
        {
            sqlCommand = new SqlCommand("SELECT Comando, codigoProfe FROM Listener WHERE ID =" + ID, sqlConnection);

            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();

            sqlConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }

        public void D_Listener_Detener_O_Iniciar(int ID, string Comando, string codigoProfe) // Detener inicio en maquinas conectadas u otro comando indicado.
        {
            sqlCommand = new SqlCommand("Update Listener SET Comando = '" + Comando + "', codigoProfe = '" + codigoProfe + "' WHERE ID =" + ID, sqlConnection);

            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
