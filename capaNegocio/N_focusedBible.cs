
using capaDatos;
using capaEntidad;
using System.Data;

namespace capaNegocio
{
    public class N_focusedBible
    {
        D_focusedBible objDato = new D_focusedBible();
        E_focusedBible preg = new E_focusedBible();

        public DataTable N_listado(E_focusedBible preg)
        {
            return objDato.D_listado(preg);
        }

        public DataTable N_listadoPor_Dificultad(E_focusedBible preg)
        {
            return objDato.D_listadoPor_Dificultad(preg);
        }

        public void N_Insertar(E_focusedBible preg)
        {
            objDato.D_insertar(preg); //pasamos el objeto de la capa E_focusedBible como parametro al metodo D_insertar.
        }

        public int N_NumFilas()
        {
            return objDato.D_NumFilas();
        }

        public int N_NumFilas_PorDificultad(E_focusedBible preg)
        {
            return objDato.D_NumFilas_PorDificultad(preg);
        }


    }

}
