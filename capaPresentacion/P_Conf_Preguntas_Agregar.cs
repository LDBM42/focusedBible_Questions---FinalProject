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

namespace capaPresentacion
{
    public partial class P_Conf_Preguntas_Agregar : Form
    {
        #region Variables y Objetos
        string[] comodin50_1 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodin50_2 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage_1 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage_2 = new string[] { "0", "+1", "+2", "+3" };
        char[] desaparecer50 = new char[] { 'a', 'b', 'c', 'd' };
        

        #endregion

        int IdLibro;

        public P_Conf_Preguntas_Agregar(E_focusedBible objEntidad)
        {
            InitializeComponent();
            listar_question();
        }


        E_focusedBible objEntidad = new E_focusedBible();
        private bool soundsVisible;
        private HowToPlay howToPlay;


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

        private void P_Conf_Preguntas_Agregar_Load(object sender, EventArgs e)
        {
            listar_question();
            CMB_Buscar.Text = "dificultad";
           
        }

        
        N_focusedBible objNego = new N_focusedBible();
        private int[] noQuestions;
        //private object dt;
        private static DataTable dt = new DataTable();

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

        //Este es el metodo de Listar
        void listar_question()
        {
            dt = objNego.N_listadoParaConfiguracion();
            Datos.DataSource = dt;
        }

        //Este es el metodo de Insertar
        void Insertar()
        {
            objEntidad.preg = tbx_Preg.Text;
            objEntidad.a = tbx_a.Text;
            objEntidad.b = tbx_b.Text;
            objEntidad.c = tbx_c.Text;
            objEntidad.d = tbx_d.Text;
            objEntidad.resp = Convert.ToChar(tbx_Resp.Text);
            objEntidad.pasage = tbx_Pasage.Text;
            objEntidad.difficulty = lbx_Dificultad.Text;

            objEntidad.Libros = lbx_catLibro.Text;

            objNego.N_InsertarPreguntas(objEntidad);

            MessageBox.Show("Registro Insertado con exito");
        }
        
        


        //Este es el Metodo para limpiar
        void limpiar()
        {
           
            tbx_Preg.Text = "";
            tbx_a.Text = "";
            tbx_b.Text = "";
            tbx_c.Text = "";
            tbx_d.Text = "";
            tbx_Resp.Text = "";
            tbx_Pasage.Text = "";
            lbx_Dificultad.Text = "Normal";

        }     
        
       

        private void Buscar_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt.Copy());
                try
                {
                    dv.RowFilter = CMB_Buscar.Text + " Like '%" + Buscar.Text + "%'";
                }
                catch (Exception)
                {
                    if (Buscar.Text != "")
                    {
                        try
                        {
                            if (Convert.ToInt32(Buscar.Text) >= 0)
                                dv.RowFilter = CMB_Buscar.Text + " = " + Buscar.Text;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Favor escribir un valor correcto", "Texto incorrecto");
                            Buscar.Text = "";
                            Buscar.Focus();
                        }
                    }
                }

                Datos.DataSource = dv;

                if (dv.Count == 0)
                {
                    noencontrado.Visible = true;
                }
                else
                {
                    noencontrado.Visible = false;
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Favor escribir un valor correcto", "Texto incorrecto");
                Buscar.Text = "";
                Buscar.Focus();
            }
        }

        private void CMB_Buscar_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Buscar_TextChanged_1(null, null);
            Buscar.Focus();
        }
        //Boton para insertar nuevas preguntas
        private void btn_NewQuests_Click_1(object sender, EventArgs e)
        {
            DialogResult confirm;

            confirm = MessageBox.Show("Are you sure you want to add this question?", "Confirmation", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                Insertar();
                listar_question();
            }
        }

       

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            objEntidad.preg = Datos.Rows[e.RowIndex].Cells["preg"].Value.ToString();
            objEntidad.a = Datos.Rows[e.RowIndex].Cells["a"].Value.ToString();
            objEntidad.b = Datos.Rows[e.RowIndex].Cells["b"].Value.ToString();
            objEntidad.c = Datos.Rows[e.RowIndex].Cells["c"].Value.ToString();
            objEntidad.d = Datos.Rows[e.RowIndex].Cells["d"].Value.ToString();
            objEntidad.resp = Convert.ToChar(Datos.Rows[e.RowIndex].Cells["resp"].Value);
            objEntidad.pasage = Datos.Rows[e.RowIndex].Cells["pasage"].Value.ToString();
            objEntidad.difficulty = Datos.Rows[e.RowIndex].Cells["dificultad"].Value.ToString();
            objEntidad.Libros = Datos.Rows[e.RowIndex].Cells["nombreLibro"].Value.ToString();

           
            
            
           
            
           
            
            
           


            if (Datos.Rows[e.RowIndex].Cells["Delete"].Selected)
            {
                int eliminar = Convert.ToInt32(Datos.Rows[e.RowIndex].Cells["codPreg"].Value.ToString());
                objNego.N_eliminarPreguntas(eliminar);
                MessageBox.Show("Pregunta Eliminado con exito");
                listar_question();
            }
            else if (Datos.Rows[e.RowIndex].Cells["Edit"].Selected)
            {
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Conf_Preguntas_Modificar").SingleOrDefault<Form>();

                if (existe != null) // para saber si el formulario principal existe
                {
                    this.AddOwnedForm(existe); //indica que este va a ser el papa del form P_Main
                    existe.Close(); // cerrar ventana principal
                }

                P_Conf_Preguntas_Modificar conf_Preguntas_Modificar = new P_Conf_Preguntas_Modificar(objEntidad);
                this.AddOwnedForm(conf_Preguntas_Modificar); //indica que este va a ser el papa del form P_Main

                conf_Preguntas_Modificar.Show();
                this.RemoveOwnedForm(conf_Preguntas_Modificar); //indica que este va a dejar de ser el papa del form P_Main
                this.Close();

            }
        }
    }
}
