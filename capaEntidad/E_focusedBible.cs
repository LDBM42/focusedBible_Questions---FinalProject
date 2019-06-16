using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class E_focusedBible
    {
        public int codPreg { get; set; }
        public string preg { get; set; }
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public char resp { get; set; }

        public int numRounds { get; set; }
        public int time2Answer { get; set; }
        public int opportunities { get; set; }
        public string group1 { get; set; }
        public string group2 { get; set; }
        public string pasage { get; set; }
        public string difficulty { get; set; }
        public string [] catEvangelios_yOtros { get; set; }
        public string [] catLibro { get; set; }
        public string catNuevoAntiguo { get; set; }
        public string questions2Answer { get; set; }
        public bool rebound { get; set; } //almacena si se actibo el rebote o no

        // para almacenar el query por dificultad
        public string queryListarPreguntas { get; set; }

        // para saber si se acaba de salir de la ventana settings a la del juego
        public static bool deSettings { get; set; }
    }
}
