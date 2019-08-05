using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using capaDatos;
using capaEntidad;

namespace capaPresentacion
{
    public partial class P_Login : Form
    {
        public P_Login(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;
            InitializeComponent();
        }

        int countDownTimer;
        public int reOpened;
        int x = 525;
        P_Main PMain;
        D_Login login = new D_Login();
        D_Usuario usuario = new D_Usuario();
        E_focusedBible objEntidad = new E_focusedBible();
        DataTable dt;
        string user;
        string password;


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


        private void P_Login_Load(object sender, EventArgs e)
        {
            pnl_fondoLogo.BackgroundImage = Properties.Resources.Focused_bible_landing_01;
            pnl_fondoLogo.BackgroundImageLayout = ImageLayout.None;

            timerFondoLogo.Start();
            
            DataSet ds = login.AutoLoginGetLocal(); //base de datos local

            dt = new DataTable();
            if (reOpened == 0)
            {
                dt = ds.Tables[0]; //base de datos remota
            }


            if (dt.Rows.Count > 0)
            {
                E_Usuario.Logged = 1; //para que no se desactive el autologin ya activado anteriormente
                user = dt.Rows[0]["Usuario"].ToString();
                password = dt.Rows[0]["Contraseña"].ToString();
            }


            if (objEntidad.enableButtonSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
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

        private void timerFondoLogo_Tick(object sender, EventArgs e)
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
                timerFondoLogo.Stop();

                if (reOpened == 0 && dt.Rows.Count > 0) // si es la primera vez que se entra
                {
                    countDownTimer = 1;
                    text_Usuario.Text = user[0].ToString(); //escribir la primera letra del User Name
                    timerUser.Start(); // para esperar a que cargue la ventana y realizar el autologin
                }
            }
        }

        private void timerUser_Tick(object sender, EventArgs e)
        {
            if (countDownTimer != dt.Rows[0]["Usuario"].ToString().Count())
            {
                text_Usuario.Text += user[countDownTimer];
                countDownTimer++;
            }
            else
            {
                timerUser.Stop();
                timerPassword.Start();
                countDownTimer = 1;
                text_Password.Text = password[0].ToString(); //escribir la primera letra del Password
            }
        }


        private void timerPassword_Tick(object sender, EventArgs e)
        {
            if (countDownTimer != dt.Rows[0]["Contraseña"].ToString().Count())
            {
                text_Password.Text += password[countDownTimer];
                countDownTimer++;
            }
            else
            {
                btnEntrar.PerformClick();
                reOpened = 1; // para solo entra automaticamente la primera vez que se entra al juego
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            DataSet ds = login.ValidarLogin(text_Usuario.Text, text_Password.Text);
            DataTable dt = ds.Tables[0];


            if (dt.Rows.Count > 0)
            {
                E_Usuario.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                E_Usuario.Nombreusuario = dt.Rows[0]["Usuario"].ToString();
                E_Usuario.Password = dt.Rows[0]["Password"].ToString();
                E_Usuario.Rol = dt.Rows[0]["Tipo"].ToString();



                if (cbx_autoLogin.Checked == true) // verificar si está activado el autologin
                {
                    E_Usuario.Logged = 1; // para activar autologgin
                }

                //Guardar Datos Autologgin
                if (login.AutoLoginSetLocal(E_Usuario.Nombreusuario, E_Usuario.Logged) == 1)
                {
                    // para saber si el formulario existe, o sea si está abierto o cerrado
                    Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

                    CerrarYVolverAAbrirMain(existe); // cierra login y abre la principal
                }  
            }
            else
            {
                MessageBox.Show("Usuario y/o contraseña incorrectos");
                text_Password.Text = "";
            }
        }

        private void CerrarYVolverAAbrirMain(Form existe)
        {
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


        private void P_Login_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 32, 52, 61), 2);
            e.Graphics.DrawLine(pen, 312, 215, 719, 215);

            Pen pen2 = new Pen(Color.FromArgb(255, 32, 52, 61), 2);
            e.Graphics.DrawLine(pen2, 312, 280, 719, 280);
        }

        #region Mover Formulario
        private void P_Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pnl_Azul_MouseDown(object sender, MouseEventArgs e)
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
                btnEntrar_Click(null, null);
            }
        }

        private void text_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                btnEntrar_Click(null, null);
                //SendKeys.Send("{TAB}"); //hace que se presione la tecla TAB por código
            }
        }


        //evitar que se presionen las teclas de flechas al estar seleccionado el texto USUARIO o CONTRASEÑA
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
                #region Cambios segun genero y nombre
                //Selecciona el genero según genero del usuario
                DataTable genero = usuario.UsuarioGeneroYTipo(text_Usuario.Text);

                if (genero.Rows[0]["Genero"].ToString() == "M") // si es masculino
                {
                    pbx_Usuario.Image = Properties.Resources.usuario_masculino;
                }
                else if (genero.Rows[0]["Genero"].ToString() == "F") // si es femenino
                {
                    pbx_Usuario.Image = Properties.Resources.usuario_femenino;
                }

                else if (genero.Rows[0]["Genero"].ToString() == "Admin") // si es administrador
                {
                    pbx_Usuario.Image = Properties.Resources.Administrador;
                }
                #endregion
            }
            else
            {
                if (text_Usuario.Text == "USUARIO" || text_Usuario.Text == "") // si es nuevo
                {
                    pbx_Usuario.Image = Properties.Resources.Usuario;
                }
            }

            if (text_Usuario.Text == "")
            {
                text_Usuario.Text = "USUARIO";
                text_Usuario.ForeColor = Color.FromArgb(32, 52, 61);
                text_Password.Focus();
            }
        }

        private void llab_nuevoUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            P_Usuario usuario = new P_Usuario(false, objEntidad);
            usuario.Show();

            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();
            CerrarYVolverAAbrirMain(existe); // cierra login y abre la principal
        }

        private void btnEntrar_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btnEntrar.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
            btnEntrar.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnEntrar_MouseLeave(object sender, EventArgs e)
        {
            btnEntrar.BackgroundImage = Properties.Resources.Boton_Empezar_MouseLeave;
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

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btnClose.BackgroundImage = Properties.Resources.Focused_bible_landing_Cerrar_MOUSE_ENTER;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.Focused_bible_landing_Cerrar;
        }

        private void P_Login_Activated(object sender, EventArgs e)
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
    }
}
