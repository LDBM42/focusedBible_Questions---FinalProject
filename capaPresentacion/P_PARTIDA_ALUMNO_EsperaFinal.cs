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
    public partial class P_PARTIDA_ALUMNO_EsperaFinal : Form
    {
        public P_PARTIDA_ALUMNO_EsperaFinal(E_focusedBible Configuracion)
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
        DataSet ds;
        DataTable dt;
        DataTable dtListar;

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
            SetDoubleBuffered(tableLayoutPanel6);
            SetDoubleBuffered(tableLayoutPanel7);
            SetDoubleBuffered(tableLayoutPanel8);
            SetDoubleBuffered(tableLayoutPanel10);
            SetDoubleBuffered(tableLayoutPanel8);
            SetDoubleBuffered(tableLayoutPanel19);
            SetDoubleBuffered(tableLayoutPanel20);
            SetDoubleBuffered(tableLayoutPanel21);


            LoadBar.Start();
            timer_waitingForFinalResults.Start(); // empieza a detectar si todos han terminado de jugar


            // Actualizar Estado Jugador en base de datos
            objEntidadAlumno.NombreUsuario = E_Usuario.Nombreusuario;
            objEntidadAlumno.Estado = "True";
            objEntidadAlumno.Terminado = "True";
            
            // actualiza y en caso de no existir lo inserta
            if (objNegoAlumno.N_Editar(objEntidadAlumno, objEntidad) == 0)
            {
                objNegoAlumno.N_Insertar(objEntidadAlumno, objEntidad);
            }

            dtListar = objNegoAlumno.N_listado();


            this.BackgroundImage = Properties.Resources.Focused_bible_landing_01;
            BackgroundImageLayout = ImageLayout.Stretch;


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

        private bool EndOfGame(string comando)
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


        void GoToMain()
        {
            this.Close();
        }

        private void LoadBar_Tick(object sender, EventArgs e)
        {
            if (circularProgressBar.Text == "r")
            {
                counter = 0;
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
                    circularProgressBar.Text = "r";
                }
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

        private void timer_waitingForFinalResults_Tick(object sender, EventArgs e)
        {
            if (EndOfGame("stop"))
            {
                GoToMain();
                timer_waitingForFinalResults.Stop();
            }
            else if (EndOfGame("End"))
            {
                this.DialogResult = DialogResult.OK;
                timer_waitingForFinalResults.Stop();
            }
        }
    }
}
