using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace capaPresentacion
{
    public class P_QueryListarPreguntas
    {

        public string QueryPorCategoriayDificultad(ListBox lbx_categoria, ListBox lbx_categoria2, 
                                                     ListBox lbx_categoria3, string difficulty)
        {
            int valor = 0;
            string itemSelected;
            string Query = "SELECT DISTINCT codPreg, preg, a, b, c, d, resp, pasage from preguntas " +
                            "INNER JOIN " +
                            "Categoria ON Categoria.catID = preguntas.catLibro OR Categoria.catID = preguntas.catEvangelios_yOtros " +
                            "OR Categoria.catID = preguntas.catNuevoAntiguo ";


            //Crear Query para consultar por categorias en la base de datos
            if (lbx_categoria.Text != "Todas")
            {
                Query += "where (catEvangelios_yOtros=0";

                foreach (var item in lbx_categoria.SelectedItems)
                {
                    itemSelected = item.ToString();

                    switch (itemSelected)
                    {
                        case "Evangelios":
                            valor = 1;
                            break;
                        case "Pentateuco":
                            valor = 8;
                            break;
                        case "históricos":
                            valor = 9;
                            break;
                        case "poéticos":
                            valor = 10;
                            break;
                        case "proféticos mayores":
                            valor = 11;
                            break;
                        case "proféticos menores":
                            valor = 12;
                            break;
                        case "Epístolas paulinas":
                            valor = 13;
                            break;
                        case "Epístolas generales":
                            valor = 14;
                            break;
                        case "Profecías":
                            valor = 15;
                            break;
                        default:
                            break;
                    }

                    Query += " OR catEvangelios_yOtros=" + valor;
                }


                foreach (var item in lbx_categoria2.SelectedItems)
                {
                    itemSelected = item.ToString();

                    switch (itemSelected)
                    {
                        case "Génesis":
                            valor = 16;
                            break;
                        case "Éxodo":
                            valor = 17;
                            break;
                        case "Levítico":
                            valor = 18;
                            break;
                        case "Números":
                            valor = 19;
                            break;
                        case "Deuteronomio":
                            valor = 20;
                            break;
                        case "Jueces":
                            valor = 21;
                            break;
                        case "Josué":
                            valor = 22;
                            break;
                        case "Rut":
                            valor = 23;
                            break;
                        case "1 Samuel":
                            valor = 24;
                            break;
                        case "2 Samuel":
                            valor = 25;
                            break;
                        case "1 Reyes":
                            valor = 26;
                            break;
                        case "2 Reyes":
                            valor = 27;
                            break;
                        case "1 Crónicas":
                            valor = 28;
                            break;
                        case "2 Crónicas":
                            valor = 29;
                            break;
                        case "Esdras":
                            valor = 30;
                            break;
                        case "Nehemías":
                            valor = 31;
                            break;
                        case "Ester":
                            valor = 32;
                            break;
                        case "Job":
                            valor = 33;
                            break;
                        case "Salmos":
                            valor = 34;
                            break;
                        case "Proverbios":
                            valor = 35;
                            break;
                        case "Eclesiastés":
                            valor = 36;
                            break;
                        case "Cantares":
                            valor = 37;
                            break;
                        case "Isaías":
                            valor = 38;
                            break;
                        case "Jeremías":
                            valor = 39;
                            break;
                        case "Lamentaciones":
                            valor = 40;
                            break;
                        case "Ezequiel":
                            valor = 41;
                            break;
                        case "Daniel":
                            valor = 42;
                            break;
                        case "Oseas":
                            valor = 43;
                            break;
                        case "Joel":
                            valor = 44;
                            break;
                        case "Amós":
                            valor = 45;
                            break;
                        case "Abdías":
                            valor = 46;
                            break;
                        case "Jonás":
                            valor = 47;
                            break;
                        case "Miqueas":
                            valor = 48;
                            break;
                        case "Nahúm":
                            valor = 49;
                            break;
                        case "Habacuc":
                            valor = 50;
                            break;
                        case "Sofonías":
                            valor = 51;
                            break;
                        case "Hageo":
                            valor = 52;
                            break;
                        case "Zacarías":
                            valor = 53;
                            break;
                        case "Malaquías":
                            valor = 54;
                            break;
                        case "Mateo":
                            valor = 2;
                            break;
                        case "Marcos":
                            valor = 3;
                            break;
                        case "Lucas":
                            valor = 4;
                            break;
                        case "Juan":
                            valor = 5;
                            break;
                        case "Hechos":
                            valor = 55;
                            break;
                        case "Romanos":
                            valor = 56;
                            break;
                        case "1 Corintios":
                            valor = 57;
                            break;
                        case "2 Corintios":
                            valor = 58;
                            break;
                        case "Gálatas":
                            valor = 59;
                            break;
                        case "Efesios":
                            valor = 60;
                            break;
                        case "Filipenses":
                            valor = 61;
                            break;
                        case "1 Tesalonicenses":
                            valor = 62;
                            break;
                        case "2 Tesalonicenses":
                            valor = 63;
                            break;
                        case "1 Timoteo":
                            valor = 64;
                            break;
                        case "2 Timoteo":
                            valor = 65;
                            break;
                        case "Tito":
                            valor = 66;
                            break;
                        case "Filemón":
                            valor = 67;
                            break;
                        case "Hebreos":
                            valor = 68;
                            break;
                        case "Santiago":
                            valor = 69;
                            break;
                        case "1 Pedro":
                            valor = 70;
                            break;
                        case "2 Pedro":
                            valor = 71;
                            break;
                        case "1 Juan":
                            valor = 72;
                            break;
                        case "2 Juan":
                            valor = 73;
                            break;
                        case "3 Juan":
                            valor = 74;
                            break;
                        case "Judas":
                            valor = 75;
                            break;
                        case "Apocalipsis":
                            valor = 76;
                            break;
                        default:
                            break;
                    }

                    Query += " OR catLibro=" + valor;
                }


                foreach (var item in lbx_categoria3.SelectedItems)
                {
                    itemSelected = item.ToString();

                    switch (itemSelected)
                    {
                        case "Nuevo Testamento":
                            valor = 6;
                            break;
                        case "Antiguo Testamento":
                            valor = 7;
                            break;
                        default:
                            break;
                    }

                    Query += " OR catNuevoAntiguo=" + valor;
                }

                Query += ")";


                if (difficulty != "Todas")
                {
                    Query = string.Format("{0} AND dificultad ='{1}'", Query, difficulty);
                }
            }


            if (lbx_categoria.Text == "Todas" && difficulty != "Todas")
            {
                Query = string.Format("where dificultad ='{0}'", difficulty);
            }

            return Query;
        }
    }
}
