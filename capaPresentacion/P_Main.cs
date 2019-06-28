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
using System.Media;

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
                objEntidad.catEvangelios_yOtros = new string[1]; // se crea de uno para almacenar "Todas" en uno y no dejar los demas NULL 
                objEntidad.catLibro = new string[66];

                objEntidad.difficulty = "Todas";
                objEntidad.catEvangelios_yOtros[0] = "Todas";
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

                objEntidad.enableButtonSound = false;
                objEntidad.enableGameSound = true;
            }

            InitializeComponent();
        }


        HowToPlay howToPlay;
        P_GameSettings GameSettings;
        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_Listener objNegoListener = new N_Listener();
        D_Login login = new D_Login();


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



        private void Main_Load(object sender, EventArgs e)
        {

            //Evita el Buffer lag al cargar la imagen de fondo
            SetDoubleBuffered(tableLayoutPanel6);
            SetDoubleBuffered(tableLayoutPanel7);
            SetDoubleBuffered(tableLayoutPanel8);
            SetDoubleBuffered(tableLayoutPanel9);
            SetDoubleBuffered(tableLayoutPanel10);
            SetDoubleBuffered(tableLayoutPanel11);
            SetDoubleBuffered(tableLayoutPanel15);
            SetDoubleBuffered(tableLayoutPanel16);

            this.BackgroundImage = Properties.Resources.Focused_bible_landing_01_Fondo;
            BackgroundImageLayout = ImageLayout.Stretch;

            if (E_Usuario.Nombreusuario == null || E_Usuario.Nombreusuario == "")
            {
                E_Usuario.Nombreusuario = "Invitado";
            }

            lab_User.Text = "User: " + E_Usuario.Nombreusuario;
            if (lab_User.Text != "User: Invitado")
            {
                btn_Logout_Login.BackgroundImage = Properties.Resources.LogOut;
                btn_Logout_Login.Text = "LOGOUT | REGISTRATE";

                btn_Partida.Enabled = true; // desbloquear modalidad Partida
            }
            else
            {
                btn_Logout_Login.BackgroundImage = Properties.Resources.Login_Registrate;
                btn_Logout_Login.Text = "LOGIN | REGISTRATE";

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

            if (objEntidad.enableButtonSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
                objEntidad.enableButtonSound = true;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
                objEntidad.enableButtonSound = false;
            }

        }

        private void Btn_Settings_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            Btn_Settings.Image = Properties.Resources.Focused_bible_landing_02_MOUSE_ENTER;
        }

        private void Btn_Settings_MouseLeave(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Focused_bible_landing_02;
        }

        private void Btn_Close_Click(object sender, EventArgs e)
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
            if (lab_User.Text == "User: Invitado") // si está deslogueado
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

        private void btn_how2Play_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_how2Play.BackgroundImage = Properties.Resources.Focused_bible_landing_03_MOUSE_ENTER;
        }

        private void btn_how2Play_MouseLeave(object sender, EventArgs e)
        {
            btn_how2Play.BackgroundImage = Properties.Resources.Focused_bible_landing_03_1;
        }

        private void Btn_Close_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            Btn_Close.BackgroundImage = Properties.Resources.Focused_bible_landing_Cerrar_MOUSE_ENTER;
        }

        private void Btn_Close_MouseLeave(object sender, EventArgs e)
        {
            Btn_Close.BackgroundImage = Properties.Resources.Focused_bible_landing_Cerrar;
        }

        private void btn_solo_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_solo.BackgroundImage = Properties.Resources.Focused_bible_landing_05_MOUSE_ENTER;
        }

        private void btn_solo_MouseLeave(object sender, EventArgs e)
        {
            btn_solo.BackgroundImage = Properties.Resources.Focused_bible_landing_05;
        }

        private void btn_debate_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_debate.BackgroundImage = Properties.Resources.Focused_bible_landing_06_MOUSE_ENTER;
        }

        private void btn_debate_MouseLeave(object sender, EventArgs e)
        {
            btn_debate.BackgroundImage = Properties.Resources.Focused_bible_landing_06;
        }

        private void btn_Partida_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Partida.BackgroundImage = Properties.Resources.Focused_bible_landing_07_MOUSE_ENTER;
        }

        private void btn_Partida_MouseLeave(object sender, EventArgs e)
        {
            btn_Partida.BackgroundImage = Properties.Resources.Focused_bible_landing_07;
        }

        private void btn_Logout_Login_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);

            if (lab_User.Text == "User: Invitado")
            {
                btn_Logout_Login.BackgroundImage = Properties.Resources.Login_Registrate_MouseEnter;
            }
            else
            {
                btn_Logout_Login.BackgroundImage = Properties.Resources.LogOut_MouseEnter;
            }
        }

        private void btn_Logout_Login_MouseLeave(object sender, EventArgs e)
        {
            if (lab_User.Text == "User: Invitado")
            {
                btn_Logout_Login.BackgroundImage = Properties.Resources.Login_Registrate;
            }
            else
            {
                btn_Logout_Login.BackgroundImage = Properties.Resources.LogOut;
            }
        }

        private void btn_debate_EnabledChanged(object sender, EventArgs e)
        {
            if (btn_debate.Enabled == false)
            {
                btn_debate.BackgroundImage = Properties.Resources.Focused_bible_landing_06_DISABLED;
            }
            else
            {
                btn_debate.BackgroundImage = Properties.Resources.Focused_bible_landing_06;
            }
        }

        private void btn_Partida_EnabledChanged(object sender, EventArgs e)
        {
            if (btn_Partida.Enabled == false)
            {
                btn_Partida.BackgroundImage = Properties.Resources.Focused_bible_landing_07_DISABLED;
            }
            else
            {
                btn_Partida.BackgroundImage = Properties.Resources.Focused_bible_landing_07;
            }
        }

        private void pbx_Sound_Click(object sender, EventArgs e)
        {
            if (objEntidad.enableButtonSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_OFF;
                objEntidad.enableButtonSound = false;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_ON;
                objEntidad.enableButtonSound = true;
            }
        }

        private void pbx_Sound_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            if (objEntidad.enableButtonSound == true)
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
            if (objEntidad.enableButtonSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }
        }

        private void btn_solo_Click(object sender, EventArgs e)
        {
            this.Hide();
            P_focusedBible_SoloyPartida soloMain = new P_focusedBible_SoloyPartida(objEntidad);
            soloMain.ShowDialog();
        }
    }
}
