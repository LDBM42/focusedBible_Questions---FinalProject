
using capaDatos;
using capaEntidad;
using System.Data;

namespace capaNegocio
{
    public class N_AlumnoPartida
    {
        D_AlumnoPartida objDato = new D_AlumnoPartida();


        public DataTable N_listado()
        {
            return objDato.D_listado();
        }

        public DataTable N_ListarPuntuacionFinal()
        {
            return objDato.D_ListarPuntuacionFinal();
        }

        public void N_Insertar(E_Alumnos AlumnoPartida, E_focusedBible objEntidad)
        {
            objDato.D_insertar(AlumnoPartida, objEntidad);
        }

        public int N_Editar(E_Alumnos AlumnoPartida, E_focusedBible objEntidad)
        {
            return objDato.D_Editar(AlumnoPartida, objEntidad);
        }

        public void N_EliminarTodo()
        {
            objDato.D_EliminarTodo();
        }

        public DataTable N_EliminarAlumno(E_Alumnos AlumnoPartida)
        {
            return objDato.D_EliminarAlumno(AlumnoPartida);
        }
    }

}
