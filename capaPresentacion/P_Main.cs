using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using capaEntidad;
using capaNegocio;
using capaDatos;

namespace capaPresentacion
{
    public partial class P_Main : Form
    {
        public P_Main()
        {
            InitializeComponent();
        }


        HowToPlay howToPlay;
        P_GameSettings GameSettings = new P_GameSettings();
        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_Listener objNegoListener = new N_Listener();
        D_Login login = new D_Login();
        public string difficulty = "Todas";
        public string queryPorDificultad = "SELECT DISTINCT codPreg, preg, a, b, c, d, resp, pasage from preguntas " +
                                            "INNER JOIN " +
                                            "Categoria ON Categoria.catID = preguntas.catLibro OR Categoria.catID = preguntas.catEvangelios_yOtros " +
                                            "OR Categoria.catID = preguntas.catNuevoAntiguo ";
        public string[] catEvangelios_yOtros = new string[10];
        public string[] catLibro = new string[66];
        public string catNuevoAntiguo = "";
        public int numRounds = 1;
        public int time2Answer = 20;


        private void Main_Load(object sender, EventArgs e)
        {

            lab_User.Text = "User: " + E_Usuario.Nombreusuario;
            if (lab_User.Text != "User: ")
            {
                btn_Logout_Login.BackColor = Color.DimGray;
                btn_Logout_Login.Text = "     Logout";

                btn_Partida.Enabled = true; // desbloquear modalidad Partida
            }
            else
            {
                btn_Logout_Login.BackColor = Color.FromArgb(255, 228, 161, 24);
                btn_Logout_Login.Text = "      Login";

                btn_Partida.Enabled = false; // bloquear modalidad Partida
            }

            // Privilegios Administrador
            if (lab_User.Text == "User: Admin")
            {
                btn_debate.Enabled = true; // desbloquear modalidad debate
            }
            else
            {
                btn_debate.Enabled = false; // bloquear modalidad debate
            }

        }

        private void Btn_Settings_MouseEnter(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Settings_MouseUp;
        }

        private void Btn_Settings_MouseLeave(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Settings;
        }

        private void btn_newUser_Click(object sender, EventArgs e)
        {
            DialogResult salir;

            salir = MessageBox.Show("Seguro que desea salir?", "Advertencia", MessageBoxButtons.YesNo);


            if (salir == DialogResult.Yes)
            {
                Application.Exit(); // se debe cerrar de esta forma ya que no se inicio todo por Aplication.Run()
            }
        }

        private void btn_debate_Click(object sender, EventArgs e)
        {
            this.Hide();
            P_Debate_Main debateMain = new P_Debate_Main(catEvangelios_yOtros, catLibro, catNuevoAntiguo, numRounds, time2Answer, difficulty, queryPorDificultad);
            debateMain.ShowDialog();
        }

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = new HowToPlay();
            howToPlay.ShowDialog();
        }

        private void Btn_Settings_Click(object sender, EventArgs e)
        {
            OpenGameSettings();
        }

        
        private void OpenGameSettings()
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_GameSettings").SingleOrDefault<Form>();

            if (existe != null)
            {
                existe.Close();
                existe.Dispose();
                GC.Collect();
            }

            GameSettings = new P_GameSettings(catEvangelios_yOtros, catLibro, catNuevoAntiguo, "Grupo 1", "Grupo 2", numRounds, time2Answer, difficulty);
            GameSettings.Show();
        }

        private void btn_Logout_Login_Click(object sender, EventArgs e)
        {
            if (lab_User.Text == "User: ") // si está deslogueado
            {
                P_Login login = new P_Login();
                login.Show();

                this.Hide();
            }
            else // si está logueado
            {
                if (MessageBox.Show("¿Estás seguro de cerrar sección " +
                E_Usuario.Nombreusuario + "?", "Cerrar Sección",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    E_Usuario.Logged = 0; // para desactivar autologgin
                                          //DESLOGEARSE
                    if (!(login.AutoLoginSetLocal(E_Usuario.Nombreusuario, E_Usuario.Logged) == 1))
                    {
                        MessageBox.Show("No se pudo hacer el cerrado de sección", "Cerado Sección");
                    }
                    else
                    {
                        E_Usuario.Nombreusuario = ""; // resetea el nombre de usuario a VACIO
                        OpenSettings();
                    }
                }
            }
        }

        private void OpenSettings()
        {
            try
            {   // para saber si el formulario existe, o sea si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Login").SingleOrDefault<Form>();

                if (existe != null)
                {
                    existe.Close();
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
    }
}
