using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using capaEntidad;
using capaNegocio;
using capaDatos;

namespace capaPresentacion
{
    public partial class P_SOLO_Marcador : Form
    {
        public P_SOLO_Marcador(E_focusedBible Configuracion)
        {
            this.objEntidad = Configuracion;
            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();

        // Las siguentes dos funciones son para
        //evitar los problemas de Buffer por tener layouts transparentes
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        #endregion
        #region .. code for Flucuring ..

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion


        private void P_Debate_Main_Load(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoJuego("fanfare.wav", true);
            SetDoubleBuffered(tableLayoutPanel7);
            SetDoubleBuffered(tableLayoutPanel19);
            SetDoubleBuffered(tableLayoutPanel21);


            this.BackgroundImage = Properties.Resources.Focused_bible_SOLO_marcador_01;
            BackgroundImageLayout = ImageLayout.Stretch;

            lbl_ganador_debate.Text = objEntidad.winner;
            SetFinalResults();
        }


        void SetFinalResults()
        {
            // puntuacion grupo 1 y 2
            lab_ScoreNum1.Text = objEntidad.finalResultsDUO[0, 0];

            // respuestas incorrectas grupo 1 y 2
            lab_wrongAnswer_1.Text = objEntidad.finalResultsDUO[0, 1];

            // comodines totales grupo 1 y 2
            lab_comodins_1.Text = objEntidad.finalResultsDUO[0, 2];
        }



        private void btn_goToMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_goToMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                this.DialogResult = DialogResult.OK; //cierra el esta ventana y deja vista la ventana Main
            }
        }

        private void btn_goToMain_MouseEnter(object sender, EventArgs e)
        {
            btn_goToMain.BackgroundImage = Properties.Resources.Focused_bible_SOLO_07_MouseEnter;
        }

        private void btn_goToMain_MouseLeave(object sender, EventArgs e)
        {
            btn_goToMain.BackgroundImage = Properties.Resources.Focused_bible_SOLO_07;
        }

        private void btn_reiniciar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; //terminar el banner y se reinicia el juego
        }

        private void btn_reiniciar_MouseEnter(object sender, EventArgs e)
        {
            btn_reiniciar.BackgroundImage = Properties.Resources.Reiniciar_ENTER;
        }

        private void btn_reiniciar_MouseLeave(object sender, EventArgs e)
        {
            btn_reiniciar.BackgroundImage = Properties.Resources.Reiniciar_LEAVE;
        }

        private void pbx_Sound_Click(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                objEntidad.StopGameSound();
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_OFF;
                objEntidad.enableGameSound = false;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_ON;
                objEntidad.enableGameSound = true;
                objEntidad.reproducirSonidoJuego("fanfare.wav", true);
            }
        }

        private void pbx_Sound_MouseEnter(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_OFF;
            }
        }

        private void pbx_Sound_MouseLeave(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }
        }
    }
}
