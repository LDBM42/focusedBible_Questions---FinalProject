using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaEntidad;

namespace capaPresentacion
{
    public class EjecutarQuery
    {
        /**********************************************************************************************/
        //Ejecuta la consulta para saber cuantas preguntas y cuales se mostrarán
        public static void ejecutarQuery(string categoria, E_focusedBible configuracion)
        {
            E_focusedBible objEntidad = configuracion;
            P_QueryListarPreguntas PQuery = new P_QueryListarPreguntas();

            int TotalQuestToAnswer;
            /*las pregutas totales son la cantidad de preguntas seleccionadas * cantidad de rondas*/
            if (objEntidad.questions2Answer != "Todas")
            {
                TotalQuestToAnswer = (Convert.ToInt32(objEntidad.questions2Answer) * objEntidad.numRounds);

                if (categoria == "DUO")
                {
                    TotalQuestToAnswer *= 2; // se multiplica por 2 para que salga el total a los dos jugadores
                }
            }
            else
            {
                TotalQuestToAnswer = 0;
            }

            objEntidad.queryListarPreguntas = PQuery.QueryPorCategoriayDificultad(objEntidad,
                                                                                  TotalQuestToAnswer);
        }
        /**********************************************************************************************/

    }
}
