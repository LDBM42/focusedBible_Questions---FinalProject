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
    public partial class HowToPlay : Form
    {        
        public HowToPlay()
        {
            InitializeComponent();
        }

        private static HowToPlay _Instancia;

        public static HowToPlay GetInscance()
        {
            if (_Instancia == null)
                _Instancia = new HowToPlay();
            return _Instancia;
        }

        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
