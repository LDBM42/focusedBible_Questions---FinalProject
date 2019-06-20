using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaDatos;

namespace capaNegocio
{
    public class N_Listener
    {
        D_Listener objDato = new D_Listener();

        public string N_Listener_Comando(int ID)
        {
            return objDato.D_Listener_Comando(ID);
        }

        public void N_Listener_Detener_O_Iniciar(int ID, string Comando)
        {
            objDato.D_Listener_Detener_O_Iniciar(1, "");
        }
    }
}
