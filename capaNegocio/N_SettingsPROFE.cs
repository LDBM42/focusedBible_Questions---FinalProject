
using capaDatos;
using capaEntidad;
using System.Data;

namespace capaNegocio
{
    public class N_SettingsPROFE
    {
        D_SettingsPROFE objDato = new D_SettingsPROFE();
        E_focusedBible objEntidad = new E_focusedBible();


        public void N_InsertarSettingsProfe(E_focusedBible objEntidad)
        {
            objDato.D_InsertarSettingsProfe(objEntidad);
        }

        public void N_InsertarSettingTipoLibro(E_focusedBible objEntidad)
        {
            objDato.D_InsertarSettingTipoLibro(objEntidad);
        }

        public void N_InsertarSettingLibro(E_focusedBible objEntidad)
        {
            objDato.D_InsertarSettingLibro(objEntidad);
        }






        public DataSet N_SettingsPROFE_listarTodo()
        {
            return objDato.D_SettingsPROFE_listarTodo();
        }

        public DataSet N_SettingsTipoLibro_listarTodo()
        {
            return objDato.D_SettingsTipoLibro_listarTodo();
        }

        public DataSet N_SettingsLibro_listarTodo()
        {
            return objDato.D_SettingsLibro_listarTodo();
        }





        public DataSet N_sp_GameSettingsPROFE_BorrarTodo()
        {
            return objDato.D_sp_GameSettingsPROFE_BorrarTodo();
        }
    }
}
