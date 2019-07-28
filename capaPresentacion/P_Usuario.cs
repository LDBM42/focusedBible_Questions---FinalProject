using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using capaDatos;
using capaEntidad;

namespace capaPresentacion
{
    public partial class P_Usuario : Form
    {
        public P_Usuario(bool rol, E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;
            this.rol = rol; // para activar o desactivar el rol

            InitializeComponent();
        }

        bool rol = true;
        int countDownTimer;
        public int reOpened;
        int x = 525;
        string genero = "M";
        D_Usuario usuario = new D_Usuario();
        E_focusedBible objEntidad = new E_focusedBible();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


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

        private void P_Usuario_Load(object sender, EventArgs e)
        {
            pnl_fondoLogo.BackgroundImage = Properties.Resources.Focused_bible_landing_01;
            pnl_fondoLogo.BackgroundImageLayout = ImageLayout.None;

            // activar o desactivar la seleccion del rol
            if (rol)
            {
                cbx_Rol.Enabled = true;
            }
            else
            {
                cbx_Rol.Enabled = false;
            }

            tmr_cuadroBlanco.Start();
           
            countDownTimer = 300;
            timer1.Start();


            if (objEntidad.enableButtonSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }
        }



        public string validarDatos()
        {
            //Funcion para validar los datos e indicarle al usuario si estos fueron completados 
            string resultado = "";
            if (text_Usuario.Text == "") resultado += "El campo: Usuario,\n";
            if (text_Password.Text == "") resultado += "El campo: Contraseña,\n";
            if (text_Usuario.Text == "USUARIO") resultado += "El campo: Usuario,\n";
            if (text_Password.Text == "CONTRASEÑA") resultado += "El campo: Contraseña,\n";

            return resultado;
        }

        

        #region Mover Formulario
        
        private void P_Usuario_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pnl_White_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pbx_logo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion



        private void text_Password_TextChanged(object sender, EventArgs e)
        {
            text_Password.UseSystemPasswordChar = true;
        }

        private void text_Usuario_MouseEnter(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "USUARIO")
            {
                text_Usuario.Text = "";
            }
        }

        private void text_Usuario_MouseLeave(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "" && text_Usuario.Focused == false)
            {
                text_Usuario.Text = "USUARIO";
                text_Usuario.ForeColor = Color.FromArgb(32, 52, 61);
            }
        }

        private void text_Password_MouseEnter(object sender, EventArgs e)
        {
            if (text_Password.Text == "CONTRASEÑA")
            {
                text_Password.Text = "";
                text_Password.UseSystemPasswordChar = true;
            }
        }

        private void text_Password_MouseLeave(object sender, EventArgs e)
        {
            if (text_Password.Text == "" && text_Password.Focused == false)
            {
                text_Password.Text = "CONTRASEÑA";
                text_Password.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.UseSystemPasswordChar = false;
            }
        }

        private void text_Usuario_Click(object sender, EventArgs e)
        {
            if (text_Password.Text == "")
            {
                text_Password.Text = "CONTRASEÑA";
                text_Password.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.UseSystemPasswordChar = false;
            }
        }

        private void text_Password_Click(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "")
            {
                text_Usuario.Text = "USUARIO";
                text_Usuario.ForeColor = Color.FromArgb(32, 52, 61);
            }
        }

        private void text_Usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                btn_Crear_Click(null, null);
            }
        }



        //evitar que se presionen las teclas de flechas al estar seleccionado el texto USUARIO o contraseña
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // detecta si el campo usuario esta enfocado y si dice Usuario. Hace lo mismo con el de contraseña.
            if ((text_Usuario.Text == "USUARIO" && text_Usuario.Focused == true) ||
                (text_Password.Text == "CONTRASEÑA" && text_Password.Focused == true))
            {
                //captura la tecla flecha arriba
                if (keyData == Keys.Up)
                {
                    return true;
                }
                //captura la tecla flecha abajo
                if (keyData == Keys.Down)
                {
                    return true;
                }
                //captura la tecla flecha izquierda
                if (keyData == Keys.Left)
                {
                    return true;
                }
                //captura la tecla flecha derecha
                if (keyData == Keys.Right)
                {
                    return true;
                }
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void text_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void text_Usuario_Enter(object sender, EventArgs e)
        {
            if (text_Password.Text == "")
            {
                text_Password.Text = "CONTRASEÑA";
                text_Password.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.UseSystemPasswordChar = false;
                text_Usuario.Focus();
            }
        }

        private void text_Password_Enter(object sender, EventArgs e)
        {

            if (usuario.ExistUser(text_Usuario.Text) > 0) //Si el usuario existe
            {

                //Cambiar botón de crear a Actualizar
                btn_Crear.Text = "ACTUALIZAR";

                #region Cambios segun genero, rol y nombre
                //Selecciona el genero según genero del usuario
                DataTable generoYTipo = usuario.UsuarioGeneroYTipo(text_Usuario.Text);

                //Seleccionar rol según rol del usuario registrado
                cbx_Rol.Text = generoYTipo.Rows[0]["Tipo"].ToString();

                    if (generoYTipo.Rows[0]["Genero"].ToString() == "M") // si es masculino
                {
                    rb_Masculino.Checked = true;

                    pbx_Usuario.Image = Properties.Resources.usuario_masculino;

                    rb_Masculino.Enabled = true;
                    rb_Femenino.Enabled = true;
                }
                else if (generoYTipo.Rows[0]["Genero"].ToString() == "F") // si es femenino
                {
                    rb_Femenino.Checked = true;

                    pbx_Usuario.Image = Properties.Resources.usuario_femenino;

                    rb_Masculino.Enabled = true;
                    rb_Femenino.Enabled = true;
                }

                else if (generoYTipo.Rows[0]["Genero"].ToString() == "Admin") // si es administrador
                {   
                    rb_Femenino.Checked = false;
                    rb_Masculino.Checked = false;
                    rb_Masculino.Enabled = false;
                    rb_Femenino.Enabled = false;

                    cbx_Rol.SelectedItem = "Admin";

                    pbx_Usuario.Image = Properties.Resources.Administrador;
                }
                #endregion
                
            }
            else
            {
                if (text_Usuario.Text == "USUARIO" || text_Usuario.Text == "") // si es nuevo
                {
                    rb_Masculino.Enabled = true;
                    rb_Femenino.Enabled = true;

                    cbx_Rol.SelectedItem = "Estudiante";

                    rb_Masculino.Checked = true;

                    pbx_Usuario.Image = Properties.Resources.Usuario;
                }
                else if (rb_Masculino.Checked == true) // si es masculino
                {
                    pbx_Usuario.Image = Properties.Resources.usuario_masculino;
                }
                else if (rb_Femenino.Checked == true)// si es femenino
                {
                    pbx_Usuario.Image = Properties.Resources.usuario_femenino;
                }
                else
                {
                    cbx_Rol.SelectedItem = "Estudiante";
                    rb_Femenino.Checked = false;
                    rb_Masculino.Checked = false;
                    rb_Masculino.Enabled = true;
                    rb_Femenino.Enabled = true;
                }
                
                btn_Crear.Text = "CREAR"; //Cambiar botón de Actualizar a crear
            }

                if (text_Usuario.Text == "")
            {
                text_Usuario.Text = "USUARIO";
                text_Usuario.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Login").SingleOrDefault<Form>();
            Form existe2 = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Configuracion").SingleOrDefault<Form>();


            this.Hide();

            if (existe != null) // Si se accedio desde la ventana login al menu usuario
            {
                existe.Show();
            }
            if (existe2 != null) // Si se accedio desde la ventana login al menu usuario
            {
                existe2.Show();
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



        private void tmr_cuadroBlanco_Tick(object sender, EventArgs e)
        {
            if (x >= 250)
            {
                pnl_fondoLogo.Size = new Size(x, 330);
                x = x - 25;
            }
            else
            {
                pnl_fondoLogo.BackgroundImage = Properties.Resources.Focused_bible_FondoLogin_NewUser;
                pnl_fondoLogo.BackgroundImageLayout = ImageLayout.Stretch;
                tmr_cuadroBlanco.Stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (countDownTimer != 0)
            {
                lbl_CountDown.Text = Convert.ToString(countDownTimer);
                countDownTimer--;

                if (!this.Visible)
                {
                    timer1.Stop();
                }
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Excedio el máximo de tiempo de espera, por motivo de seguridad el programa sera cerrado", "Tiempo Agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        
        private void P_Usuario_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 32, 52, 61), 2);
            e.Graphics.DrawLine(pen, 312, 215, 719, 215);

            Pen pen2 = new Pen(Color.FromArgb(255, 32, 52, 61), 2);
            e.Graphics.DrawLine(pen2, 312, 280, 719, 280);
        }

        private void rb_Masculino_CheckedChanged(object sender, EventArgs e)
        {
            pbx_Usuario.Image = Properties.Resources.usuario_masculino;
        }

        private void rb_Femenino_CheckedChanged(object sender, EventArgs e)
        {
            pbx_Usuario.Image = Properties.Resources.usuario_femenino;
        }

        private void btn_Crear_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Crear.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
            btn_Crear.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_Crear_MouseLeave(object sender, EventArgs e)
        {
            btn_Crear.BackgroundImage = Properties.Resources.Boton_Empezar_MouseLeave;
        }

        private void btnMinimize_MouseEnter(object sender, EventArgs e)
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

        private void btnMinimize_MouseLeave(object sender, EventArgs e)
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

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btnClose.BackgroundImage = Properties.Resources.Focused_bible_landing_Cerrar_MOUSE_ENTER;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.Focused_bible_landing_Cerrar;
        }

        private void btn_Crear_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = validarDatos();
                if (sResultado == "") // no faltan datos
                {
                    //saber si es masculino o femenino
                    if (rb_Masculino.Checked == true)
                    {
                        genero = rb_Masculino.Text;
                    }
                    else if(rb_Femenino.Checked == true)
                    {
                        genero = rb_Femenino.Text;
                    }
                    else
                    {
                        genero = "Admin";
                    }

                    if (usuario.ExistUser(text_Usuario.Text) > 0) //Actualizar registro
                    {

                        if (usuario.Actualizar(text_Usuario.Text, text_Password.Text, cbx_Rol.SelectedItem.ToString(), genero) == 1)
                        {
                            MessageBox.Show("Datos Actualizados Correctamente");
                            P_Usuario_Load(null, null);
                        }
                    }
                    else //Nuevo registro
                    {
                        if (usuario.Insertar(text_Usuario.Text, text_Password.Text, cbx_Rol.SelectedItem.ToString(), genero) > 0)
                        {
                            MessageBox.Show("Usuario " + text_Usuario.Text +  " Agregado Correctamente");
                            P_Usuario_Load(null, null);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos en: \n" + sResultado);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Algo está mal, Favor llenar correctamente todos los campos e intentarlo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
