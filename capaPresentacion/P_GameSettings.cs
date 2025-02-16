﻿using System;
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
using System.IO;

namespace capaPresentacion
{
    public partial class P_GameSettings : Form
    {
        public P_GameSettings(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;
            InitializeComponent();
        }

        HowToPlay howToPlay;
        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        N_SettingsPROFE objNegoSettingsPROFE = new N_SettingsPROFE();
        P_SeleccionCategoria SeleccionCategoria = new P_SeleccionCategoria();
        D_Login login = new D_Login();
        P_DUO_Main PDebateMain;
        bool soundsVisible = false;



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




        private void Settings_Load(object sender, EventArgs e)
        {
            SetDoubleBuffered(tlyo_configuracionJuego);
            SetDoubleBuffered(tableLayoutPanel2);
            SetDoubleBuffered(tableLayoutPanel3);
            SetDoubleBuffered(tableLayoutPanel4);
            SetDoubleBuffered(tlyo_categorias);
            SetDoubleBuffered(tableLayoutPanel7);
            SetDoubleBuffered(tableLayoutPanel8);
            SetDoubleBuffered(tableLayoutPanel16);
            SetDoubleBuffered(tableLayoutPanel17);
            SetDoubleBuffered(tableLayoutPanel22);
            SetDoubleBuffered(tableLayoutPanel23);

            this.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Fondo_01;
            this.BackgroundImageLayout = ImageLayout.Stretch;


            lbx_Dificuldad_Setting.Text = objEntidad.difficulty;

            //Activar o desactivar Checkboxes
            cbx_categoriaXTestamento.Checked = objEntidad.catNuevoAntiguoChecked;
            cbx_categoriaXTipoLibro.Checked = objEntidad.catEvangelios_yOtrosChecked;
            cbx_categoriaXLibro.Checked = objEntidad.catLibroChecked;

            //Seleccionar Testamento
            lbx_catNuevoAntiguo.Text = objEntidad.catNuevoAntiguo;


            // actualizar imagen del sonido
            if (objEntidad.enableGameSound == true)
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseLeave;
            }
            else
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseLeave_OFF;
            }

            if (objEntidad.enableButtonSound == true)
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseLeave;
            }
            else
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseLeave_OFF;
            }

            if (objEntidad.enableButtonSound == true || objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON_NEUTRO;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }

            //seleccionar elementos previamente seleccionados
            try
            {
                if (objEntidad.catEvangelios_yOtros[0] != null)
                {
                    SeleccionCategoria.SeleccionarCategorías(objEntidad.catEvangelios_yOtros, lbx_catEvangelios_yOtros);
                }
            }
            catch (Exception)
            {
                // recuperacion de la exepcion
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


            //cambiar cantidad de preguntas impar a par si es diferente de Todas
            if (objEntidad.questions2Answer != "Todas")
            {
                if (Convert.ToInt32(objEntidad.questions2Answer) % 2 != 0)
                {
                    objEntidad.questions2Answer = (Convert.ToInt32(objEntidad.questions2Answer) - 1).ToString();
                }
            }

            //cantidad de preguntas a responder
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

            lbx_opportunitie.Text = Convert.ToString(objEntidad.opportunities);
            lbx_Rounds.Text = Convert.ToString(objEntidad.numRounds);
            lbx_time2Answer.Text = Convert.ToString(objEntidad.time2Answer);
            lab_User.Text = "Usuario: " + E_Usuario.Nombreusuario;
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
            lbx_Rounds.Font = new Font(lbx_Rounds.Font.Name, 10.2f, lbx_Rounds.Font.Style, lbx_Rounds.Font.Unit);
        }

        private void lbx_time2Answer_Leave(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_time2Answer.Font = new Font(lbx_time2Answer.Font.Name, 10.2f, lbx_time2Answer.Font.Style, lbx_time2Answer.Font.Unit);
        }

        private void lbx_Dificuldad_Setting_Leave(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_Dificuldad_Setting.Font = new Font(lbx_Dificuldad_Setting.Font.Name, 10.2f, lbx_Dificuldad_Setting.Font.Style, lbx_Dificuldad_Setting.Font.Unit);
        }

        private void lbx_preguntas_Leave(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_preguntas.Font = new Font(lbx_preguntas.Font.Name, 10.2f, lbx_preguntas.Font.Style, lbx_preguntas.Font.Unit);
        }

        private void lbx_opportunitie_Leave(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_opportunitie.Font = new Font(lbx_opportunitie.Font.Name, 10.2f, lbx_opportunitie.Font.Style, lbx_opportunitie.Font.Unit);
        }

        private void lbx_Rounds_Enter(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_Rounds.Font = new Font(lbx_Rounds.Font.Name, 23f, lbx_Rounds.Font.Style, lbx_Rounds.Font.Unit);
        }

        private void lbx_time2Answer_Enter(object sender, EventArgs e)
        {
            lbx_time2Answer.Font = new Font(lbx_time2Answer.Font.Name, 23f, lbx_time2Answer.Font.Style, lbx_time2Answer.Font.Unit);
            lbx_time2Answer.Focus();
        }

        private void lbx_Dificuldad_Setting_Enter(object sender, EventArgs e)
        {
            lbx_Dificuldad_Setting.Font = new Font(lbx_Dificuldad_Setting.Font.Name, 23f, lbx_Dificuldad_Setting.Font.Style, lbx_Dificuldad_Setting.Font.Unit);
        }

        private void lbx_preguntas_Enter(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_preguntas.Font = new Font(lbx_preguntas.Font.Name, 23, lbx_preguntas.Font.Style, lbx_preguntas.Font.Unit);
        }

        private void lbx_opportunitie_Enter(object sender, EventArgs e)
        {
            //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
            lbx_opportunitie.Font = new Font(lbx_opportunitie.Font.Name, 23, lbx_opportunitie.Font.Style, lbx_opportunitie.Font.Unit);
        }





        private void lbx_Rounds_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                btn_goBack.PerformClick();
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
                btn_goBack.PerformClick();
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
                btn_goBack.PerformClick();
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

                P_Login login = new P_Login(objEntidad);
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
            P_Usuario usuario = new P_Usuario(true, objEntidad);
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
            lab_User.Text = "Usuario: " + E_Usuario.Nombreusuario;
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            Change_Settings();
            // solo se guardaran los settings si es el admin
            if (E_Usuario.Rol == "Admin")
            {
                Save_SettingsInDatabase();
            }

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

                    PDebateMain = new P_DUO_Main(objEntidad);

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
            objEntidad.catNuevoAntiguoChecked = cbx_categoriaXTestamento.Checked;
            // tomar en cuenta solo si esta checked
            if (objEntidad.catNuevoAntiguoChecked == true)
            {
                objEntidad.catNuevoAntiguo = lbx_catNuevoAntiguo.Text;
            }
            else
            {
                objEntidad.catNuevoAntiguo = "";
            }
            objEntidad.catEvangelios_yOtros = SeleccionCategoria.AlmacenarSeleccionCategorías(lbx_catEvangelios_yOtros);
            objEntidad.catEvangelios_yOtrosChecked = cbx_categoriaXTipoLibro.Checked;
            objEntidad.catLibro = SeleccionCategoria.AlmacenarSeleccionCategorías(lbx_catLibro);
            objEntidad.catLibroChecked = cbx_categoriaXLibro.Checked;
            objEntidad.numRounds = Convert.ToInt32(lbx_Rounds.Text);
            objEntidad.time2Answer = Convert.ToInt32(lbx_time2Answer.Text);
            objEntidad.rebound = cbx_rebote.Checked;

            GetCategories2Show(); // arma el string con las diferentes categorías a mostrar.


            if (objEntidad.opportunitiesBoolean == false)
            {
                // las oportunidades son igual a la cantidad de preguntas
                objEntidad.opportunities = Convert.ToInt32(lbx_preguntas.Text);
            }
        }

        private void Save_SettingsInDatabase()
        {
            //borrar base settings de la base de datos
            objNegoSettingsPROFE.N_sp_GameSettingsPROFE_BorrarTodo();

            objNegoSettingsPROFE.N_InsertarSettingsProfe(objEntidad);
            objNegoSettingsPROFE.N_InsertarSettingLibro(objEntidad);
            objNegoSettingsPROFE.N_InsertarSettingTipoLibro(objEntidad);
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
                    categoria[index] = dt_SettingsPROFE.Rows[index][0].ToString();
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

            foreach (var category in objEntidad.catEvangelios_yOtros)
            {
                objEntidad.categories2Show += category + ", ";
            }

            foreach (var category in objEntidad.catLibro)
            {
                objEntidad.categories2Show += category + ", ";
            }

            objEntidad.categories2Show = objEntidad.categories2Show.TrimEnd();
            objEntidad.categories2Show = objEntidad.categories2Show.TrimEnd(',');
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
                btn_goBack.PerformClick();
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
                btn_goBack.PerformClick();
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
                btn_goBack.PerformClick();
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
            try // si hay alguno seleccionado
            {
                if (lbx_catEvangelios_yOtros.SelectedItem.ToString() != null)
                {
                    lbx_catLibro.ClearSelected();

                    SeleccionCategoria.bloquearDesbloquearDeseleccionarCamposCategoría(true, lbx_catLibro);
                }
                else
                {
                    lbx_catEvangelios_yOtros.SetSelected(0, true);
                }
            }
            catch (Exception)
            {
                if (cbx_categoriaXTipoLibro.Checked == true)
                {
                    lbx_catEvangelios_yOtros.SetSelected(0, true); // evita que se deseleccione
                }
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
                cbx_Opportunities.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Checked_01;
                lbx_opportunitie.Enabled = true;
                objEntidad.opportunitiesBoolean = true;
            }
            else
            {
                cbx_Opportunities.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Unchecked_01;

                if (lbx_preguntas.Text == "Todas") // si está desactivado y preguntas es igual a Todas
                {
                    cbx_Opportunities.Checked = true;
                }
                else // si está desactivado y preguntas es diferente de Todos
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
                cbx_rebote.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Checked_01;
            }
            else
            {
                cbx_rebote.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Unchecked_01;
            }
        }

        private void btn_Aceptar_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Aceptar.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
        }

        private void btn_Aceptar_MouseLeave(object sender, EventArgs e)
        {
            btn_Aceptar.BackgroundImage = Properties.Resources.Boton_Empezar_MouseLeave;
        }

        private void btn_goBack_Click(object sender, EventArgs e)
        {
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

        private void pbx_Sound_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            if (objEntidad.enableButtonSound == true || objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_ON_NEUTRO;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_OFF;
            }
        }

        private void pbx_Sound_MouseLeave(object sender, EventArgs e)
        {
            if (objEntidad.enableButtonSound == true || objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON_NEUTRO;
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

        private void pbx_buttonSound_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            if (objEntidad.enableButtonSound == true)
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseEnter;
            }
            else
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseEnter_OFF;
            }
        }

        private void pbx_buttonSound_MouseLeave(object sender, EventArgs e)
        {
            if (objEntidad.enableButtonSound == true)
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseLeave;
            }
            else
            {
                pbx_buttonSound.BackgroundImage = Properties.Resources.clickSound_MouseLeave_OFF;
            }
        }

        private void pbx_gameSound_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            if (objEntidad.enableGameSound == true)
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseEnter;
            }
            else
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseEnter_OFF;
            }
        }

        private void pbx_gameSound_MouseLeave(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseLeave;
            }
            else
            {
                pbx_gameSound.BackgroundImage = Properties.Resources.GameSound_MouseLeave_OFF;
            }
        }

        private void lbx_catLibro_SelectedIndexChanged(object sender, EventArgs e)
        {
            // seleccionar el primer elemento si se quiere dejar vacio
            try
            {
                if (lbx_catLibro.SelectedItem.ToString() != null)
                { }
            }
            catch (Exception)
            {
                if (cbx_categoriaXLibro.Checked == true)
                {
                    lbx_catLibro.SetSelected(0, true); // evita que se deseleccione
                }
            }
        }


        private void EnableSelectedCategoryGroup(string category)
        {
            if (category == "Testamento")
            {
                //habilitar este tipo de categoria
                lbx_catNuevoAntiguo.Enabled = true;
                lab_NuevoAntiguo.Enabled = true;
                lbx_catNuevoAntiguo.SetSelected(0, true); //selecciona el primer elemento

                // deshabilitar los tipos de categorias no utilizados
                cbx_categoriaXTipoLibro.Checked = false;
                lbx_catEvangelios_yOtros.Enabled = false;
                lab_tipoDeLibro.Enabled = false;
                lbx_catEvangelios_yOtros.ClearSelected();

                cbx_categoriaXLibro.Checked = false;
                lbx_catLibro.Enabled = false;
                lab_libro.Enabled = false;
                lbx_catLibro.ClearSelected();
            }
            else if (category == "Tipo libro")
            {
                //habilitar este tipo de categoria
                lbx_catEvangelios_yOtros.Enabled = true;
                lab_tipoDeLibro.Enabled = true;
                lbx_catEvangelios_yOtros.SetSelected(0, true); //selecciona el primer elemento

                // deshabilitar los tipos de categorias no utilizados
                cbx_categoriaXTestamento.Checked = false;
                lbx_catNuevoAntiguo.Enabled = false;
                lab_NuevoAntiguo.Enabled = false;
                lbx_catNuevoAntiguo.ClearSelected();

                cbx_categoriaXLibro.Checked = false;
                lbx_catLibro.Enabled = false;
                lab_libro.Enabled = false;
                lbx_catLibro.ClearSelected();
            }
            else if (category == "Libro")
            {
                //habilitar este tipo de categoria
                lbx_catLibro.Enabled = true;
                lab_libro.Enabled = true;
                lbx_catLibro.SetSelected(0, true); //selecciona el primer elemento

                // deshabilitar los tipos de categorias no utilizados
                cbx_categoriaXTestamento.Checked = false;
                lbx_catNuevoAntiguo.Enabled = false;
                lab_NuevoAntiguo.Enabled = false;
                lbx_catNuevoAntiguo.ClearSelected();

                cbx_categoriaXTipoLibro.Checked = false;
                lbx_catEvangelios_yOtros.Enabled = false;
                lab_tipoDeLibro.Enabled = false;
                lbx_catEvangelios_yOtros.ClearSelected();
            }
        }

        private void cbx_categoriaXTestamento_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_categoriaXTestamento.Checked == true)
            {
                cbx_categoriaXTestamento.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Checked_01;

                //Habilitar la categoria Testamento
                EnableSelectedCategoryGroup("Testamento");
            }
            else
            {
                //evitar que se queden todos los chekbox deseleccionados
                if (cbx_categoriaXTipoLibro.Checked == false && cbx_categoriaXLibro.Checked == false)
                {
                    cbx_categoriaXTestamento.Checked = true;
                }
                else
                {
                    cbx_categoriaXTestamento.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Unchecked_01;
                }
            }
        }

        private void cbx_categoriaXTipoLibro_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_categoriaXTipoLibro.Checked == true)
            {
                cbx_categoriaXTipoLibro.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Checked_01;
                
                //Habilitar la categoria Tipo de libro
                EnableSelectedCategoryGroup("Tipo libro");
            }
            else
            {
                //evitar que se queden todos los chekbox deseleccionados
                if (cbx_categoriaXTestamento.Checked == false && cbx_categoriaXLibro.Checked == false)
                {
                    cbx_categoriaXTipoLibro.Checked = true;
                }
                else
                {
                    cbx_categoriaXTipoLibro.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Unchecked_01;
                }
            }
        }

        private void cbx_categoriaXLibro_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_categoriaXLibro.Checked == true)
            {
                cbx_categoriaXLibro.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Checked_01;

                //Habilitar la categoria Libro
                EnableSelectedCategoryGroup("Libro");
            }
            else
            {
                //evitar que se queden todos los chekbox deseleccionados
                if (cbx_categoriaXTestamento.Checked == false && cbx_categoriaXTipoLibro.Checked == false)
                {
                    cbx_categoriaXLibro.Checked = true;
                }
                else
                {
                    cbx_categoriaXLibro.BackgroundImage = Properties.Resources.Focused_bible_CONFIGURACIÓN_Unchecked_01;
                }
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
        }

        private void btn_deleteConnection_Click(object sender, EventArgs e)
        {
            DialogResult borrarBDE;

            borrarBDE = MessageBox.Show("Seguro que desea BORRAR la CONEXION a la BASE DE DATOS EXTERNA?", "Advertencia", MessageBoxButtons.YesNo);
            E_ConnectionString.remoteHostName = null;
            E_ConnectionString.remoteUserName = null;
            E_ConnectionString.remotePassword = null;

            if (borrarBDE == DialogResult.Yes)
            {
                login.DeleteRemoteCredentials();

                E_ConnectionString.conectionString = @"server=.;Integrated Security=yes;Database=focusedBible";
                objNegoSettingsPROFE = new N_SettingsPROFE();

                MessageBox.Show("Datos de conexión remota BORRADOS correctamente!");
            }
        }
    }
}
