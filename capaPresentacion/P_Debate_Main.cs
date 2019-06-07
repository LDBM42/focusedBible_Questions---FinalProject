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
    public partial class P_Debate_Main : Form
    {
        public P_Debate_Main(int numRounds = 1, int time2Answer = 20, string difficulty = "All")
        {
            this.numRounds = numRounds;
            this.time2Answer = time2Answer;
            this.difficulty = difficulty;
            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_Listener objNegoListener = new N_Listener();
        P_GameSettings GameSettings = new P_GameSettings();
        D_Login login = new D_Login();
        P_focusedBible PfocusedB;
        P_Main PMain;
        HowToPlay howToPlay;
        string g1_Name;
        string g2_Name;
        int Rounds;
        public string difficulty;
        public int numRounds;
        public int time2Answer;


        private void btn_IniciarDebate_Click(object sender, EventArgs e)
        {
            Change_Settings();

            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBibles").SingleOrDefault<Form>();

            if (existe != null)
            {
                existe.Close();
            }

            PfocusedB = new P_focusedBible();
            this.DialogResult = DialogResult.OK;
            PfocusedB.ShowDialog();
        }

        public void Change_Settings()
        {
            g1_Name = tbx_Grupo1.Text;
            g2_Name = tbx_Grupo2.Text;
        }


        private void tbx_Grupo1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_Grupo1.Text == "")
            {
                tbx_Grupo1.Text = "Grupo 1";
                tbx_Grupo1.SelectAll();
            }
        }
        private void tbx_Grupo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                this.DialogResult = DialogResult.OK; //cierra el esta ventana y deja vista la ventana Main
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
                {
                    e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                      //al ser true, evita que apareca la tecla presionada
                    tbx_Grupo2.Focus();
                }
        }


        private void tbx_Grupo2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_Grupo1.Text == "")
            {
                tbx_Grupo1.Text = "Grupo 2";
                tbx_Grupo1.SelectAll();
            }
        }
        private void tbx_Grupo2_KeyPress(object sender, KeyPressEventArgs e)
        {// 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                this.DialogResult = DialogResult.OK; //cierra el esta ventana y deja vista la ventana Main
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
                {
                    e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                      //al ser true, evita que apareca la tecla presionada
                    btn_IniciarDebate.PerformClick();
                }
        }


        private void btn_goToMain_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void tbx_Grupo1_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Grupo1.SelectAll();
        }

        private void tbx_Grupo2_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Grupo2.SelectAll();
        }

        private void P_Debate_Main_Load(object sender, EventArgs e)
        {
            this.AddOwnedForm(GameSettings); //indica que este va a ser el papa del form Gamesettings
        }




        private void btn_goToMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                this.DialogResult = DialogResult.OK; //cierra el esta ventana y deja vista la ventana Main
            }
        }

        private void OpenSettings()
        {

            try
            {   // para saber si el formulario existe, o sea si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Login").SingleOrDefault<Form>();
                Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBibles").SingleOrDefault<Form>();

                if (existe != null)
                {
                    existe.Close();
                }

                if (existe2 != null) // para cerrar el juego, en caso de haberse iniciado
                {
                    existe2.Close();
                }

                P_Login login = new P_Login();
                login.reOpened++;
                this.Hide();
                login.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Algo salió mal, Favor intentarlo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        private void btn_newUser_Click(object sender, EventArgs e)
        {
            P_Usuario usuario = new P_Usuario();
            usuario.Show();

            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBibles").SingleOrDefault<Form>();

            if (existe == null) // para ocultar settings al abrir nuevo usuario, solo en caso de que el juego no se haya iniciado
            {
                this.Hide();
                usuario.TopMost = false;
            }
            else
            {
                usuario.TopMost = true;
                usuario.WindowState = FormWindowState.Maximized;
            }

        }

        private void Btn_SettingsBtn_goToMain_MouseEnter(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Settings_MouseUp;
        }

        private void Btn_SettingsBtn_goToMain_MouseLeave(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Settings;
        }

        private void Btn_Settings_Click(object sender, EventArgs e)
        {
            OpenGameSettings();
        }

        private void OpenGameSettings()
        {
            try
            {   // para saber si el formulario existe, o sea si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_GameSettings").SingleOrDefault<Form>();

                if (existe != null)
                {
                    GameSettings.ShowDialog();
                }
                else
                {
                    GameSettings = new P_GameSettings();
                    GameSettings.ShowDialog();
                }
            }
            catch (Exception)
            {
                GameSettings.ShowDialog();
            }
        }
    }
}
