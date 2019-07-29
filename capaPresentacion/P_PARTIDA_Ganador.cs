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
    public partial class P_PARTIDA_Ganador : Form
    {
        public P_PARTIDA_Ganador(E_focusedBible Configuracion)
        {
            this.objEntidad = Configuracion;
            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        N_AlumnoPartida objNegoAlumno = new N_AlumnoPartida();
        N_Listener objNegoListener = new N_Listener();//-------------------------------------------

        DataSet ds;
        DataTable dt;


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
            objEntidad.reproducirSonidoJuego("fanfare.wav", true);
            SetDoubleBuffered(tableLayoutPanel1);
            SetDoubleBuffered(tableLayoutPanel7);
            SetDoubleBuffered(tableLayoutPanel19);
            SetDoubleBuffered(tableLayoutPanel21);


            this.BackgroundImage = Properties.Resources.Focused_bible_PARTIDA_marcador;
            BackgroundImageLayout = ImageLayout.Stretch;
                        
            listarAlumnosYGanador();
            SetFinalResults();

            //actualizar imagen del sonido
            if (objEntidad.enableButtonSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }
        }


        private void listarAlumnosYGanador()
        {
            dt = objNegoAlumno.N_ListarPuntuacionFinal();
            //DataSource permite vaciar un DataTable en un dataGridView
            dgvAlumnosPuntuacionFinal.DataSource = dt;
        }

        void SetFinalResults()
        {
            if (dt.Rows.Count > 0)
            {
                //Alumno
                lbl_ganador_Alumno.Text = dt.Rows[0]["Alumno"].ToString();

                if (dt.Rows.Count > 1) //si existe un 2do lugar
                {
                    lbl_ganador_Alumno_2do.Text = dt.Rows[1]["Alumno"].ToString();
                }

                if (dt.Rows.Count > 2) //si existe un 3er lugar
                {
                    lbl_ganador_Alumno_3ro.Text = dt.Rows[2]["Alumno"].ToString();
                }
            }
            
        }


        private void btn_goToMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_goToMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                this.DialogResult = DialogResult.OK; //cierra el esta ventana y deja vista la ventana Main
            }
        }

        private void btn_goToMain_MouseEnter(object sender, EventArgs e)
        {
            btn_goToMain.BackgroundImage = Properties.Resources.Focused_bible_SOLO_07_MouseEnter;
        }

        private void btn_goToMain_MouseLeave(object sender, EventArgs e)
        {
            btn_goToMain.BackgroundImage = Properties.Resources.Focused_bible_SOLO_07;
        }
        
        private void pbx_Sound_Click(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                objEntidad.StopGameSound();
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_OFF;
                objEntidad.enableGameSound = false;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_ON;
                objEntidad.enableGameSound = true;
                objEntidad.reproducirSonidoJuego("fanfare.wav", true);
            }
        }

        private void pbx_Sound_MouseEnter(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
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
            if (objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }
        }

        private void dgvAlumnosPuntuacionFinal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (dgvAlumnosPuntuacionFinal.Rows[e.RowIndex].Index < 3)
            //{
            //    e.CellStyle.BackColor = Color.FromArgb(60, 255, 0);
            //    e.CellStyle.ForeColor = Color.FromArgb(255, 255, 255);
            //}
        }

        private void P_PARTIDA_Ganador_Activated(object sender, EventArgs e)
        {
            ds = objNegoListener.N_Listener_Comando(1);
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Comando"].ToString() == "stop")
                    {
                        btn_goToMain.PerformClick();
                    }
                }
        }
    }
}
