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
        public P_Debate_Main(string[] catEvangelios_yOtros = null, string[] catLibro = null, string catNuevoAntiguo = "",
            int numRounds = 1, int time2Answer = 20, string difficulty = "Todas", string queryPorDificultad = "")
        {
            this.numRounds = numRounds;
            this.time2Answer = time2Answer;
            this.difficulty = difficulty;
            this.queryPorDificultad = queryPorDificultad;
            this.catEvangelios_yOtros = catEvangelios_yOtros;
            this.catLibro = catLibro;
            this.catNuevoAntiguo = catNuevoAntiguo;
            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_Listener objNegoListener = new N_Listener();
        P_GameSettings GameSettings;
        D_Login login = new D_Login();
        P_focusedBible_Debate PfocusedB;
        P_Main PMain;
        string g1_Name;
        string g2_Name;
        public string difficulty; public string queryPorDificultad = "SELECT DISTINCT codPreg, preg, a, b, c, d, resp, pasage from preguntas " +
                                              "INNER JOIN " +
                                              "Categoria ON Categoria.catID = preguntas.catLibro OR Categoria.catID = preguntas.catEvangelios_yOtros " +
                                              "OR Categoria.catID = preguntas.catNuevoAntiguo ";
        public string[] catEvangelios_yOtros = new string[10];
        public string[] catLibro = new string[66];
        public string catNuevoAntiguo;
        public int numRounds;
        public int time2Answer;


        private void btn_IniciarDebate_Click(object sender, EventArgs e)
        {
            Change_Settings();

            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBible_Debate").SingleOrDefault<Form>();

            if (existe != null)
            {
                existe.Close();
            }

            PfocusedB = new P_focusedBible_Debate(catEvangelios_yOtros, catLibro, catNuevoAntiguo, g1_Name, g2_Name, time2Answer, numRounds, difficulty, queryPorDificultad);
            this.Close();
            PfocusedB.Show();
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
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

            if (existe != null) // para saber si el formulario principal existe
            {
                this.AddOwnedForm(existe); //indica que este va a ser el papa del form P_Main
                existe.Close(); // cerrar ventana principal
            }

            P_Main PMain = new P_Main();
            this.AddOwnedForm(PMain); //indica que este va a ser el papa del form P_Main

            PMain.numRounds = numRounds;
            PMain.difficulty = difficulty;
            PMain.queryPorDificultad = queryPorDificultad;
            PMain.catEvangelios_yOtros = catEvangelios_yOtros;
            PMain.catLibro = catLibro;
            PMain.catNuevoAntiguo = catNuevoAntiguo;
            PMain.time2Answer = time2Answer;

            PMain.Show();
            this.RemoveOwnedForm(PMain); //indica que este va a dejar de ser el papa del form P_Main
            this.Close();
        }

        private void tbx_Grupo1_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Grupo1.SelectAll();
        }

        private void tbx_Grupo2_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Grupo2.SelectAll();
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
                Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBible_Debate").SingleOrDefault<Form>();

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

            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBible_Debate").SingleOrDefault<Form>();

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
            Change_Settings();

            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_GameSettings").SingleOrDefault<Form>();
            Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

            if (existe != null)
            {
                existe.Close();
                existe.Dispose();
                GC.Collect();
            }

            GameSettings = new P_GameSettings(catEvangelios_yOtros, catLibro, catNuevoAntiguo, g1_Name, g2_Name, numRounds, time2Answer, difficulty);
            existe2.Hide();
            //this.Hide();
            GameSettings.Show();
        }


        public void Change_Settings()
        {
            g1_Name = tbx_Grupo1.Text;
            g2_Name = tbx_Grupo2.Text;
        }
        
    }
}
