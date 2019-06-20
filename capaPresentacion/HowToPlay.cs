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
    public partial class HowToPlay : Form
    {        
        public HowToPlay()
        {
            InitializeComponent();
        }

        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // Esto cierra la ventana de ayuda y deja ver la ventana Main
        }
    }
}
