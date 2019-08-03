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
        public P_Conf_Preguntas_Agregar(E_focusedBible configuracion)
        {
            objEntidad = configuracion;

            InitializeComponent();
            listar_question();
        }

        #region Variables y Objetos

        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        private static DataTable dt = new DataTable();
        private HowToPlay howToPlay;
        bool soundsVisible;
        string operacion;
        int IdLibro;

        #endregion

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
            operacion = "Insertar";
            CMB_Buscar.Text = "Pregunta";

            tbx_Preg.Focus(); // enfocar el textbox preguntas
            
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

        
        private void btn_goBack_Click(object sender, EventArgs e)
        {
            Agregar_O_Modificar("Insertar");

            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();
            Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Configuracion").SingleOrDefault<Form>();

            if (existe != null) // para saber si el formulario principal existe
            {
                this.AddOwnedForm(existe); //indica que este va a ser el papa del form P_Main
                existe.Close(); // cerrar ventana principal
            }

            P_Main PMain = new P_Main(objEntidad);
            this.AddOwnedForm(PMain); //indica que este va a ser el papa del form P_Main

            PMain.Show();

            if (existe2 != null) // si existe la ventana Configuración
            {
                existe2.Show();
            }

            this.RemoveOwnedForm(PMain); //indica que este va a dejar de ser el papa del form P_Main
            this.Close();
        }

        //Este es el metodo de Listar
        void listar_question()
        {
            Datos.AllowUserToAddRows = false; // para que no aparezca la ultima fila
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
            objEntidad.resp = Convert.ToChar(cbx_Resp.Text);
            objEntidad.pasage = tbx_Pasaje.Text;
            objEntidad.difficulty = cbx_Dificultad.Text;

            objEntidad.Libros = cbx_catLibro.Text;

            objNego.N_InsertarPreguntas(objEntidad);

            MessageBox.Show("Registro AGREGADO con exito");
        }
                
        //Este es el Metodo para limpiar
        void limpiar()
        {
            tbx_Preg.Text = "";
            tbx_a.Text = "";
            tbx_b.Text = "";
            tbx_c.Text = "";
            tbx_d.Text = "";
            cbx_Resp.Text = "";
            tbx_Pasaje.Text = "";
            cbx_Dificultad.Text = "";
            cbx_catLibro.Text = "";
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
        
        //Boton para insertar nuevas preguntas
        private void btn_Save_Click(object sender, EventArgs e)
        {
            DialogResult confirm;
            string validationResult = ValidarRegistrosVacios();


            if (validationResult == "") // si hay algun campo vacío
            {
                if (operacion == "Insertar")
                {
                    confirm = MessageBox.Show("Estas seguro de AGREGAR este registro?", "Confirmación", MessageBoxButtons.YesNo);

                    if (confirm == DialogResult.Yes)
                    {
                        Insertar();
                        listar_question();
                    }
                }
                else if (operacion == "Editar")
                {
                    confirm = MessageBox.Show("Estas seguro de EDITAR este registro?", "Confirmación", MessageBoxButtons.YesNo);

                    if (confirm == DialogResult.Yes)
                    {
                        editar();
                        listar_question();
                        limpiar();
                    }
                }
            }
            else // si todos los campos están llenos
            {
                MessageBox.Show("Favor llenar :" + validationResult, "Operación cancelada!");                
            }        
        }

        private string ValidarRegistrosVacios()
        {
            string resultado = "";

            if (tbx_Preg.Text == "")
            {
                resultado += "\n> La pregunta";
            }
            if (tbx_a.Text == "")
            {
                resultado += "\n> La respuesta (a)";
            }
            if (tbx_b.Text == "")
            {
                resultado += "\n> La respuesta (b)";
            }
            if (tbx_c.Text == "")
            {
                resultado += "\n> La respuesta (c)";
            }
            if (tbx_d.Text == "")
            {
                resultado += "\n> La respuesta (d)";
            }
            if (cbx_Resp.Text == "")
            {
                resultado += "\n> La respuesta correcta";
            }
            if (tbx_Pasaje.Text == "")
            {
                tbx_Pasaje.Text = "N/A";
            }
            if (cbx_catLibro.Text == "")
            {
                resultado += "\n> El libro";
            }
            if (cbx_Dificultad.Text == "")
            {
                resultado += "\n> La dificultad";
            }

            return resultado;
        }

        private void editar()
        {
            objEntidad.preg = tbx_Preg.Text;
            objEntidad.a = tbx_a.Text;
            objEntidad.b = tbx_b.Text;
            objEntidad.c = tbx_c.Text;
            objEntidad.d = tbx_d.Text;
            objEntidad.resp = Convert.ToChar(cbx_Resp.Text);
            objEntidad.pasage = tbx_Pasaje.Text;
            objEntidad.difficulty = cbx_Dificultad.Text;

            objEntidad.Libros = cbx_catLibro.Text;


            objNego.N_EditarPreguntas(objEntidad);

            MessageBox.Show("Registro MODIFICADO con exito");
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            objEntidad.preg = Datos.Rows[e.RowIndex].Cells["Pregunta"].Value.ToString();
            objEntidad.a = Datos.Rows[e.RowIndex].Cells["a"].Value.ToString();
            objEntidad.b = Datos.Rows[e.RowIndex].Cells["b"].Value.ToString();
            objEntidad.c = Datos.Rows[e.RowIndex].Cells["c"].Value.ToString();
            objEntidad.d = Datos.Rows[e.RowIndex].Cells["d"].Value.ToString();
            objEntidad.resp = Convert.ToChar(Datos.Rows[e.RowIndex].Cells["Respuesta"].Value);
            objEntidad.pasage = Datos.Rows[e.RowIndex].Cells["Pasaje"].Value.ToString();
            objEntidad.difficulty = Datos.Rows[e.RowIndex].Cells["Dificultad"].Value.ToString();
            objEntidad.Libros = Datos.Rows[e.RowIndex].Cells["Libro"].Value.ToString();
           
            
            if (Datos.Rows[e.RowIndex].Cells["Eliminar"].Selected)
            {
                DialogResult delete;

                delete = MessageBox.Show("Seguro que desea ELIMINAR este Registro?", "Advertencia", MessageBoxButtons.YesNo);
                
                if (delete == DialogResult.Yes)
                {
                    int eliminar = Convert.ToInt32(Datos.Rows[e.RowIndex].Cells["codPreg"].Value.ToString());
                    objNego.N_eliminarPreguntas(eliminar);
                    MessageBox.Show("Registro ELIMINADO con exito");
                    listar_question();
                }
            }
            else if (Datos.Rows[e.RowIndex].Cells["Editar"].Selected)
            {
                Agregar_O_Modificar("Editar");

                objEntidad.codPreg = Convert.ToInt32(Datos.Rows[e.RowIndex].Cells["codPreg"].Value);
                tbx_Preg.Text = Datos.Rows[e.RowIndex].Cells["Pregunta"].Value.ToString();
                tbx_a.Text = Datos.Rows[e.RowIndex].Cells["a"].Value.ToString();
                tbx_b.Text = Datos.Rows[e.RowIndex].Cells["b"].Value.ToString();
                tbx_c.Text = Datos.Rows[e.RowIndex].Cells["c"].Value.ToString();
                tbx_d.Text = Datos.Rows[e.RowIndex].Cells["d"].Value.ToString();
                cbx_Resp.Text = Datos.Rows[e.RowIndex].Cells["Respuesta"].Value.ToString();
                tbx_Pasaje.Text = Datos.Rows[e.RowIndex].Cells["Pasaje"].Value.ToString();
                cbx_Dificultad.Text = Datos.Rows[e.RowIndex].Cells["Dificultad"].Value.ToString();
                cbx_catLibro.Text = Datos.Rows[e.RowIndex].Cells["Libro"].Value.ToString();
            }
        }

        private void Agregar_O_Modificar(string Operation)
        {
            if (Operation == "Editar")
            {
                operacion = "Editar";
                lab_Agregar_Modificar.Text = "MODIFICAR";
                pbx_AgregarModificar.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_PREGUNTAS_04;
                this.BackColor = Color.FromArgb(23, 33, 38);
            }
            else if (Operation == "Insertar")
            {
                operacion = "Insertar";
                lab_Agregar_Modificar.Text = "AGREGAR";
                pbx_AgregarModificar.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_PREGUNTAS_03;
                this.BackColor = Color.FromArgb(40, 54, 62);
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

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = new HowToPlay(objEntidad);
            this.Hide();/************************************************************/
            howToPlay.ShowDialog();
            this.Show();/************************************************************/
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

        private void btn_goBack_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_goBack.BackgroundImage = Properties.Resources.goBack_MouseEnter_01_01;
        }

        private void btn_goBack_MouseLeave(object sender, EventArgs e)
        {
            btn_goBack.BackgroundImage = Properties.Resources.goBack_MouseLeave_01;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Agregar_O_Modificar("Insertar");
            listar_question();
            //Datos.Rows[0].Cells["Eliminar"].Selected = true;
        }

        private void tbx_Pasaje_TextChanged(object sender, EventArgs e)
        {
            if (tbx_Pasaje.Text == "")
            {
                tbx_Pasaje.Text = "N/A";
                tbx_Pasaje.SelectAll();
            }
        }

        private void tbx_Pasaje_MouseClick(object sender, MouseEventArgs e)
        {
            if (tbx_Pasaje.Text == "N/A")
            {
                tbx_Pasaje.SelectAll();
            }
        }

        private void tbx_Pasaje_Enter(object sender, EventArgs e)
        {
            if (tbx_Pasaje.Text == "N/A")
            {
                tbx_Pasaje.SelectAll();
            }
        }

        private void btn_Save_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Save.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
        }

        private void btn_Save_MouseLeave(object sender, EventArgs e)
        {
            btn_Save.BackgroundImage = Properties.Resources.Boton_Empezar_MouseLeave;
        }

        private void btn_cancelar_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_cancelar.BackgroundImage = Properties.Resources.Cancelar_MouseEnter_01;
        }

        private void btn_cancelar_MouseLeave(object sender, EventArgs e)
        {
            btn_cancelar.BackgroundImage = Properties.Resources.Cancelar_01;
        }
    }
}
