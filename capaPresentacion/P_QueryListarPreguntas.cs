using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using capaEntidad;


namespace capaPresentacion
{
    public class P_QueryListarPreguntas
    {

        public string QueryPorCategoriayDificultad(E_focusedBible objEntidad, int LimitOfQuestions)
        {
            string Query;

            if (LimitOfQuestions != 0) // si LimitOfQuestions no es "Todos", limitar la consulta a la cantidad de preguntas por responder
            {
                Query = "SELECT TOP " + LimitOfQuestions + " * FROM PregCategoriaDificultad ";
            }
            else // si LimitOfQuestions es igual a "Todos"
            {
                Query = "SELECT * FROM PregCategoriaDificultad ";
            }


            //Crear Query para consultar por categorias en la base de datos
            if (objEntidad.catNuevoAntiguo != "Todos")
            {
                if (objEntidad.catNuevoAntiguoChecked == true)
                {
                    Query += "WHERE idTestamento IN(";

                    if (objEntidad.catNuevoAntiguo == "Antiguo Testamento")
                    {
                        Query += "1,";
                    }
                    else if (objEntidad.catNuevoAntiguo == "Nuevo Testamento")
                    {
                        Query += "2,";
                    }
                }
                else if (objEntidad.catEvangelios_yOtrosChecked == true)
                {
                    Query += "WHERE nombreCat IN(";

                    foreach (var cat in objEntidad.catEvangelios_yOtros)
                    {
                        Query += string.Format("'{0}',", cat);
                    }
                }
                else if (objEntidad.catLibroChecked == true)
                {
                    Query += "WHERE nombreLibro IN(";

                    foreach (var cat in objEntidad.catLibro)
                    {
                        Query += string.Format("'{0}',", cat);
                    }
                }

                Query = Query.TrimEnd(',');

                Query += ")";



                if (objEntidad.difficulty != "Todos")
                {
                    Query += string.Format(" AND dificultad ='{0}'", objEntidad.difficulty);
                }
            }


            if (objEntidad.difficulty == "Todos" && objEntidad.difficulty != "Todos")
            {
                Query += string.Format("WHERE dificultad ='{0}'", objEntidad.difficulty);
            }


            Query += " ORDER BY NEWID()";

            return Query;
        }
    }
}
