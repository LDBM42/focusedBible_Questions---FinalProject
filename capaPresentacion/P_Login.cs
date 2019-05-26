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
    public partial class P_Login : Form
    {
        public P_Login()
        {             
            InitializeComponent();
        }
        int countDownTimer;
        public int reOpened;
        int x = 525, xP = 368;
        Settings Principal;
        D_Login login = new D_Login();


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void mostrarOcultar(bool t)
        {
            pbx_logo.Visible = !t;

        }
        
        private void P_Login_Load(object sender, EventArgs e)
        {
            pbx_logo.Image = Properties.Resources.focusedBible_Questions;

            tmr_cuadroAzul.Start();
            //this.Opacity = 0.95;
                        
            DataSet ds = login.AutoLoginGet();
            DataTable dt = ds.Tables[0];


            if (dt.Rows.Count > 0)
            {
                text_Usuario.Text = dt.Rows[0]["Usuario"].ToString();
                text_Password.Text = dt.Rows[0]["Contraseña"].ToString();

                btnEntrar.PerformClick();
            }
            else
            {
                countDownTimer = 300;
                timer1.Start();
            }
        }
        


        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void tmr_cuadroAzul_Tick(object sender, EventArgs e)
        {
            if (x >= 250)
            {
                pnl_Azul.Size = new Size(x, 330);
                x = x - 25;

                pbx_logo.Location = new Point(xP, 42);
                xP = xP - 29;
            }
            else
            {
                tmr_cuadroAzul.Stop();
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

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            DataSet ds = login.ValidarLogin(text_Usuario.Text, text_Password.Text);
            DataTable dt = ds.Tables[0];


            if (dt.Rows.Count > 0)
            {
                E_Usuario.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                E_Usuario.Nombreusuario = dt.Rows[0]["Usuario"].ToString();
                E_Usuario.Password = dt.Rows[0]["Password"].ToString();
                E_Usuario.Logged = 1; // para activar autologgin

                //Guardar Datos Autologgin
                if (login.AutoLoginSet(E_Usuario.Nombreusuario, E_Usuario.Password, E_Usuario.Logged) == 1)
                {
                    if (reOpened > 0)
                    {
                        try
                        {
                            // para saber si el formulario existe, o sea si está abierto o cerrado
                            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "Settings").SingleOrDefault<Form>();

                            existe.Refresh();

                            if (existe != null)
                            {
                                this.Close();
                                //Principal = new Settings();
                                //Principal.ShowDialog();
                                existe.Show();
                            }
                        }
                        catch (Exception)
                        {
                            Principal = new Settings();
                            Principal.ShowDialog();
                        }

                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK; //terminar loggin y abrir Principal
                    }
                }
            }
            else
            {

                MessageBox.Show("Usuario y/o contraseña incorrectos");
                text_Password.Text = "";
                pbx_logo.Visible = true;

                mostrarOcultar(false);

            }
        }

        private void P_Login_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 40, 40, 40), 2);
            e.Graphics.DrawLine(pen, 312, 215, 719, 215);

            Pen pen2 = new Pen(Color.FromArgb(255, 40, 40, 40), 2);
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
                //text_Usuario.ForeColor = Color.LightGray;
            }
        }

        private void text_Usuario_MouseLeave(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "" && text_Usuario.Focused == false)
            {
                text_Usuario.Text = "USUARIO";
                text_Usuario.ForeColor = Color.DimGray;
            }
        }

        private void text_Password_MouseEnter(object sender, EventArgs e)
        {
            if (text_Password.Text == "PASSWORD")
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
                text_Password.Text = "PASSWORD";
                text_Password.ForeColor = Color.DimGray;
                text_Password.UseSystemPasswordChar = false;
            }
        }

        private void text_Usuario_Click(object sender, EventArgs e)
        {
            if (text_Password.Text == "")
            {
                text_Password.Text = "PASSWORD";
                text_Password.ForeColor = Color.DimGray;
                text_Password.UseSystemPasswordChar = false;
            }
        }

        private void text_Password_Click(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "")
            {
                text_Usuario.Text = "USUARIO";
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

        private void text_Usuario_Enter(object sender, EventArgs e)
        {
            if (text_Password.Text == "")
            {
                text_Password.Text = "PASSWORD";
                text_Password.ForeColor = Color.DimGray;
                text_Password.UseSystemPasswordChar = false;
                text_Usuario.Focus();
            }
        }

        private void text_Password_Enter(object sender, EventArgs e)
        {
            if (text_Usuario.Text == "")
            {
                text_Usuario.Text = "USUARIO";
                text_Usuario.ForeColor = Color.DimGray;
                text_Password.Focus();
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
