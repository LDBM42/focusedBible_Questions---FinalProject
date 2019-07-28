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
    public partial class P_PARTIDA_PROFE_Main : Form
    {
        public P_PARTIDA_PROFE_Main(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;

            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        E_Alumnos objEntidadAlumno = new E_Alumnos();
        N_focusedBible objNego = new N_focusedBible();
        N_SettingsPROFE objNegoSettingsPROFE = new N_SettingsPROFE();
        N_AlumnoPartida objNegoAlumno = new N_AlumnoPartida();
        N_Listener objNegoListener = new N_Listener();
        P_PARTIDA_Ganador FinalScorePARTIDA;
        P_GameSettings GameSettings;
        P_Configuracion SettingsAdmin;


        public string difficulty;
        public string queryPorDificultad;
        public string[] catEvangelios_yOtros = new string[10];
        public string[] catLibro = new string[66];
        public string catNuevoAntiguo;
        public int countFinishedStudent = 0;
        public bool AfterFinish = false;
        public int numRounds;


        private void P_Debate_Main_Load(object sender, EventArgs e)
        {
            SetDoubleBuffered(tableLayoutPanel11);
            SetDoubleBuffered(tableLayoutPanel15);
            SetDoubleBuffered(tableLayoutPanel20);
            SetDoubleBuffered(tableLayoutPanel21);


            this.BackgroundImage = Properties.Resources.Focused_bible_PARTIDA_PROFESOR_01;
            BackgroundImageLayout = ImageLayout.Stretch;


            objNegoAlumno.N_EliminarTodo(); // borrar todos los alumnos de la lista
            objNegoSettingsPROFE.N_sp_GameSettingsPROFE_BorrarTodo(); // borrar todos los settings de la base de datos

            listarAlumnos();
            timer_ActualizarEstadoLista.Start(); // iniciar actualizar lista cada 2 segundos
            timer_waitingEverybodyToFinish.Start(); //detecta si todos los alumnos han terminado de jugar

            //this.BackgroundImage = Properties.Resources.Focused_bible_landing_01_Fondo;
            //BackgroundImageLayout = ImageLayout.Stretch;

            tbx_codigoPartida.Text = generarCodigoPartida();

            // enviar codigo a base de datos
            objNegoListener.N_Listener_Detener_O_Iniciar(1, "", tbx_codigoPartida.Text);


            //actualizar imagen sonido
            if (objEntidad.enableButtonSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }
        }


        private void listarAlumnos()
        {

            DataTable dt = objNegoAlumno.N_listado();
            //DataSource permite vaciar un DataTable en un dataGridView
            dgvAlumnos.DataSource = dt;
        }


        private string generarCodigoPartida()
        {
            int randomValor;
            string randomValueFormated;

            Random random = new Random();

            randomValor = random.Next(1, 999);
            randomValueFormated = Convert.ToString(randomValor);

            switch (randomValueFormated.Length)
            {
                case 1:
                    randomValueFormated = "000" + randomValueFormated;
                    break;
                case 2:
                    randomValueFormated = "00" + randomValueFormated;
                    break;
                case 3:
                    randomValueFormated = "0" + randomValueFormated;
                    break;
            }

            return randomValueFormated;

        }



        private void btn_IniciarDebate_Click(object sender, EventArgs e)
        {
            objNegoListener.N_Listener_Detener_O_Iniciar(1, "start", tbx_codigoPartida.Text);
        }



        private void goToMain()
        {
            //detener la partida
            objNegoListener.N_Listener_Detener_O_Iniciar(1, "stop", "");
            objNegoAlumno.N_EliminarTodo(); // borrar todos los alumnos de la lista


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

        private void Btn_Settings_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            Btn_Settings.Image = Properties.Resources.Focused_bible_landing_02_MOUSE_ENTER;
        }

        private void Btn_Settings_MouseLeave(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Focused_bible_landing_02;
        }

        private void Btn_Settings_Click(object sender, EventArgs e)
        {
            if (E_Usuario.Rol == "Admin")
            {
                OpenSettingsAdmin();
            }
            else
            {
                OpenGameSettings();
            }
        }


        private void OpenSettingsAdmin()
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Configuracion").SingleOrDefault<Form>();

            if (existe != null)
            {
                existe.Close();
                existe.Dispose();
                GC.Collect();
            }

            SettingsAdmin = new P_Configuracion(objEntidad);
            this.Hide();
            SettingsAdmin.Show();
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
            GameSettings.ShowDialog();
        }


        public void Change_Settings()
        {
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



        private void btn_IniciarPartida_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_IniciarDebate.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
        }

        private void btn_IniciarPartida_MouseLeave(object sender, EventArgs e)
        {
            btn_IniciarDebate.BackgroundImage = Properties.Resources.Boton_Empezar_MouseLeave;
        }



        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            goToMain();
        }

        void BannerFinalScorePARTIDA()
        {
            objNegoListener.N_Listener_Detener_O_Iniciar(1, "End", "");

            Thread.Sleep(1000);
            objEntidad.reproducirSonidoJuego("finalSuccess.wav", false);
            Thread.Sleep(1000);
            objEntidad.StopGameSound();
            objEntidad.winner = E_Usuario.Nombreusuario;
            FinalScorePARTIDA = new P_PARTIDA_Ganador(objEntidad);
            FinalScorePARTIDA.ShowDialog();


            objEntidad.StopGameSound();
            goToMain();
        }


        private void dgvAlumnos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvAlumnos.Rows[e.RowIndex].Cells["Terminado"].Value.ToString() == "True")
                dgvAlumnos.Rows[e.RowIndex].Cells["Finish"].Value = Properties.Resources._status_connected;
            else
                dgvAlumnos.Rows[e.RowIndex].Cells["Finish"].Value = Properties.Resources.status_offline;

            if (dgvAlumnos.Rows[e.RowIndex].Cells["Estado"].Value.ToString() == "True")
                dgvAlumnos.Rows[e.RowIndex].Cells["Connected"].Value = Properties.Resources._status_connected;
            else
                dgvAlumnos.Rows[e.RowIndex].Cells["Connected"].Value = Properties.Resources.status_offline;
        }

        private void timer_ActualizarEstadoLista_Tick(object sender, EventArgs e)
        {
            listarAlumnos();
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

        private void btn_cancelar_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_cancelar.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
        }

        private void btn_cancelar_MouseLeave(object sender, EventArgs e)
        {
            btn_cancelar.BackgroundImage = Properties.Resources.Boton_Empezar_MouseLeave;
        }

        private void btn_cancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                this.DialogResult = DialogResult.OK; //cierra el esta ventana y deja vista la ventana Main
            }
        }

        private void timer_waitingEverybodyToFinish_Tick(object sender, EventArgs e)
        {
            if (AfterFinish == false)
            {
                for (int i = 0; i < dgvAlumnos.Rows.Count; i++)
                {
                    if (dgvAlumnos.Rows[i].Cells["Terminado"].Value.ToString() == "True")
                    {
                        countFinishedStudent++;
                    }
                }

                if (countFinishedStudent == dgvAlumnos.Rows.Count && countFinishedStudent > 0)
                {
                    BannerFinalScorePARTIDA();
                    timer_waitingEverybodyToFinish.Stop(); // detener timer
                    AfterFinish = true;

                }
            }

            countFinishedStudent = 0;
        }
    }
}
