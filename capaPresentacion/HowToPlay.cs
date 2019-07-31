using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using capaEntidad;

namespace capaPresentacion
{
    public partial class HowToPlay : Form
    {        
        public HowToPlay(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;
            InitializeComponent();
        }

        E_focusedBible objEntidad = new E_focusedBible();



        private void HowToPlay_Load(object sender, EventArgs e)
        {
            string nombreArchivo = "ayuda.pdf";
            string path = Application.StartupPath + @"\ayuda\" + nombreArchivo;

            leerPDF(path);
        }

        public void leerPDF(string path)
        {
            try
            {
                // leer pdf combinado
                axAcroPDF.LoadFile(path);
                axAcroPDF.setShowToolbar(false);
                axAcroPDF.setZoom(100);
            }
            catch (Exception)
            {
                MessageBox.Show("Archivo no encontrado");
            }
        }

        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            // para saber si se acaba de salir de settings a pantalla de juego
            E_focusedBible.fromHowToPlay = true;

            this.DialogResult = DialogResult.OK; // Esto cierra la ventana de ayuda y deja ver la ventana Main
        }



        // to be able to move the windows-------------
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //-------------------------------------------------

        //METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO  TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 15;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.pnlMain.Region = region;
            this.Invalidate();
        }

        private void btn_Cerrar_MouseEnter(object sender, EventArgs e)
        {
            objEntidad.reproducirSonidoBoton("button.wav", false);
            btn_Cerrar.BackgroundImage = Properties.Resources.Boton_Empezar_MouseEnter;
        }

        private void btn_Cerrar_MouseLeave(object sender, EventArgs e)
        {
            btn_Cerrar.BackgroundImage = Properties.Resources.Boton_Empezar_MouseLeave;
        }
    }
}
