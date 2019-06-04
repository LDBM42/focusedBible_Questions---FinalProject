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
        public P_Usuario()
        {
            InitializeComponent();
        }

        int countDownTimer;
        public int reOpened;
        int x = 525, xP = 368;
        string genero = "M";
        D_Usuario usuario = new D_Usuario();


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void mostrarOcultar(bool t)
        {
            pbx_logo.Visible = !t;

        }


        private void P_Usuario_Load(object sender, EventArgs e)
        {
            pbx_logo.Image = Properties.Resources.focusedBible_Questions;

            tmr_cuadroBlanco.Start();
           
            countDownTimer = 300;
            timer1.Start();
        }



        public string validarDatos()
        {
            //Funcion para validar los datos e indicarle al usuario si estos fueron completados 
            string resultado = "";
            if (text_Usuario.Text == "") resultado += "El campo: Usuario,\n";
            if (text_Password.Text == "") resultado += "El campo: Contraseña,\n";
            if (text_Usuario.Text == "Usuario") resultado += "El campo: Usuario,\n";
            if (text_Password.Text == "Contraseña") resultado += "El campo: Contraseña,\n";

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
            if (text_Usuario.Text == "Usuario")
            {
                text_Usuario.Text = "";
                //text_Usuario.ForeColor = Color.LightGray;
            }
        }

        private void text_Usuario_MouseLeave(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "" && text_Usuario.Focused == false)
            {
                text_Usuario.Text = "Usuario";
                text_Usuario.ForeColor = Color.DimGray;
            }
        }

        private void text_Password_MouseEnter(object sender, EventArgs e)
        {
            if (text_Password.Text == "Contraseña")
            {
                text_Password.Text = "";
                //text_Password.ForeColor = Color.LightGray;
                text_Password.UseSystemPasswordChar = true;
            }
        }

        private void text_Password_MouseLeave(object sender, EventArgs e)
        {
            if (text_Password.Text == "" && text_Password.Focused == false)
            {
                text_Password.Text = "Contraseña";
                text_Password.ForeColor = Color.DimGray;
                text_Password.UseSystemPasswordChar = false;
            }
        }

        private void text_Usuario_Click(object sender, EventArgs e)
        {
            if (text_Password.Text == "")
            {
                text_Password.Text = "Contraseña";
                text_Password.ForeColor = Color.DimGray;
                text_Password.UseSystemPasswordChar = false;
            }
        }

        private void text_Password_Click(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "")
            {
                text_Usuario.Text = "Usuario";
                text_Usuario.ForeColor = Color.DimGray;
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


        
        //evitar que se presionen las teclas de flechas al estar seleccionado el texto USUARIO o CONTRASEÑA
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // detecta si el campo usuario esta enfocado y si dice Usuario. Hace lo mismo con el de contraseña.
            if ((text_Usuario.Text == "Usuario" && text_Usuario.Focused == true) ||
                (text_Password.Text == "Contraseña" && text_Password.Focused == true))
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
                text_Password.Text = "Contraseña";
                text_Password.ForeColor = Color.DimGray;
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

                #region Cambios segun genero y nombre
                //Selecciona el genero según genero del usuario
                DataTable genero = usuario.UsuarioGenero(text_Usuario.Text);

                if (genero.Rows[0]["Genero"].ToString() == "M") // si es masculino
                {
                    rb_Masculino.Checked = true;

                    pbx_Usuario.Image = Properties.Resources.usuario_masculino;

                    rb_Masculino.Enabled = true;
                    rb_Femenino.Enabled = true;
                    cb_Rol.Enabled = true;
                }
                else if (genero.Rows[0]["Genero"].ToString() == "F") // si es femenino
                {
                    rb_Femenino.Checked = true;

                    pbx_Usuario.Image = Properties.Resources.usuario_femenino;

                    rb_Masculino.Enabled = true;
                    rb_Femenino.Enabled = true;
                    cb_Rol.Enabled = true;
                }

                else if (genero.Rows[0]["Genero"].ToString() == "Admin") // si es administrador
                {                  
                    rb_Femenino.Checked = false;
                    rb_Masculino.Checked = false;
                    rb_Masculino.Enabled = false;
                    rb_Femenino.Enabled = false;

                    cb_Rol.SelectedItem = "Admin";
                    cb_Rol.Enabled = false;

                    pbx_Usuario.Image = Properties.Resources.Administrador;
                }
                #endregion
                
            }
            else
            {
                if (text_Usuario.Text == "Usuario" || text_Usuario.Text == "") // si es nuevo
                {
                    rb_Masculino.Enabled = true;
                    rb_Femenino.Enabled = true;

                    cb_Rol.SelectedItem = "Usuario";
                    cb_Rol.Enabled = true;

                    rb_Masculino.Checked = true;

                    pbx_Usuario.Image = Properties.Resources.Usuario;
                }
                else if (rb_Masculino.Checked == true) // si es masculino
                {
                    pbx_Usuario.Image = Properties.Resources.usuario_masculino;
                }
                else // si es femenino
                {
                    pbx_Usuario.Image = Properties.Resources.usuario_femenino;
                }
                
                btn_Crear.Text = "CREAR"; //Cambiar botón de Actualizar a crear
            }

                if (text_Usuario.Text == "")
            {
                text_Usuario.Text = "Usuario";
                text_Usuario.ForeColor = Color.DimGray;
                text_Password.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            this.Hide();
            settings.Show();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }



        private void tmr_cuadroBlanco_Tick(object sender, EventArgs e)
        {
            if (x >= 250)
            {
                pnl_White.Size = new Size(x, 330);
                x = x - 25;

                pbx_logo.Location = new Point(xP, 40);
                xP = xP - 29;
            }
            else
            {
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
            Pen pen = new Pen(Color.FromArgb(255, 40, 40, 40), 2);
            e.Graphics.DrawLine(pen, 312, 215, 719, 215);

            Pen pen2 = new Pen(Color.FromArgb(255, 40, 40, 40), 2);
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

                        if (usuario.Actualizar(text_Usuario.Text, text_Password.Text, "Admin", genero) == 1)
                        {
                            MessageBox.Show("Datos Actualizados Correctamente");
                            P_Usuario_Load(null, null);
                        }
                    }
                    else //Nuevo registro
                    {
                        if (usuario.Insertar(text_Usuario.Text, text_Password.Text, "Admin", genero) > 0)
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
