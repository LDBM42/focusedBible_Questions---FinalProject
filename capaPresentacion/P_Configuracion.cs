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
    public partial class P_Configuracion : Form
    {
        public P_Configuracion(E_focusedBible configuracion)
        {
            objEntidad = configuracion;
            InitializeComponent();
        }


        P_Conf_Preguntas_Agregar conf_Preguntas_Agregar;
        P_GameSettings GameSettings;
        E_focusedBible objEntidad = new E_focusedBible();
        private bool soundsVisible;
        private HowToPlay howToPlay;

        private void btn_Conf_Juego_Click(object sender, EventArgs e)
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


        private void btn_Preguntas_Click(object sender, EventArgs e)
        {
            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Conf_Preguntas_Agregar").SingleOrDefault<Form>();
            Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

            if (existe != null)
            {
                existe.Close();
                existe.Dispose();
                GC.Collect();
            }

            conf_Preguntas_Agregar = new P_Conf_Preguntas_Agregar(objEntidad);
            existe2.Hide();
            conf_Preguntas_Agregar.Show();
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

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = new HowToPlay();
            howToPlay.ShowDialog();
        }

        private void pbx_Sound_Click(object sender, EventArgs e)
        {
            if (soundsVisible)
            {
                pbx_gameSound.Visible = false;
                pbx_buttonSound.Visible = false;

                soundsVisible = false;
            }
            else
            {

                pbx_buttonSound.Visible = true;

                pbx_gameSound.Visible = true;

                soundsVisible = true;
            }
        }

        private void pbx_buttonSound_Click(object sender, EventArgs e)
        {

            if (objEntidad.enableButtonSound == true)
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseEnter_OFF;
                objEntidad.enableButtonSound = false;
            }
            else
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseEnter;
                objEntidad.enableButtonSound = true;
            }
        }

        private void pbx_gameSound_Click(object sender, EventArgs e)
        {

            if (objEntidad.enableGameSound == true)
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseEnter_OFF;
                objEntidad.enableGameSound = false;
            }
            else
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseEnter;
                objEntidad.enableGameSound = true;
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

        private void P_Configuracion_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Focused_bible_landing_01_Fondo;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }


        //METODOS

        SoundPlayer sonido;
        private bool enableGameSound;

        public void reproducirSonidoJuego(string nombreArchivo, bool loop)
        {
            if (enableGameSound)
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

        private void btn_Crear_usuario_Click(object sender, EventArgs e)
        {
            P_Usuario usuario = new P_Usuario(true, objEntidad);
            usuario.Show();

            this.Hide();
        }



        private void btn_Conf_Juego_MouseEnter(object sender, EventArgs e)
        {
            btn_Conf_Juego.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_SHADOW_05;
        }

        private void btn_Conf_Juego_MouseLeave(object sender, EventArgs e)
        {
            btn_Conf_Juego.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_05;
        }

        private void btn_Preguntas_MouseEnter(object sender, EventArgs e)
        {
            btn_Preguntas.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_SHADOW_06;

        }

        private void btn_Preguntas_MouseLeave(object sender, EventArgs e)
        {
            btn_Preguntas.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_06;
        }

        private void btn_Crear_usuario_MouseEnter(object sender, EventArgs e)
        {
            btn_Crear_usuario.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_SHADOW_07;

        }

        private void btn_Crear_usuario_MouseLeave(object sender, EventArgs e)
        {
            btn_Crear_usuario.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_07;
        }
    }
}
