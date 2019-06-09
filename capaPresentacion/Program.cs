using System;
using System.Windows.Forms;

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
                P_Main main = new P_Main();
                main.Show();
                Application.Run();
            }

        }
    }
}
