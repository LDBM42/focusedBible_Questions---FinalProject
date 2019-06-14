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
        public P_GameSettings(string[] catEvangelios_yOtros = null, string[] catLibro = null, string catNuevoAntiguo = "", 
                              string grupo1 = "Grupo 1", string grupo2 = "Grupo 2", int numRounds = 1, 
                              int time2Answer = 20, string difficulty = "Todas")
        {
            this.numRounds = numRounds;
            this.grupo1 = grupo1;
            this.grupo2 = grupo2;
            this.time2Answer = time2Answer;
            this.difficulty = difficulty;
            this.catEvangelios_yOtros = catEvangelios_yOtros;
            this.catLibro = catLibro;
            this.catNuevoAntiguo = catNuevoAntiguo;
            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_Listener objNegoListener = new N_Listener();
        P_SeleccionCategoria SeleccionCategoria = new P_SeleccionCategoria();
        D_Login login = new D_Login();
        P_Debate_Main PDebateMain;
        P_QueryListarPreguntas PQuery;

        string grupo1;
        string grupo2;
        public string difficulty;
        public string queryPorDificultad;
        public string [] catEvangelios_yOtros = new string [10];
        public string [] catLibro = new string[66];
        public string catNuevoAntiguo;
        public int numRounds;
        public int time2Answer;
        

        private void Settings_Load(object sender, EventArgs e)
        {
            //seleccionar elementos previamente seleccionados
            if (catEvangelios_yOtros[0] != null)
            {
                SeleccionCategoria.SeleccionarCategorías(catEvangelios_yOtros, lbx_catEvangelios_yOtros);
            }
            else
            {
                lbx_catEvangelios_yOtros.Text = "Todas";
            }

            // try: para evitar que de error al estár todo deseleccionado
            try
            {
                if (catLibro[0] != null || lbx_catLibro.Visible == true)
                {
                    SeleccionCategoria.SeleccionarCategorías(catLibro, lbx_catLibro);
                }
            }
            catch (Exception)
            {
                // recuperacion de la exepcion
            }

            lbx_Dificuldad_Setting.Text = difficulty;
            lbx_catNuevoAntiguo.Text = catNuevoAntiguo;
            lbx_Rounds.Text = Convert.ToString(numRounds);
            lbx_time2Answer.Text = Convert.ToString(time2Answer);
            numRounds = Convert.ToInt32(lbx_Rounds.Text);
            time2Answer = Convert.ToInt32(lbx_time2Answer.Text);
            lab_User.Text = "User: " + E_Usuario.Nombreusuario;

            //Mostrar los elementos seleccionados en los listbox al abrir la ventana
            lbx_Rounds.TopIndex = lbx_Rounds.SelectedIndex;
            lbx_time2Answer.TopIndex = lbx_time2Answer.SelectedIndex;
            lbx_Dificuldad_Setting.TopIndex = lbx_Dificuldad_Setting.SelectedIndex;
            lbx_catEvangelios_yOtros.TopIndex = lbx_catEvangelios_yOtros.SelectedIndex;
            lbx_catLibro.TopIndex = lbx_catLibro.SelectedIndex;
            lbx_catNuevoAntiguo.TopIndex = lbx_catNuevoAntiguo.SelectedIndex;
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

                    PDebateMain = new P_Debate_Main();

                    PDebateMain.numRounds = numRounds;
                    PDebateMain.difficulty = difficulty;
                    PDebateMain.catEvangelios_yOtros = catEvangelios_yOtros;
                    PDebateMain.catLibro = catLibro;
                    PDebateMain.catNuevoAntiguo = catNuevoAntiguo;
                    PDebateMain.time2Answer = time2Answer;
                    PDebateMain.queryPorDificultad = queryPorDificultad;
                    
                    // mostrar los nombres que estan jugando
                    PDebateMain.tbx_Grupo1.Text = grupo1;
                    PDebateMain.tbx_Grupo2.Text = grupo2;

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

                    P_Main PMain = new P_Main();
                    this.AddOwnedForm(PMain); //indica que este va a ser el papa del form P_Main

                    PMain.numRounds = numRounds;
                    PMain.difficulty = difficulty;
                    PMain.catEvangelios_yOtros = catEvangelios_yOtros;
                    PMain.catLibro = catLibro;
                    PMain.catNuevoAntiguo = catNuevoAntiguo;
                    PMain.time2Answer = time2Answer;
                    PMain.queryPorDificultad = queryPorDificultad;

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
            difficulty = lbx_Dificuldad_Setting.Text;
            queryPorDificultad = PQuery.QueryPorCategoriayDificultad(lbx_catEvangelios_yOtros,lbx_catLibro,lbx_catNuevoAntiguo,difficulty);
            catEvangelios_yOtros = SeleccionCategoria.AlmacenarSeleccionCategorías(lbx_catEvangelios_yOtros);
            catLibro = SeleccionCategoria.AlmacenarSeleccionCategorías(lbx_catLibro);
            catNuevoAntiguo = lbx_catNuevoAntiguo.Text;
            numRounds = Convert.ToInt32(lbx_Rounds.Text);
            time2Answer = Convert.ToInt32(lbx_time2Answer.Text);
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
    }
}
