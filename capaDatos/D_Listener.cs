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
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);
        SqlCommand sqlCommand;
        string Comando;

        public string D_Listener_Comando(int ID) // Empezar el juego en todas las maquinas conectadas u otro comando indicado.
        {
            sqlCommand = new SqlCommand("SELECT Comando FROM Listener WHERE ID =" + ID, sqlConnection);

            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();

            sqlConnection.Open();
            Comando = sqlCommand.ExecuteScalar().ToString();
            sqlConnection.Close();

            return Comando;
        }

        public void D_Listener_Detener_O_Iniciar(int ID, string Comando) // Detener inicio en maquinas conectadas u otro comando indicado.
        {
            sqlCommand = new SqlCommand("Update Listener SET Comando = '" + Comando + "' WHERE ID =" + ID, sqlConnection);

            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
