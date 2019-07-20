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
    public partial class P_Conf_Preguntas_Modificar : Form
    {



        #region Variables y Objetos
        string[] comodin50_1 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodin50_2 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage_1 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage_2 = new string[] { "0", "+1", "+2", "+3" };
        char[] desaparecer50 = new char[] { 'a', 'b', 'c', 'd' };


        #endregion

        int IdLibro;


        public P_Conf_Preguntas_Modificar(E_focusedBible objEntidad)
        {
            this.objEntidad = objEntidad;
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

        private void P_Conf_Preguntas_Modificar_Load(object sender, EventArgs e)
        {

            tbx_Preg.Text = objEntidad.preg;
            tbx_a.Text = objEntidad.a;
            tbx_b.Text = objEntidad.b;
            tbx_c.Text = objEntidad.c;
            tbx_d.Text = objEntidad.d;
            tbx_Resp.Text = objEntidad.resp.ToString();
            tbx_Pasage.Text = objEntidad.pasage;
            lbx_Dificultad.Text = objEntidad.difficulty;
            lbx_catLibro.Text = objEntidad.Libros;





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

        private void Buscar_TextChanged(object sender, EventArgs e)
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

        private void CMB_Buscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Buscar_TextChanged(null, null);
            Buscar.Focus();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            editar();
            listar_question();
            limpiar();
        }

        //Este es el metodo Editar

        void editar()
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


            objNego.N_EditarPreguntas(objEntidad);

            MessageBox.Show("Registro Editado con exito");

        }


        private void Datos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (Datos.Rows[e.RowIndex].Cells["Eliminarlo"].Selected)
            {
                int eliminar = Convert.ToInt32(Datos.Rows[e.RowIndex].Cells["codPreg"].Value.ToString());
                objNego.N_eliminarPreguntas(eliminar);
                MessageBox.Show("Pregunta Eliminado con exito");
                listar_question();
            }
            else if (Datos.Rows[e.RowIndex].Cells["Modificarlo"].Selected)
            {
                objEntidad.codPreg = Convert.ToInt32(Datos.Rows[e.RowIndex].Cells["codpreg"].Value);
                tbx_Preg.Text = Datos.Rows[e.RowIndex].Cells["preg"].Value.ToString();
                tbx_a.Text = Datos.Rows[e.RowIndex].Cells["a"].Value.ToString();
                tbx_b.Text = Datos.Rows[e.RowIndex].Cells["b"].Value.ToString();
                tbx_c.Text = Datos.Rows[e.RowIndex].Cells["c"].Value.ToString();
                tbx_d.Text = Datos.Rows[e.RowIndex].Cells["d"].Value.ToString();
                tbx_Resp.Text = Datos.Rows[e.RowIndex].Cells["resp"].Value.ToString();
                tbx_Pasage.Text = Datos.Rows[e.RowIndex].Cells["pasage"].Value.ToString();
                lbx_Dificultad.Text = Datos.Rows[e.RowIndex].Cells["dificultad"].Value.ToString();
                lbx_catLibro.Text = Datos.Rows[e.RowIndex].Cells["nombreLibro"].Value.ToString();

                listar_question();


            }
        }
    }
}
