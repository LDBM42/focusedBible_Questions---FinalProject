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

            // Actualizar Estado Jugador en base de datos
            objEntidadAlumno.NombreUsuario = E_Usuario.Nombreusuario;
            objEntidadAlumno.Estado = "False";
            objEntidadAlumno.Terminado = "False";
            // actualiza y en caso de no existir lo inserta
            
            if (objNegoAlumno.N_Editar(objEntidadAlumno) == 0)
            {
                objNegoAlumno.N_Insertar(objEntidadAlumno);
            }

            dtListar = objNegoAlumno.N_listado();

            //para evitar el acceso a la partida si ya se ha iniciado
            if (startStopGame("start"))
            {
                lockStart = true;
                lab_countDown2Srart.Text = "Se Acaba de quedar Fuera de la PARTIDA";
                tbx_codigoPartida.Enabled = false;
            }
            else
            {
                lockStart = false;
            }
            
            this.BackgroundImage = Properties.Resources.Fondo_Debate_Main_ConTextBox;
            BackgroundImageLayout = ImageLayout.Stretch;
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
                        // optener los settings del PROFE
                        Get_SettingsFromDatabase();

                        objEntidadAlumno.Estado = "True";
                        objNegoAlumno.N_Editar(objEntidadAlumno);

                        circularProgressBar.Visible = true;
                        LoadBar.Start();
                        tbx_codigoPartida.Enabled = false;
                    }
                    else
                    {
                        objEntidadAlumno.Estado = "False";
                        objNegoAlumno.N_Editar(objEntidadAlumno);
                        MessageBox.Show("Codigo No Valido!");
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




        private void P_PARTIDA_ALUMNO_Main_Activated(object sender, EventArgs e)
        {
            if (lockStart == false)
            {
                if (startStopGame("start"))
                {
                    // optener los settings del PROFE
                    Get_SettingsFromDatabase();

                    timer2Start.Start();
                }
                else if (startStopGame("stop"))
                {
                    btn_goToMain.PerformClick();
                }
            }
        }

        private void timer2Start_Tick(object sender, EventArgs e)
        {
            LoadBar.Stop(); // detener label Esperando
            circularProgressBar.Font = new Font(circularProgressBar.Font.Name, 40f, circularProgressBar.Font.Style, circularProgressBar.Font.Unit);


            if (countDown > 0)
            {
                countDown--;
                circularProgressBar.Text = countDown.ToString();
                lab_countDown2Srart.Text = countDown.ToString();
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
                        lab_countDown2Srart.Text = "Se Acaba de quedar Fuera de la PARTIDA";
                        tbx_codigoPartida.Enabled = false;
                    }
                }
            }
        }


        
        private void LoadBar_Tick(object sender, EventArgs e)
        {
            if (circularProgressBar.Text == "Esperando")
            {
                circularProgressBar.Text = "Esperando.";
            }
            else if (circularProgressBar.Text == "Esperando.")
            {
                circularProgressBar.Text = "Esperando..";
            }
            else if (circularProgressBar.Text == "Esperando..")
            {
                circularProgressBar.Text = "Esperando...";
            }
            else
            {
                circularProgressBar.Text = "Esperando";
            }
        }
    }
}
