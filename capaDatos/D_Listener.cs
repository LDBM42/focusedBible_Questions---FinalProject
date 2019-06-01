using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace capaDatos
{
    public class D_Listener : Form
    {
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        string Comando;

        public string D_Empezar() // Empezar el juego en todas las maquinas conectadas
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);
            sqlConnection.Open();
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);
            sqlCommand = new SqlCommand("SELECT Comando FROM Listener WHERE ID = 1", sqlConnection); // "dbo" is required for SqlDependency: http://stackoverflow.com/questions/7946885/sqldependency-notification-immediate-failure-notification-after-executing-quer

            Comando = sqlCommand.ExecuteScalar().ToString();// SQL command must be executed AFTER "SqlDependency sqlDependency = new SqlDependency(sqlCommand)".

            return Comando;
        }

        public void D_TerminarListener() // Empezar el juego en todas las maquinas conectadas
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["sqlconex"].ConnectionString);
            sqlConnection.Close();
        }
            
    }
}
