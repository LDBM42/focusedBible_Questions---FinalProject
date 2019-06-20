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
        public P_Main(E_focusedBible Configuracion)
        {
            if (Configuracion.catEvangelios_yOtros != null)
            {
                objEntidad = Configuracion;
            }
            else // entra aquí si es la primera vez que se entra al Main
            {
                /***INICIALIZANDO TODO***/

                // para asignar tamaño al arreglo si nunca se le ha asignado (para evitar error)
                objEntidad.catEvangelios_yOtros = new string[10];
                objEntidad.catLibro = new string[66];

                objEntidad.difficulty = "Todas";
                // para asignar una consulta al arreglo si nunca se le ha asignado (para tener algo que consultar)
                objEntidad.queryListarPreguntas = "SELECT DISTINCT codPreg, preg, a, b, c, d, resp, pasage from preguntas " +
                                                "INNER JOIN " +
                                                "Categoria ON Categoria.catID = preguntas.catLibro OR Categoria.catID = preguntas.catEvangelios_yOtros " +
                                                "OR Categoria.catID = preguntas.catNuevoAntiguo ";
                objEntidad.numRounds = 1;
                objEntidad.time2Answer = 20;
                objEntidad.opportunities = 2;
                objEntidad.group1 = "Grupo 1";
                objEntidad.group2 = "Grupo 2";

                objEntidad.questions2Answer = "Todas";
                objEntidad.rebound = false;
                objEntidad.opportunitiesBoolean = true;
            }

            InitializeComponent();
        }


        HowToPlay howToPlay;
        P_GameSettings GameSettings;
        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_Listener objNegoListener = new N_Listener();
        D_Login login = new D_Login();



        private void Main_Load(object sender, EventArgs e)
        {
            //Evita el Buffer lag al cargar la imagen de fondo
            SetDoubleBuffered(tableLayoutPanel1);
            SetDoubleBuffered(tableLayoutPanel2);
            SetDoubleBuffered(tableLayoutPanel3);
            SetDoubleBuffered(tableLayoutPanel4);
            SetDoubleBuffered(tableLayoutPanel5);
            SetDoubleBuffered(tableLayoutPanel12);
            SetDoubleBuffered(tableLayoutPanel13);
            SetDoubleBuffered(tableLayoutPanel14);

            this.BackgroundImage = Properties.Resources.Focused_bible_landing_01_Fondo;
            BackgroundImageLayout = ImageLayout.Stretch;


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
            P_Debate_Main debateMain = new P_Debate_Main(objEntidad);
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

            GameSettings = new P_GameSettings(objEntidad);
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
