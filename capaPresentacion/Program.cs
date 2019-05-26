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


            P_Login login = new P_Login();
            login.ShowDialog();

            //
            // Si el login es correcto, procedo con la apertura normal
            // de la aplicacion
            //
            if (login.DialogResult == DialogResult.OK)
                Application.Run(new Settings());
        }
    }
}
