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
    public partial class P_Debate_Ganador : Form
    {
        public P_Debate_Ganador(E_focusedBible Configuracion)
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


            this.BackgroundImage = Properties.Resources.Focused_bible_DUO_WINNER_Background_01;
            BackgroundImageLayout = ImageLayout.Stretch;

            lbl_ganador_debate.Text = objEntidad.winner;
            SetFinalResults();
        }


        void SetFinalResults()
        {
            lab_Group1.Text = objEntidad.group1;
            lab_Group2.Text = objEntidad.group2;

            // puntuacion grupo 1
            lab_ScoreNum1.Text = objEntidad.finalResults[0, 0];
            lab_ScoreNum2.Text = objEntidad.finalResults[1, 0];

            // comodin pasage grupo 1
            lab_Passage_1.Text = objEntidad.finalResults[0, 1];
            lab_Passage_2.Text = objEntidad.finalResults[1, 1];

            // comodin 50% grupo 1
            lab_comodin50_1.Text = objEntidad.finalResults[0, 2];
            lab_comodin50_2.Text = objEntidad.finalResults[1, 2];
        }

        private void btn_goToMain_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

            if (existe != null) // para saber si el formulario principal existe
            {
                this.AddOwnedForm(existe); //indica que este va a ser el papa del form P_Main
                existe.Close(); // cerrar ventana principal
            }

            P_Main PMain = new P_Main(objEntidad);
            this.AddOwnedForm(PMain); //indica que este va a ser el papa del form P_Main

            PMain.Show();
            this.RemoveOwnedForm(PMain); //indica que este va a dejar de ser el papa del form P_Main
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
            objEntidad.reproducirSonidoBoton("button.wav", false);
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
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_reiniciar.BackgroundImage = Properties.Resources.Reiniciar_ENTER;
        }

        private void btn_reiniciar_MouseLeave(object sender, EventArgs e)
        {
            btn_reiniciar.BackgroundImage = Properties.Resources.Reiniciar_LEAVE;
        }
    }
}
