using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace capaPresentacion
{
    public partial class P_Splash : Form
    {
        public P_Splash()
        {
            InitializeComponent();
        }

        private void P_Splash_Load(object sender, EventArgs e)
        {
            TerminarSplash.Start();
        }

        private void TerminarSplash_Tick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; //terminar Splash y abrir Main
        }
    }
}
