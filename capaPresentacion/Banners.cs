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
    public partial class Banners : Form
    {
        public Banners(string banner = "Round 1")
        {
            this.banner = banner;
            InitializeComponent();
        }

        string banner;

        private void Banners_Load(object sender, EventArgs e)
        {
            lab_banner.Text = banner;
        }
    }
}
