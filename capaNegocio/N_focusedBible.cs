
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

        public DataTable N_listadoParaConfiguracion()
        {
            return objDato.D_listadoPreguntasConfiguracion();
        }

        //Este es el Metodo para Eliminar preguntas
        public void N_eliminarPreguntas(int codPreg)
        {
            objDato.D_eliminarPreguntas(codPreg);
        }

        public void N_EditarPreguntas(E_focusedBible preg)
        {
            objDato.D_EditarPreguntas(preg);
        }
        
        public void N_InsertarPreguntas(E_focusedBible preg)
        {
            objDato.D_InsertarPreguntas(preg); //pasamos el objeto de la capa E_focusedBible como parametro al metodo D_insertar.
        }

        public DataTable N_listadoPor_DificultadYCategoria(E_focusedBible preg)
        {
            return objDato.D_listadoPor_DificultadYCategoría(preg);
        }

        public int N_NumFilas_PorDificultadYCategoria(E_focusedBible preg)
        {
            return objDato.D_NumFilas_PorDificultadYCategoria(preg);
        }


    }

}
