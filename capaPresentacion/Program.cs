using System;
using System.Windows.Forms;

using capaEntidad;
using capaNegocio;

namespace capaPresentacion
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            P_Splash splash = new P_Splash();
            splash.ShowDialog();

            //
            // Si el login es correcto, procedo con la apertura normal
            // de la aplicacion
            //
            if (splash.DialogResult == DialogResult.OK)
            {
                E_focusedBible objEntidad = new E_focusedBible();
                N_SettingsPROFE objNegoSettingsPROFE = new N_SettingsPROFE();

                P_Main main = new P_Main(objEntidad);
                main.Show();
                Application.Run();

                //borrar todos los settings guardados y los Alumnos de la base de datos, si es el admin
                if (E_Usuario.Rol == "Admin")
                {
                    objNegoSettingsPROFE.N_sp_GameSettingsPROFE_BorrarTodo();
                }
            }

        }
    }
}
