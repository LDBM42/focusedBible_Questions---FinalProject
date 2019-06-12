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
        public string pasage { get; set; }
        public string dificultad { get; set; }
        public string catEvangelios_yOtros { get; set; }
        public string catLibro { get; set; }
        public string catNuevoAntiguo { get; set; }

        // para saber si se acaba de salir de la ventana settings a la del juego
        public static bool deSettings { get; set; }
    }
}
