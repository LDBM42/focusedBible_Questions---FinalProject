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

        public string N_Empezar()
        {
            return objDato.D_Empezar();
        }
    }
}
