using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace capaPresentacion
{
    class P_SeleccionCategoria
    {
        public void SeleccionarCategorías(string[] categoria, ListBox lbx_categoria)
        {    
            for (int index = 0; index <= categoria.Length - 1; index++)
            {
                lbx_categoria.SetSelected(lbx_categoria.FindString(categoria[index]), true);
            }

            if (categoria[0] != "Evangelios" && categoria[0] != "Génesis")
            {
                lbx_categoria.SetSelected(0, false);
            }
        }

        // almacenar los elementos seleccionados en "x" listbox
        public string[] AlmacenarSeleccionCategorías(ListBox lbx_categoria)
        {
            string[] categoria = new string[lbx_categoria.SelectedItems.Count];
            for (int index = 0; index <= lbx_categoria.SelectedItems.Count - 1; index++)
            {
                categoria[index] = lbx_categoria.SelectedItems[index].ToString();
            }

            return categoria;
        }



        public void bloquearDesbloquearDeseleccionarCamposCategoría(bool bloquearOcultar, ListBox lbx_categoria)
        {
            // solo si lbx categoria está visible. Para que no se seleccione todo.
            if (bloquearOcultar == false && lbx_categoria.Enabled == true)
            {
                for (int index = 0; index <= lbx_categoria.Items.Count - 1; index++)
                {
                    lbx_categoria.SetSelected(index, bloquearOcultar);
                }
            }

            lbx_categoria.Visible = bloquearOcultar;
        }
    }
}
