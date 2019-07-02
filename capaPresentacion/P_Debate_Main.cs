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
        public P_Debate_Main(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;

            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_Listener objNegoListener = new N_Listener();
        P_GameSettings GameSettings;
        D_Login login = new D_Login();
        P_focusedBible_Debate PfocusedB;
        HowToPlay howToPlay;
        string g1_Name;
        string g2_Name;
        public string difficulty;
        public string queryPorDificultad;
        public string[] catEvangelios_yOtros = new string[10];
        public string[] catLibro = new string[66];
        public string catNuevoAntiguo;
        public int numRounds;
        public int time2Answer;


        private void btn_IniciarDebate_Click(object sender, EventArgs e)
        {
            int? [] noRepetir_PorDificultadyCategoria = new int?[objNego.N_NumFilas_PorDificultadYCategoria(objEntidad)];

            // solo se puede jugar si hay mas de cero pregunta seleccionada
            if (noRepetir_PorDificultadyCategoria.Count() == 0)
            {
                MessageBox.Show("No existen preguntas que cumplan las categorias y dificultad seleccionadas, Favor Elegir otra opción.", "LO SENTIMOS!");
            }
            else
            {
                Change_Settings();

                // para saber si el formulario existe, o sea, si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBible_Debate").SingleOrDefault<Form>();

                if (existe != null)
                {
                    existe.Close();
                }

                PfocusedB = new P_focusedBible_Debate(objEntidad);
                this.Close();
                PfocusedB.Show();
            }
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

            P_Main PMain = new P_Main(objEntidad);
            this.AddOwnedForm(PMain); //indica que este va a ser el papa del form P_Main

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
            objEntidad.reproducirSonidoBoton("button.wav", false);
            Btn_Settings.Image = Properties.Resources.Focused_bible_landing_02_MOUSE_ENTER;
        }

        private void Btn_SettingsBtn_goToMain_MouseLeave(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Focused_bible_landing_02;
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

            GameSettings = new P_GameSettings(objEntidad);
            existe2.Hide();
            //this.Hide();
            GameSettings.Show();
        }


        public void Change_Settings()
        {
            objEntidad.group1 = tbx_Grupo1.Text;
            objEntidad.group2 = tbx_Grupo2.Text;
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


        private void P_Debate_Main_Load(object sender, EventArgs e)
        {
            SetDoubleBuffered(tableLayoutPanel6);
            SetDoubleBuffered(tableLayoutPanel7);
            SetDoubleBuffered(tableLayoutPanel8);
            SetDoubleBuffered(tableLayoutPanel10);
            SetDoubleBuffered(tableLayoutPanel11);
            SetDoubleBuffered(tableLayoutPanel13);
            SetDoubleBuffered(tableLayoutPanel15);
            SetDoubleBuffered(tableLayoutPanel8);
            SetDoubleBuffered(tableLayoutPanel19);
            SetDoubleBuffered(tableLayoutPanel20);
            SetDoubleBuffered(tableLayoutPanel21);


            this.BackgroundImage = Properties.Resources.Fondo_Debate_Main_ConTextBox;
            BackgroundImageLayout = ImageLayout.Stretch;

            tbx_Grupo1.Text = objEntidad.group1;
            tbx_Grupo2.Text = objEntidad.group2;

        }

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = new HowToPlay();
            howToPlay.ShowDialog();
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

        private void btn_how2Play_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_how2Play.BackgroundImage = Properties.Resources.Focused_bible_landing_03_MOUSE_ENTER;
        }

        private void btn_how2Play_MouseLeave(object sender, EventArgs e)
        {
            btn_how2Play.BackgroundImage = Properties.Resources.Focused_bible_landing_03_1;
        }

        private void btn_IniciarDebate_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_IniciarDebate.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
        }

        private void btn_IniciarDebate_MouseLeave(object sender, EventArgs e)
        {
            btn_IniciarDebate.BackgroundImage = Properties.Resources.Boton_Empezar_MouseLeave;
        }
    }
}
