using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace capaPresentacion
{
    public class P_QueryListarPreguntas
    {

        public string QueryPorCategoriayDificultad(string testamentos, string [] categoria, string difficulty, 
                                                     int LimitOfQuestions, bool opportunities)
        {
            string Query;

            if (LimitOfQuestions != 0) // si LimitOfQuestions no es "todas", limitar la consulta a la cantidad de preguntas por responder
            {
                Query = "SELECT TOP " + LimitOfQuestions + " * FROM PregCategoriaDificultad ";
            }
            else // si LimitOfQuestions es igual a "todas"
            {
                Query = "SELECT * FROM PregCategoriaDificultad ";
            }


            //Crear Query para consultar por categorias en la base de datos
            if (testamentos != "Todas")
            {
                Query += "WHERE nombreLibro IN(";

                foreach (var cat in categoria)
                {
                    Query += string.Format("'{0}',", cat);
                }

                Query = Query.TrimEnd(',');

                Query += ")";


                if (difficulty != "Todas")
                {
                    Query += string.Format(" AND dificultad ='{0}'", difficulty);
                }
            }


            if (testamentos == "Todas" && difficulty != "Todas")
            {
                Query += string.Format("WHERE dificultad ='{0}'", difficulty);
            }


            Query += " ORDER BY NEWID()";

            return Query;
        }
    }
}
