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
        N_AlumnoPartida objNegoAlumno = new N_AlumnoPartida();
        N_Listener objNegoListener = new N_Listener();
        P_GameSettings GameSettings;


        public string difficulty;
        public string queryPorDificultad;
        public string[] catEvangelios_yOtros = new string[10];
        public string[] catLibro = new string[66];
        public string catNuevoAntiguo;
        public int numRounds;
        public int time2Answer;



        private void P_Debate_Main_Load(object sender, EventArgs e)
        {
            SetDoubleBuffered(tableLayoutPanel7);
            SetDoubleBuffered(tableLayoutPanel11);
            SetDoubleBuffered(tableLayoutPanel15);
            SetDoubleBuffered(tableLayoutPanel20);
            SetDoubleBuffered(tableLayoutPanel21);

            objNegoAlumno.N_EliminarTodo(); // borrar todos los alumnos de la lista

            listarAlumnos();
            timer_ActualizarEstadoLista.Start(); // iniciar actualizar lista cada 2 segundos
            
            //this.BackgroundImage = Properties.Resources.Focused_bible_landing_01_Fondo;
            //BackgroundImageLayout = ImageLayout.Stretch;

            tbx_codigoPartida.Text = generarCodigoPartida();

            // enviar codigo a base de datos
            objNegoListener.N_Listener_Detener_O_Iniciar(1, "", tbx_codigoPartida.Text);
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




        private void btn_goToMain_Click(object sender, EventArgs e)
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


        private void btn_goToMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                this.DialogResult = DialogResult.OK; //cierra el esta ventana y deja vista la ventana Main
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



        private void btn_goToMain_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_goToMain.BackgroundImage = Properties.Resources.Focused_bible_SOLO_07_MouseEnter;
        }

        private void btn_goToMain_MouseLeave(object sender, EventArgs e)
        {
            btn_goToMain.BackgroundImage = Properties.Resources.Focused_bible_SOLO_07;
        }

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
            //detener la partida
            objNegoListener.N_Listener_Detener_O_Iniciar(1, "stop", "");
            objNegoAlumno.N_EliminarTodo(); // borrar todos los alumnos de la lista
        }

        private void P_PARTIDA_PROFE_Main_Activated(object sender, EventArgs e)
        {
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
    }
}
