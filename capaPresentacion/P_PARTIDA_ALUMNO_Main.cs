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
    public partial class P_PARTIDA_ALUMNO_Main : Form
    {
        public P_PARTIDA_ALUMNO_Main(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;

            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        E_Alumnos objEntidadAlumno = new E_Alumnos();
        N_focusedBible objNego = new N_focusedBible();
        N_AlumnoPartida objNegoAlumno = new N_AlumnoPartida();
        N_SettingsPROFE objNegoSettingsPROFE = new N_SettingsPROFE();
        N_Listener objNegoListener = new N_Listener();
        P_GameSettings GameSettings;
        DataSet ds;
        DataTable dt;
        DataTable dtListar;

        public int countDown = 5;
        public bool lockStart = true;

        public string difficulty;
        public string queryPorDificultad;
        public string[] catEvangelios_yOtros = new string[10];
        public string[] catLibro = new string[66];        
        public string catNuevoAntiguo;
        public int numRounds;
        public int time2Answer;
        private int counter = 0; // contador para saber que tiempo esperar con la palabra "Esperando"


        private void P_Debate_Main_Load(object sender, EventArgs e)
        {
            SetDoubleBuffered(tableLayoutPanel8);
            SetDoubleBuffered(tableLayoutPanel10);
            SetDoubleBuffered(tableLayoutPanel8);
            SetDoubleBuffered(tableLayoutPanel19);
            SetDoubleBuffered(tableLayoutPanel20);
            SetDoubleBuffered(tableLayoutPanel21);

            // Actualizar Estado Jugador en base de datos
            objEntidadAlumno.NombreUsuario = E_Usuario.Nombreusuario;
            objEntidadAlumno.Estado = "False";
            objEntidadAlumno.Terminado = "False";
            // actualiza y en caso de no existir lo inserta
            
            if (objNegoAlumno.N_Editar(objEntidadAlumno, objEntidad) == 0)
            {
                objNegoAlumno.N_Insertar(objEntidadAlumno, objEntidad);
            }

            dtListar = objNegoAlumno.N_listado();

            //para evitar el acceso a la partida si ya se ha iniciado
            if (startStopGame("start"))
            {
                lockStart = true;
                lab_anuncio_1.Text = "SE ACABA DE QUEDAR FUERA DE LA PARTIDA";
                lab_anuncio_2.Text = "SE ACABA DE QUEDAR FUERA DE LA PARTIDA";
                tbx_codigoPartida.Enabled = false;
            }
            else
            {
                lockStart = false;
            }

            this.BackgroundImage = Properties.Resources.Focused_bible_PARTIDA_ALUMNO_01;
            BackgroundImageLayout = ImageLayout.Stretch;


            if (objEntidad.enableButtonSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }
        }

        private bool startStopGame(string comando)
        {
            ds = objNegoListener.N_Listener_Comando(1);
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Comando"].ToString() == comando)
                {
                    return true;
                }
            }

            return false;
        }

        
        private void btn_goToMain_Click(object sender, EventArgs e)
        {
            //optener el id del alumno
            objEntidadAlumno.Id = Convert.ToInt32(dtListar.Rows[0]["Id"]);
            objNegoAlumno.N_EliminarAlumno(objEntidadAlumno); // eliminar alumno de la partida por salir
            

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

        private void tbx_codigoPartida_TextChanged(object sender, EventArgs e)
        {
            if (tbx_codigoPartida.Text.Length == 4)
            {
                ds = objNegoListener.N_Listener_Comando(1);
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["codigoProfe"].ToString() == tbx_codigoPartida.Text)
                    {
                        //empieza a detectar cuando el profesor inicia la partida
                        timer_waitingToStart.Start();

                        // optener los settings del PROFE
                        Get_SettingsFromDatabase();

                        objEntidadAlumno.Estado = "True";
                        objNegoAlumno.N_Editar(objEntidadAlumno, objEntidad);

                        circularProgressBar.ProgressColor = Color.FromArgb(252, 195, 26);
                        LoadBar.Start();
                        
                        tbx_codigoPartida.Enabled = false;
                        cbx_codigo.Checked = true;
                    }
                    else
                    {
                        objEntidadAlumno.Estado = "False";
                        objNegoAlumno.N_Editar(objEntidadAlumno, objEntidad);
                        cbx_codigo.Checked = false;
                    }
                }
            }
        }


        private void Get_SettingsFromDatabase()
        {
            DataSet ds_SettingsPROFE = objNegoSettingsPROFE.N_SettingsPROFE_listarTodo();
            DataTable dt_SettingsPROFE = new DataTable();
            DataSet ds_SettingsTipoLibro = objNegoSettingsPROFE.N_SettingsTipoLibro_listarTodo();
            DataTable dt_SettingsTipoLibro = new DataTable();
            DataSet ds_SettingsLibro = objNegoSettingsPROFE.N_SettingsLibro_listarTodo();
            DataTable dt_SettingsLibro = new DataTable();


            dt_SettingsPROFE = ds_SettingsPROFE.Tables[0];
            dt_SettingsTipoLibro = ds_SettingsTipoLibro.Tables[0];
            dt_SettingsLibro = ds_SettingsLibro.Tables[0];


            if (dt_SettingsPROFE.Rows.Count > 0)
            {
                objEntidad.difficulty = dt_SettingsPROFE.Rows[0]["difficulty"].ToString();
                objEntidad.catNuevoAntiguoChecked = Convert.ToBoolean(dt_SettingsPROFE.Rows[0]["catTestamentoChecked"]);
                if (objEntidad.catNuevoAntiguoChecked == true)
                {
                    objEntidad.catNuevoAntiguo = dt_SettingsPROFE.Rows[0]["catTestamento"].ToString();
                }
                else
                {
                    objEntidad.catNuevoAntiguo = "";
                }

                objEntidad.catEvangelios_yOtrosChecked = Convert.ToBoolean(dt_SettingsPROFE.Rows[0]["catTipoLibroChecked"]);
                objEntidad.catLibroChecked = Convert.ToBoolean(dt_SettingsPROFE.Rows[0]["catLibroChecked"]);
                objEntidad.numRounds = Convert.ToInt32(dt_SettingsPROFE.Rows[0]["numRounds"]);
                objEntidad.time2Answer = Convert.ToInt32(dt_SettingsPROFE.Rows[0]["time2Answer"]);
                objEntidad.rebound = Convert.ToBoolean(dt_SettingsPROFE.Rows[0]["rebound"]);

                GetCategories2Show(); // arma el string con las diferentes categorías a mostrar.



                objEntidad.opportunitiesBoolean = Convert.ToBoolean(dt_SettingsPROFE.Rows[0]["opportunitiesChecked"]);
                objEntidad.opportunities = Convert.ToInt32(dt_SettingsPROFE.Rows[0]["opportunities"]);


                objEntidad.questions2Answer = dt_SettingsPROFE.Rows[0]["questions2Answer"].ToString();

                objEntidad.queryListarPreguntas = dt_SettingsPROFE.Rows[0]["queryListarPreguntas"].ToString();
            }


            if (dt_SettingsTipoLibro.Rows.Count > 0)
            {
                string[] categoria = new string[dt_SettingsTipoLibro.Rows.Count];
                for (int index = 0; index <= dt_SettingsTipoLibro.Rows.Count - 1; index++)
                {
                    categoria[index] = dt_SettingsTipoLibro.Rows[index][0].ToString();
                }

                objEntidad.catEvangelios_yOtros = categoria;
            }


            if (dt_SettingsLibro.Rows.Count > 0)
            {
                string[] categoria = new string[dt_SettingsLibro.Rows.Count];
                for (int index = 0; index <= dt_SettingsLibro.Rows.Count - 1; index++)
                {
                    categoria[index] = dt_SettingsLibro.Rows[index][0].ToString();
                }

                objEntidad.catLibro = categoria;
            }

        }

        private void GetCategories2Show()
        {
            objEntidad.categories2Show = "";

            objEntidad.categories2Show = objEntidad.catNuevoAntiguo;

            if (objEntidad.catEvangelios_yOtrosChecked == true)
            {
                foreach (var category in objEntidad.catEvangelios_yOtros)
                {
                    objEntidad.categories2Show += category + ", ";
                }

            }else if (objEntidad.catLibroChecked == true)
            {
                foreach (var category in objEntidad.catLibro)
                {
                    objEntidad.categories2Show += category + ", ";
                }
            }

            objEntidad.categories2Show = objEntidad.categories2Show.TrimEnd();
            objEntidad.categories2Show = objEntidad.categories2Show.TrimEnd(',');
        }

        private void timer2Start_Tick(object sender, EventArgs e)
        {
            LoadBar.Stop(); // detener label Esperando
            circularProgressBar.Font = new Font(circularProgressBar.Font.Name, 40f, circularProgressBar.Font.Style, circularProgressBar.Font.Unit);

            if (countDown > 0)
            {
                countDown--;
                circularProgressBar.Text = countDown.ToString();
                lab_anuncio.Text = countDown.ToString();
            }
            else
            {
                timer2Start.Stop();


                ds = objNegoListener.N_Listener_Comando(1);
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["codigoProfe"].ToString() == tbx_codigoPartida.Text)
                    {
                        this.Hide();
                        P_focusedBible_SOLO_y_PARTIDA soloMain = new P_focusedBible_SOLO_y_PARTIDA(objEntidad, objEntidadAlumno);
                        soloMain.Show();
                    }
                    else
                    {
                        lab_anuncio.Text = "Se Acaba de quedar Fuera de la PARTIDA";
                        tbx_codigoPartida.Enabled = false;
                    }
                }
            }
        }


        private void LoadBar_Tick(object sender, EventArgs e)
        {
            if (circularProgressBar.Text == "")
            {
                counter = 0;
                circularProgressBar.Text = "r";
            }
            else if (circularProgressBar.Text == "r")
            {
                circularProgressBar.Text = "Ero";
            }
            else if (circularProgressBar.Text == "Ero")
            {
                circularProgressBar.Text = "Esrdo";
            }
            else if (circularProgressBar.Text == "Esrdo")
            {
                circularProgressBar.Text = "Esprndo";
            }
            else if (circularProgressBar.Text == "Esprndo")
            {
                circularProgressBar.Text = "Esperando";
            }
            else if (circularProgressBar.Text == "Esperando")
            {
                circularProgressBar.Text = "Esperando";
                counter++;
                if (counter > 4) // esperar 4 segundos con "Esperando" escrito
                {
                    circularProgressBar.Text = "";
                }
            }
        }

        private void cbx_codigo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_codigo.Checked == true)
            {
                cbx_codigo.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Checked_01;
            }
            else
            {
                cbx_codigo.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Unchecked_01;
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

        private void timer_waitingToStart_Tick(object sender, EventArgs e)
        {
            if (lockStart == false)
            {
                if (startStopGame("start"))
                {
                    // optener los settings del PROFE
                    Get_SettingsFromDatabase();

                    timer2Start.Start();
                    timer_waitingToStart.Stop(); // detiene la deteccion del inicio de seccion
                }
                else if (startStopGame("stop"))
                {
                    btn_goToMain.PerformClick();
                }
            }
        }
    }
}
