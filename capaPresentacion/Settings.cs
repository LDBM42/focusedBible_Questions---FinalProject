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
using capaDatos;

namespace capaPresentacion
{
    public partial class Settings : Form
    {
        public Settings(int numRounds = 1, int time2Answer = 20, string difficulty = "All")
        {
            this.numRounds = numRounds;
            this.time2Answer = time2Answer;
            this.difficulty = difficulty;
            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        D_Login login = new D_Login();
        P_focusedBibles PfocusedB;
        HowToPlay howToPlay;
        string p1_Name;
        string p2_Name;
        public string difficulty;
        public int numRounds;
        public int time2Answer;
        int[] noQuestions;


        private void btn_submit_Click(object sender, EventArgs e)
        {
            Change_Settings();

            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBibles").SingleOrDefault<Form>();

            if (existe != null)
            {
                existe.Close();
            }

            PfocusedB = new P_focusedBibles(p1_Name, p2_Name, numRounds, time2Answer, numRounds, difficulty);
            this.Hide();
            PfocusedB.Show();
        }

        public void Change_Settings()
        {
            difficulty = lbx_Dificuldad_Setting.Text;
            p1_Name = tbx_Player1.Text;
            p2_Name = tbx_Player2.Text;
            numRounds = Convert.ToInt32(lbx_Rounds.Text);
            time2Answer = Convert.ToInt32(lbx_time2Answer.Text);
        }


        void Insertar()
        {
            objEntidad.preg = tbx_Preg.Text;
            objEntidad.a = tbx_a.Text;
            objEntidad.b = tbx_b.Text;
            objEntidad.c = tbx_c.Text;
            objEntidad.d = tbx_d.Text;
            objEntidad.resp = Convert.ToChar(tbx_Resp.Text);
            objEntidad.pasage = tbx_Pasage.Text;
            objEntidad.dificultad = lbx_Dificultad.Text;

            objNego.N_Insertar(objEntidad);

            MessageBox.Show("Registro Insertado con exito");
        }

        private void tbx_Player1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_Player1.Text == "")
            {
                tbx_Player1.Text = "Player One";
                tbx_Player1.SelectAll();
            }
        }
        private void tbx_Player1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                Btn_Cancel.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
                {
                    e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                      //al ser true, evita que apareca la tecla presionada
                    tbx_Player2.Focus();
                }
        }


        private void tbx_Player2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_Player1.Text == "")
            {
                tbx_Player1.Text = "Player One";
                tbx_Player1.SelectAll();
            }
        }
        private void tbx_Player2_KeyPress(object sender, KeyPressEventArgs e)
        {// 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                Btn_Cancel.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
                {
                    e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                      //al ser true, evita que apareca la tecla presionada
                    btn_submit.PerformClick();
                }
        }


        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult salir;

            salir = MessageBox.Show("Are Sure you want to Exit?", "Warning", MessageBoxButtons.YesNo);


            if (salir == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            BackToMain();
        }

        private void BackToMain()
        {
            try
            {   // para saber si el formulario existe, o sea si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_focusedBibles").SingleOrDefault<Form>();

                if (existe != null)

                {
                    this.Hide();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                this.Hide();
            }

        }

        private void btn_NewQuests_Click(object sender, EventArgs e)
        {
            if (!(tlyo_AddQuest.Visible && gbx_AddQuest.Visible))
            {
                tlyo_AddQuest.Visible = true;
                gbx_AddQuest.Visible = true;
                tbx_Preg.Focus();
            }
            else
            {
                DialogResult confirm;

                confirm = MessageBox.Show("Are you sure you want to add this question?", "Confirmation", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    Insertar();
                }

                NumberOfQuestions(); // actualizar Numero de preguntas agregadas

                tlyo_AddQuest.Visible = false;
                gbx_AddQuest.Visible = false;
                tbx_Player1.Focus();
            }
            
        }

        private void NumberOfQuestions()
        {
            noQuestions = new int[objNego.N_NumFilas()]; // el tamaño es el tamaño del numero de filas

            if (noQuestions.Length + 1 >= 100)
            {
                lab_NoQuest.Text = Convert.ToString(noQuestions.Length + 1);
            }
            else
            {
                lab_NoQuest.Text = "0" + Convert.ToString(noQuestions.Length + 1);
            }
            
        }


        private void tbx_Preg_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Preg.SelectAll();
        }

        private void tbx_a_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_a.SelectAll();
        }

        private void tbx_b_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_b.SelectAll();
        }

        private void tbx_c_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_c.SelectAll();
        }

        private void tbx_d_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_d.SelectAll();
        }

        private void tbx_Resp_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Resp.SelectAll();
        }

        private void tbx_Player1_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Player1.SelectAll();
        }

        private void tbx_Player2_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Player2.SelectAll();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            NumberOfQuestions();

            lbx_Dificuldad_Setting.Text = difficulty;
            lbx_Dificultad.Text = "Normal";
            lbx_Rounds.Text = Convert.ToString(numRounds);
            lbx_time2Answer.Text = Convert.ToString(time2Answer);
            numRounds = Convert.ToInt32(lbx_Rounds.Text);
            time2Answer = Convert.ToInt32(lbx_time2Answer.Text);
            lab_User.Text = "User: " + E_Usuario.Nombreusuario;
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            if (tlyo_Settings.Visible == false && gbx_Settings.Visible == false)
            {
                tlyo_Settings.Visible = true;
                gbx_Settings.Visible = true;
                btn_Settings.Text = "Hide Settings";
                lbx_Rounds.Focus();
            }
            else
            {
                tlyo_Settings.Visible = false;
                gbx_Settings.Visible = false;
                btn_Settings.Text = "Show Settings";
                tbx_Player1.Focus();
            }
        }

        private void Btn_Cancel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                Btn_Cancel.PerformClick();
            }
        }

        private void tbx_Pasage_MouseClick(object sender, MouseEventArgs e)
        {
            tbx_Pasage.SelectAll();
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
                Btn_Cancel.PerformClick();
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
                Btn_Cancel.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                lbx_Dificuldad_Setting.Focus();
            }
        }

        private void lbx_Dificuldad_Setting_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
                Btn_Cancel.PerformClick();
            }
            else
                if (e.KeyChar == (char)13) //si la tecla pesionada es igual a ENTER (13)
            {
                e.Handled = true; //.Handled significa que nosotros nos haremos cargo del codigo
                                  //al ser true, evita que apareca la tecla presionada
                tbx_Player1.Focus();
            }
        }

        // para que se escriba N/A como pasage sin no ponen nada
        private void tbx_Pasage_TextChanged(object sender, EventArgs e)
        {
            if (tbx_Pasage.Text == "")
            {
                tbx_Pasage.Text = "N/A";
                tbx_Pasage.SelectAll();
            }
        }

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = HowToPlay.GetInscance();
            howToPlay.Show();
            howToPlay.BringToFront();
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de cerrar sección " +
                E_Usuario.Nombreusuario + "?", "Cerrar Sección",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                E_Usuario.Logged = 0; // para desactivar autologgin
                //DESLOGEARSE
                if (!(login.AutoLoginSet(E_Usuario.Nombreusuario, E_Usuario.Password, E_Usuario.Logged) == 1))
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
            this.Hide();
            usuario.Show();
        }

        private void Settings_Activated(object sender, EventArgs e)
        {
            lab_User.Text = "User: " + E_Usuario.Nombreusuario;
        }
    }
}
