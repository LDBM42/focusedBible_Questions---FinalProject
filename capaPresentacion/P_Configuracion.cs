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


        E_focusedBible objEntidad = new E_focusedBible();
        P_Conf_Preguntas_Agregar conf_Preguntas_Agregar;
        P_GameSettings GameSettings;
        P_Usuario usuario;
        SoundPlayer sonido;
        private HowToPlay howToPlay;
        private bool soundsVisible;
        private bool enableGameSound;



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


        private void btn_Conf_Juego_Click(object sender, EventArgs e)
        {
            OpenGameSettings();
        }

        private void OpenGameSettings()
        {
            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_GameSettings").SingleOrDefault<Form>();
            Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();
            Form existe3 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Configuracion").SingleOrDefault<Form>();


            if (existe != null)
            {
                existe.Close();
                existe.Dispose();
                GC.Collect();
            }

            GameSettings = new P_GameSettings(objEntidad);
            existe2.Hide();
            existe3.Hide();
            GameSettings.ShowDialog();
        }


        private void btn_Preguntas_Click(object sender, EventArgs e)
        {
            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Conf_Preguntas_Agregar").SingleOrDefault<Form>();
            Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();
            Form existe3 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Configuracion").SingleOrDefault<Form>();


            if (existe != null)
            {
                existe.Close();
                existe.Dispose();
                GC.Collect();
            }

            conf_Preguntas_Agregar = new P_Conf_Preguntas_Agregar(objEntidad);
            existe2.Hide();
            existe3.Hide();
            conf_Preguntas_Agregar.ShowDialog();
        }

        private void btn_goBack_Click(object sender, EventArgs e)
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

            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(P_DUO_Main))
                {
                    PMain.Hide();
                    frm.Show();
                    frm.BringToFront();
                }
                else if (frm.GetType() == typeof(P_PARTIDA_PROFE_Main))
                {
                    PMain.Hide();
                    frm.Show();
                    frm.BringToFront();
                }
            }

            this.RemoveOwnedForm(PMain); //indica que este va a dejar de ser el papa del form P_Main
            this.Close();
        }



        private void btn_goBack_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_goBack.BackgroundImage = Properties.Resources.goBack_MouseEnter_01_01;
        }

        private void btn_goBack_MouseLeave(object sender, EventArgs e)
        {
            btn_goBack.BackgroundImage = Properties.Resources.goBack_MouseLeave_01;
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
        

        private void pbx_Sound_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            if (objEntidad.enableButtonSound == true || objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_ON_NEUTRO;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_OFF;
            }
        }

        private void pbx_Sound_MouseLeave(object sender, EventArgs e)
        {
            if (objEntidad.enableButtonSound == true || objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON_NEUTRO;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
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


        private void P_Configuracion_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Focused_bible_landing_01_Fondo;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            lab_User.Text = "Usuario: " + E_Usuario.Nombreusuario;


            // cambiar imagen según configuracion de sonido establecida
            if (objEntidad.enableGameSound == true)
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseLeave;
            }
            else
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseLeave_OFF;
            }

            if (objEntidad.enableButtonSound == true)
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseLeave;
            }
            else
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseLeave_OFF;
            }

            if (objEntidad.enableButtonSound == true || objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON_NEUTRO;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }

        }


        //METODOS

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
            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Usuario").SingleOrDefault<Form>();
            Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();
            Form existe3 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Configuracion").SingleOrDefault<Form>();

            if (existe != null)
            {
                existe.Close();
                existe.Dispose();
                GC.Collect();
            }

            usuario = new P_Usuario(true, objEntidad);
            existe2.Hide();
            existe3.Hide();
            usuario.ShowDialog();
        }



        private void btn_Conf_Juego_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Conf_Juego.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_SHADOW_05;
        }

        private void btn_Conf_Juego_MouseLeave(object sender, EventArgs e)
        {
            btn_Conf_Juego.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_05;
        }

        private void btn_Preguntas_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Preguntas.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_SHADOW_06;
        }

        private void btn_Preguntas_MouseLeave(object sender, EventArgs e)
        {
            btn_Preguntas.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_06;
        }

        private void btn_Crear_usuario_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Crear_usuario.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_SHADOW_07;
        }

        private void btn_Crear_usuario_MouseLeave(object sender, EventArgs e)
        {
            btn_Crear_usuario.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_07;
        }

        private void pbx_gameSound_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            if (objEntidad.enableGameSound == true)
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseEnter;
            }
            else
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseEnter_OFF;
            }
        }

        private void pbx_gameSound_MouseLeave(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseLeave;
            }
            else
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseLeave_OFF;
            }
        }

        private void pbx_buttonSound_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            if (objEntidad.enableButtonSound == true)
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseEnter;
            }
            else
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseEnter_OFF;
            }
        }

        private void pbx_buttonSound_MouseLeave(object sender, EventArgs e)
        {
            if (objEntidad.enableButtonSound == true)
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseLeave;
            }
            else
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseLeave_OFF;
            }
        }

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = new HowToPlay();
            howToPlay.ShowDialog();
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
    }
}
