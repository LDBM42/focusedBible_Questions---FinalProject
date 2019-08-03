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
    public partial class P_SetDataBaseAutentication : Form
    {
        public P_SetDataBaseAutentication(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;
            InitializeComponent();
        }

        int countDownTimer;
        int x = 525;
        E_focusedBible objEntidad = new E_focusedBible();
        D_Login login = new D_Login();

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


            tmr_cuadroBlanco.Start();
           
            countDownTimer = 300;


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
            if (text_hostName.Text == "") resultado += "El campo: Nombre de equipo,\n";
            if (text_Usuario.Text == "") resultado += "El campo: Usuario,\n";
            if (text_Password.Text == "") resultado += "El campo: Contraseña,\n";
            if (text_Usuario.Text == "USUARIO") resultado += "El campo: Usuario,\n";
            if (text_Password.Text == "CONTRASEÑA") resultado += "El campo: Contraseña,\n";
            if (text_hostName.Text == "NOMBRE DE EQUIPO") resultado += "El campo: Nombre de equipo,\n";

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
            if (text_hostName.Text == "")
            {
                text_hostName.Text = "NOMBRE DE EQUIPO";
                text_hostName.ForeColor = Color.FromArgb(32, 52, 61);
            }

            if (text_Password.Text == "")
            {
                text_Password.Text = "CONTRASEÑA";
                text_Password.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.UseSystemPasswordChar = false;
            }
        }

        private void text_Password_Click(object sender, EventArgs e)
        {
            if (text_hostName.Text == "")
            {
                text_hostName.Text = "NOMBRE DE EQUIPO";
                text_hostName.ForeColor = Color.FromArgb(32, 52, 61);
            }

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
                btn_Guardar_Click(null, null);
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
            if (text_hostName.Text == "")
            {
                text_hostName.Text = "NOMBRE DE EQUIPO";
                text_hostName.ForeColor = Color.FromArgb(32, 52, 61);
                text_Usuario.Focus();
            }

            if (text_Password.Text == "")
            {
                text_Password.Text = "CONTRASEÑA";
                text_Password.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.UseSystemPasswordChar = false;
                text_Usuario.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

            this.Hide();

            if (existe != null) // Si se accedio desde la ventana prin al menu usuario
            {
                existe.Show();
                existe.BringToFront();
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
        
        private void P_Usuario_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.FromArgb(255, 32, 52, 61), 2);
            e.Graphics.DrawLine(pen1, 312, 150, 719, 150);

            Pen pen2 = new Pen(Color.FromArgb(255, 32, 52, 61), 2);
            e.Graphics.DrawLine(pen2, 312, 215, 719, 215);

            Pen pen3 = new Pen(Color.FromArgb(255, 32, 52, 61), 2);
            e.Graphics.DrawLine(pen3, 312, 280, 719, 280);
        }

        private void btn_Guardar_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Crear.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
            btn_Crear.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_Guardar_MouseLeave(object sender, EventArgs e)
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

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = validarDatos();
                if (sResultado == "") // no faltan datos
                {
                    login.SetRemoteCredentials(text_hostName.Text,
                    text_Usuario.Text, text_Password.Text);

                    E_ConnectionString.remoteHostName = text_hostName.Text;
                    E_ConnectionString.remoteUserName = text_Usuario.Text;
                    E_ConnectionString.remotePassword = text_Password.Text;

                    this.DialogResult = DialogResult.OK;

                    text_hostName.Text = "NOMBRE DE EQUIPO";
                    text_Usuario.Text = "USUARIO";
                    text_Password.Text = "CONTRASEÑA";
                    text_Password.UseSystemPasswordChar = false;
                    text_hostName.Focus();
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

        private void text_hostName_Click(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "")
            {
                text_Usuario.Text = "USUARIO";
                text_Usuario.ForeColor = Color.FromArgb(32, 52, 61);
            }

            if (text_Password.Text == "")
            {
                text_Password.Text = "CONTRASEÑA";
                text_Password.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.UseSystemPasswordChar = false;
            }
        }

        private void text_hostName_Enter(object sender, EventArgs e)
        {
            if (text_Password.Text == "")
            {
                text_Password.Text = "CONTRASEÑA";
                text_Password.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.UseSystemPasswordChar = false;
                text_hostName.Focus();
            }
        }

        private void text_hostName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }

        private void text_hostName_MouseEnter(object sender, EventArgs e)
        {
            if (text_hostName.Text == "NOMBRE DE EQUIPO")
            {
                text_hostName.Text = "";
            }
        }

        private void text_hostName_MouseLeave(object sender, EventArgs e)
        {
            if (text_hostName.Text == "" && text_hostName.Focused == false)
            {
                text_hostName.Text = "NOMBRE DE EQUIPO";
                text_hostName.ForeColor = Color.FromArgb(32, 52, 61);
            }
        }
    }
}
