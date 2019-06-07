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

namespace capaPresentacion
{
    public partial class P_Main : Form
    {
        public P_Main()
        {
            InitializeComponent();
        }


        HowToPlay howToPlay;
        P_GameSettings GameSettings = new P_GameSettings();


        private void Main_Load(object sender, EventArgs e)
        {
            lab_User.Text = "User: " + E_Usuario.Nombreusuario;
        }

        private void Btn_Settings_MouseEnter(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Settings_MouseUp;
        }

        private void Btn_Settings_MouseLeave(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Settings;
        }

        private void btn_newUser_Click(object sender, EventArgs e)
        {
            DialogResult salir;

            salir = MessageBox.Show("Seguro que desea salir?", "Advertencia", MessageBoxButtons.YesNo);


            if (salir == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_debate_Click(object sender, EventArgs e)
        {
            P_Debate_Main debateMain = new P_Debate_Main();
            debateMain.ShowDialog();
        }

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = new HowToPlay();
            howToPlay.ShowDialog();
        }

        private void Btn_Settings_Click(object sender, EventArgs e)
        {
            OpenGameSettings();
        }

        private void OpenGameSettings()
        {
            try
            {   // para saber si el formulario existe, o sea si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_GameSettings").SingleOrDefault<Form>();

                if (existe != null)
                {
                    GameSettings.ShowDialog();
                }
                else
                {
                    GameSettings = new P_GameSettings();
                    GameSettings.ShowDialog();
                }
            }
            catch (Exception)
            {
                GameSettings.ShowDialog();
            }
        }
    }
}
