
using capaDatos;
using capaEntidad;
using System.Data;

namespace capaNegocio
{
    public class N_AlumnoPartida
    {
        D_AlumnoPartida objDato = new D_AlumnoPartida();
        E_Alumnos AlumnoPartida = new E_Alumnos();


        public DataTable N_listado()
        {
            return objDato.D_listado();
        }
        
        public void N_Insertar(E_Alumnos AlumnoPartida)
        {
            objDato.D_insertar(AlumnoPartida);
        }

        public int N_Editar(E_Alumnos AlumnoPartida)
        {
            return objDato.D_Editar(AlumnoPartida);
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
