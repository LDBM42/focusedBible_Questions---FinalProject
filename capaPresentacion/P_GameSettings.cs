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
    public partial class P_GameSettings : Form
    {
        public P_GameSettings(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;

            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_Listener objNegoListener = new N_Listener();
        P_SeleccionCategoria SeleccionCategoria = new P_SeleccionCategoria();
        D_Login login = new D_Login();
        P_Debate_Main PDebateMain;
        P_QueryListarPreguntas PQuery;
        
        

        private void Settings_Load(object sender, EventArgs e)
        {
            if (objEntidad.enableButtonSound)
            {
                btn_soundButton.Text = "Sonido Boton ON";
            }
            else
            {
                btn_soundButton.Text = "Sonido Boton OFF";
            }

            if (objEntidad.enableGameSound)
            {
                btn_soundGame.Text = "Sonido Juego ON";
            }
            else
            {
                btn_soundGame.Text = "Sonido Juego OFF";
            }

            //seleccionar elementos previamente seleccionados
            if (objEntidad.catEvangelios_yOtros[0] != null)
            {
                SeleccionCategoria.SeleccionarCategorías(objEntidad.catEvangelios_yOtros, lbx_catEvangelios_yOtros);
            }
            else
            {
                lbx_catEvangelios_yOtros.Text = "Todas";
            }

            if (objEntidad.opportunitiesBoolean == true && objEntidad.questions2Answer != "Todas")
            {
                lbx_opportunitie.Enabled = true;
            }
            else
            {
                if (objEntidad.opportunitiesBoolean == false) // si está desactivado
                {
                    lbx_opportunitie.Enabled = false;
                }
                else // si está desactivado el checkbox oportunidades
                {
                    if (objEntidad.questions2Answer != "Todas")
                    {
                        // las oportunidades son igual a la cantidad de preguntas
                        objEntidad.opportunities = Convert.ToInt32(objEntidad.questions2Answer);
                    }
                }
            }

            // try: para evitar que de error al estár todo deseleccionado
            try
            {
                if (objEntidad.catLibro[0] != null || lbx_catLibro.Visible == true)
                {
                    SeleccionCategoria.SeleccionarCategorías(objEntidad.catLibro, lbx_catLibro);
                }
            }
            catch (Exception)
            {
                // recuperacion de la exepcion
            }

            lbx_Dificuldad_Setting.Text = objEntidad.difficulty;
            lbx_catNuevoAntiguo.Text = objEntidad.catNuevoAntiguo;

            if (objEntidad.questions2Answer != "Todas")
            {
                if (Convert.ToInt32(objEntidad.questions2Answer) > 60)
                {
                    lbx_preguntas.Text = "Todas";
                }
                else
                {
                    lbx_preguntas.Text = objEntidad.questions2Answer;
                }
            }
            else
            {
                lbx_preguntas.Text = objEntidad.questions2Answer;
            }
            lbx_opportunitie.Text = Convert.ToString(objEntidad.opportunities);
            lbx_Rounds.Text = Convert.ToString(objEntidad.numRounds);
            lbx_time2Answer.Text = Convert.ToString(objEntidad.time2Answer);
            lab_User.Text = "User: " + E_Usuario.Nombreusuario;
            cbx_rebote.Checked = objEntidad.rebound;
            cbx_Opportunities.Checked = objEntidad.opportunitiesBoolean;

            //Mostrar los elementos seleccionados en los listbox al abrir la ventana
            lbx_Rounds.TopIndex = lbx_Rounds.SelectedIndex;
            lbx_time2Answer.TopIndex = lbx_time2Answer.SelectedIndex;
            lbx_Dificuldad_Setting.TopIndex = lbx_Dificuldad_Setting.SelectedIndex;
            lbx_catEvangelios_yOtros.TopIndex = lbx_catEvangelios_yOtros.SelectedIndex;
            lbx_catLibro.TopIndex = lbx_catLibro.SelectedIndex;
            lbx_catNuevoAntiguo.TopIndex = lbx_catNuevoAntiguo.SelectedIndex;
            lbx_opportunitie.TopIndex = lbx_opportunitie.SelectedIndex;
            lbx_preguntas.TopIndex = lbx_preguntas.SelectedIndex;
        }

        


        private void lbx_Rounds_Leave(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_Rounds.Font = new Font(lbx_Rounds.Font.Name, 15, lbx_Rounds.Font.Style, lbx_Rounds.Font.Unit);
        }

        private void lbx_time2Answer_Leave(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_time2Answer.Font = new Font(lbx_time2Answer.Font.Name, 15, lbx_time2Answer.Font.Style, lbx_time2Answer.Font.Unit);
        }

        private void lbx_Dificuldad_Setting_Leave(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_Dificuldad_Setting.Font = new Font(lbx_Dificuldad_Setting.Font.Name, 15, lbx_Dificuldad_Setting.Font.Style, lbx_Dificuldad_Setting.Font.Unit);
        }

        private void lbx_Rounds_Enter(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_Rounds.Font = new Font(lbx_Rounds.Font.Name, 31.89f, lbx_Rounds.Font.Style, lbx_Rounds.Font.Unit);
        }

        private void lbx_time2Answer_Enter(object sender, EventArgs e)
        {
            lbx_time2Answer.Font = new Font(lbx_time2Answer.Font.Name, 31.89f, lbx_time2Answer.Font.Style, lbx_time2Answer.Font.Unit);
            lbx_time2Answer.Focus();
        }

        private void lbx_Dificuldad_Setting_Enter(object sender, EventArgs e)
        {
            lbx_Dificuldad_Setting.Font = new Font(lbx_Dificuldad_Setting.Font.Name, 31.89f, lbx_Dificuldad_Setting.Font.Style, lbx_Dificuldad_Setting.Font.Unit);

        }

        private void lbx_Rounds_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                btn_Cancelar.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                lbx_time2Answer.Focus();
            }
        }

        private void lbx_time2Answer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                btn_Cancelar.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                lbx_catEvangelios_yOtros.Focus();
            }
        }

        private void lbx_Dificuldad_Setting_KeyPress(object sender, KeyPressEventArgs e)
        {
                // 'e' almacena la tecla presionada
                if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                btn_Cancelar.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                btn_Aceptar.Focus();
            }
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de cerrar sección " +
                E_Usuario.Nombreusuario + "?", "Cerrar Sección",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                E_Usuario.Logged = 0; // para desactivar autologgin
                //DESLOGEARSE
                if (!(login.AutoLoginSetLocal(E_Usuario.Nombreusuario, E_Usuario.Logged) == 1))
                {
                    MessageBox.Show("No se pudo hacer el cerrado de sección", "Cerado Sección");
                }
                else
                {
                    OpenSettings();
                }

            }
        }


        private void OpenSettings()
        {

            try
            {   // para saber si el formulario existe, o sea si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Login").SingleOrDefault<Form>();
                Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBibles").SingleOrDefault<Form>();

                if (existe != null)
                {
                    existe.Close();
                }

                if (existe2 != null) // para cerrar el juego, en caso de haberse iniciado
                {
                    existe2.Close();
                }

                P_Login login = new P_Login();
                login.reOpened++;
                this.Hide();
                login.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Algo salió mal, Favor intentarlo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        private void btn_newUser_Click(object sender, EventArgs e)
        {
            P_Usuario usuario = new P_Usuario();
            usuario.Show();

            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBibles").SingleOrDefault<Form>();

            if (existe == null) // para ocultar settings al abrir nuevo usuario, solo en caso de que el juego no se haya iniciado
            {
                this.Hide();
                usuario.TopMost = false;
            }
            else
            {
                usuario.TopMost = true;
                usuario.WindowState = FormWindowState.Maximized;
            }

        }

        private void Settings_Activated(object sender, EventArgs e)
        {
            lab_User.Text = "User: " + E_Usuario.Nombreusuario;
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            PQuery = new P_QueryListarPreguntas();
            Change_Settings();

            try
            {   // para saber si el formulario existe, o sea si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Debate_Main").SingleOrDefault<Form>();
                Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

                if (existe != null) // si el formulario existe
                {
                    //para cerrarlo y liberar el espacio en memoria
                    existe.Close();
                    existe.Dispose();
                    GC.Collect();

                    PDebateMain = new P_Debate_Main(objEntidad);

                    PDebateMain.Show();
                    this.Hide();
                }
                else
                {
                    if (existe2 != null) // para saber si el formulario principal existe
                    {
                        this.AddOwnedForm(existe2); //indica que este va a ser el papa del form P_Main
                        existe2.Close(); // cerrar ventana principal
                    }

                    P_Main PMain = new P_Main(objEntidad);
                    this.AddOwnedForm(PMain); //indica que este va a ser el papa del form P_Main

                    PMain.Show();
                    this.RemoveOwnedForm(PMain); //indica que este va a dejar de ser el papa del form P_Main
                    this.Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error inexperado!, favor volver a intentarlo.");
            }

        }

        public void Change_Settings()
        {
            objEntidad.difficulty = lbx_Dificuldad_Setting.Text;
            objEntidad.catEvangelios_yOtros = SeleccionCategoria.AlmacenarSeleccionCategorías(lbx_catEvangelios_yOtros);
            objEntidad.catLibro = SeleccionCategoria.AlmacenarSeleccionCategorías(lbx_catLibro);
            objEntidad.catNuevoAntiguo = lbx_catNuevoAntiguo.Text;
            objEntidad.numRounds = Convert.ToInt32(lbx_Rounds.Text);
            objEntidad.time2Answer = Convert.ToInt32(lbx_time2Answer.Text);
            objEntidad.rebound = cbx_rebote.Checked;
            if (objEntidad.opportunitiesBoolean == false)
            {
                // las oportunidades son igual a la cantidad de preguntas
                objEntidad.opportunities = Convert.ToInt32(lbx_preguntas.Text);
            }

            int TotalQuestToAnswer;
            /*las pregutas totales son la cantidad de preguntas seleccionadas * cantidad de rondas*/
            if (objEntidad.questions2Answer != "Todas")
            {
                TotalQuestToAnswer = Convert.ToInt32(objEntidad.questions2Answer) * objEntidad.numRounds;
            }
            else
            {
                TotalQuestToAnswer = 0;
            }

            objEntidad.queryListarPreguntas = PQuery.QueryPorCategoriayDificultad(lbx_catEvangelios_yOtros,
                                                                                  lbx_catLibro,
                                                                                  lbx_catNuevoAntiguo,
                                                                                  objEntidad.difficulty,
                                                                                  TotalQuestToAnswer,
                                                                                  objEntidad.opportunitiesBoolean);
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbx_catEvangelios_yOtros_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                btn_Cancelar.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                lbx_catLibro.Focus();
            }
        }

        private void lbx_catLibro_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                btn_Cancelar.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                lbx_catNuevoAntiguo.Focus();
            }
        }

        private void lbx_catNuevoAntiguo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                btn_Cancelar.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                lbx_Dificuldad_Setting.Focus();
            }
        }

        private void lbx_catEvangelios_yOtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            // evitar que se deseleccionen todos los campos
            try
            {
                lbx_catEvangelios_yOtros.SelectedItem.ToString();
            }
            catch (Exception)
            {
                lbx_catEvangelios_yOtros.SetSelected(10, true);
            }

            if (lbx_catEvangelios_yOtros.SelectedItem.ToString() == "Todas")
            {
                SeleccionCategoria.bloquearBesbloquearDeseleccionarCamposCategoría(false, lbx_catNuevoAntiguo);
                SeleccionCategoria.bloquearBesbloquearDeseleccionarCamposCategoría(false, lbx_catLibro);
            }
            else
            {
                SeleccionCategoria.bloquearBesbloquearDeseleccionarCamposCategoría(true, lbx_catNuevoAntiguo);
                SeleccionCategoria.bloquearBesbloquearDeseleccionarCamposCategoría(true, lbx_catLibro);
            }
        }

        private void lbx_opportunitie_SelectedIndexChanged(object sender, EventArgs e)
        {
            // igualar oportunidades a preguntas si estas son mayores que las preguntas
            if (lbx_preguntas.Text != "Todas" && lbx_opportunitie.Text != "")
            {
                if (Convert.ToInt32(lbx_opportunitie.Text) > Convert.ToInt32(lbx_preguntas.Text))
                {
                    MessageBox.Show("Las Oportunidades no pueden ser mayores que las Preguntas", "Error");
                    lbx_opportunitie.Text = lbx_preguntas.Text;
                }
            }

            objEntidad.opportunities = Convert.ToInt32(lbx_opportunitie.Text);
        }

        private void lbx_preguntas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // igualar oportunidades a preguntas si estas son mayores que las preguntas y las oportunidades están abilitadas
            if (lbx_preguntas.Text != "Todas" && objEntidad.opportunitiesBoolean == true
                && lbx_opportunitie.Text != "")
            {
                if ((Convert.ToInt32(lbx_preguntas.Text) < Convert.ToInt32(lbx_opportunitie.Text))
                    && cbx_Opportunities.Checked == true)
                {
                    MessageBox.Show("Las Preguntas no pueden ser menores que las Oportunidades", "Error");
                    lbx_preguntas.Text = lbx_opportunitie.Text;
                }
            }

            objEntidad.questions2Answer = lbx_preguntas.Text;

            if (lbx_preguntas.Text == "Todas")
            {
                cbx_Opportunities.Checked = true;
            }
        }


    private void cbx_Opportunities_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Opportunities.Checked) // si está activado
            {
                lbx_opportunitie.Enabled = true;
                objEntidad.opportunitiesBoolean = true;
            }
            else
            {
                if (lbx_preguntas.Text == "Todas") // si está desactivado y preguntas es igual a todas
                {
                    cbx_Opportunities.Checked = true;
                }
                else // si está desactivado y preguntas es diferente de todas
                {
                    lbx_opportunitie.Enabled = false;
                    objEntidad.opportunitiesBoolean = false;
                    // las oportunidades son igual a la cantidad de preguntas
                    objEntidad.opportunities = Convert.ToInt32(lbx_preguntas.Text);
                }                
            }

            // igualar oportunidades a preguntas si estas son mayores que las preguntas
            if (lbx_preguntas.Text != "Todas" && lbx_opportunitie.Text != "")
            {
                if (Convert.ToInt32(lbx_opportunitie.Text) > Convert.ToInt32(lbx_preguntas.Text))
                {
                    MessageBox.Show("Las Oportunidades no pueden ser mayores que las Preguntas", "Error");
                    lbx_opportunitie.Text = lbx_preguntas.Text;
                }
            }
        }

        private void cbx_rebote_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_rebote.Checked == true)
            {
            }
            else
            {
            }
        }

        private void btn_soundButton_Click(object sender, EventArgs e)
        {
            if (btn_soundButton.Text == "Sonido Boton ON")
            {
                objEntidad.enableButtonSound = false;
                btn_soundButton.Text = "Sonido Boton OFF";
            }
            else
            {
                objEntidad.enableButtonSound = true;
                btn_soundButton.Text = "Sonido Boton ON";
            }
        }

        private void btn_soundGame_Click(object sender, EventArgs e)
        {
            if (btn_soundGame.Text == "Sonido Juego ON")
            {
                objEntidad.enableGameSound = false;
                btn_soundGame.Text = "Sonido Juego OFF";
            }
            else
            {
                objEntidad.enableGameSound = true;
                btn_soundGame.Text = "Sonido Juego ON";
            }
        }
    }
}
