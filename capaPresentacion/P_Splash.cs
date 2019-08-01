using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using capaEntidad;

namespace capaPresentacion
{
    public partial class P_Splash : Form
    {
        public P_Splash()
        {
            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        SoundPlayer sonido;
        int countDown = 0;

        private void P_Splash_Load(object sender, EventArgs e)
        {
            TerminarSplash.Start();
        }

        private void TerminarSplash_Tick(object sender, EventArgs e)
        {
            if (countDown < 70)
            {
                countDown++;

                if (countDown == 5)
                {
                    reproducirSonidoJuego("heavenIntro.wav", false);
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK; //terminar Splash y abrir Main
            }
        }

        public void reproducirSonidoJuego(string nombreArchivo, bool loop)
        {
            if (sonido != null)
            {
                sonido.Stop();
            }
            //SystemSounds.Hand.Play(); // Sonido de windows
            try
            {
                sonido = new SoundPlayer(System.Windows.Forms.Application.StartupPath + @"\son\" + nombreArchivo);
                if (loop == true)
                {
                    sonido.PlayLooping();
                }
                else
                {
                    sonido.Play();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex);
            }
        }
    }
}
