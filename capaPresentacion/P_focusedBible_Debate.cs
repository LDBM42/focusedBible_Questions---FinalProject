using capaEntidad;
using capaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace capaPresentacion
{
    public partial class P_focusedBible_Debate : Form
    {
        public P_focusedBible_Debate(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;

            opportunities_1 = objEntidad.opportunities;
            opportunities_2 = objEntidad.opportunities;

            InitializeComponent();
        }


        #region Variables y Objetos
        int timeToIncrease = 15; // tiempo que incrementa al elegir el comodin pasage
        Banners Banner;
        SoundPlayer sonido;
        int?[] noRepetir_PorDificultadyCategoria; // para que no se repitan cuando se eligen solo x dificultad
        E_focusedBible[] lista_porDificultadYCategoria; // Para almacenar la lista completa y asi evitar que se repitan
        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        int numeroPrueba;
        int codPregNoRepetir;
        int retry;
        int reintentos = 50;
        int click50_1 = 0; // para saber si el jugador(es) 1 ya ha entrado al evento click de el comodin 50%
        int click50_2 = 0; // para saber si el jugador(es) 1 ya ha entrado al evento click de el comodin 50%
        int clickPassage_1 = 0; // para saber si el jugador(es) 1 ya ha entrado al evento click de el Passage_1
        int clickPassage_2 = 0; // para saber si el jugador(es) 1 ya ha entrado al evento click de el Passage_1
        int i = 0;
        int countUp = 0;
        int countDownTimer = 3;
        int countDownTimer2;
        int countDownTimer3 = 2;
        int score_1 = 0; // puntuacion inicial
        int score_2 = 0; // puntuacion inicial
        int opportunities_1;
        int opportunities_2;
        int startingTurn = 1; // turno inicial
        int startingRound = 1; // ronda inicial
        string banner;
        int wins_01 = 0;
        int wins_02 = 0;
        bool restart = false;
        int enumerate = 1; // para ponerle número a las preguntas
        string[] comodin50_1 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodin50_2 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage_1 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage_2 = new string[] { "0", "+1", "+2", "+3" };
        char[] disappear50 = new char[] { 'a', 'b', 'c', 'd' };
        int countDownComodin_1 = 3;
        int countDownComodin_2 = 3;
        int countDownPassage_1 = 3;
        int countDownPassage_2 = 3;
        #endregion



        private void P_focusedBibles_Load(object sender, EventArgs e)
        {
            lab_Wins_P1.Text = Convert.ToString(wins_01);
            lab_Wins_P2.Text = Convert.ToString(wins_02);
            tlyo_Wins_P1.Visible = true;
            tlyo_Wins_P2.Visible = false;
            lab_Rounds_Left.Text = startingRound + "/" + objEntidad.numRounds;
            lab_Rounds_Right.Text = startingRound + "/" + objEntidad.numRounds;
            lab_Difficulty.Text = objEntidad.difficulty;
            lab_Categoria.Text = objEntidad.catEvangelios_yOtros[0];
            lab_LifesNum.Text = Convert.ToString(opportunities_1);
            lab_LifesNum2.Text = Convert.ToString(opportunities_2);
            lab_Group1.Text = objEntidad.group1;
            lab_Group2.Text = objEntidad.group2;
            countDownTimer2 = objEntidad.time2Answer;
            Timer_2Answer.Start();
            banner = "Round" + startingRound;
            reproducirSonido("levelclearer.wav", true);
            objEntidad.difficulty = objEntidad.difficulty;
            objEntidad.catEvangelios_yOtros = objEntidad.catEvangelios_yOtros;
            objEntidad.catLibro = objEntidad.catLibro;
            objEntidad.catNuevoAntiguo = objEntidad.catNuevoAntiguo;
            noRepetir_PorDificultadyCategoria = new int?[objNego.N_NumFilas_PorDificultadYCategoria(objEntidad)]; // el valor devuelto es tamaño del numero de filas
            lista_porDificultadYCategoria = new E_focusedBible[objNego.N_NumFilas_PorDificultadYCategoria(objEntidad)];
            Llenar_listaPorDificultadYCategoria(objEntidad);
            listarFocusedBible(objEntidad);
            focoRbtn();
            bloquear_Btn_Submit();
        }
        
        void Llenar_listaPorDificultadYCategoria(E_focusedBible dificultad)
        {
            DataTable dt2 = objNego.N_listadoPor_DificultadYCategoria(dificultad);

            // llena todos los atributos de cada objeto del arreglo, creando así una copia de la tabla
            for (int i = 0; i < noRepetir_PorDificultadyCategoria.Length; i++)
            {
                dificultad = new E_focusedBible(); // para que se cree un nuevo objeto para cada posición
                dificultad.codPreg = Convert.ToInt32(dt2.Rows[i]["codPreg"].ToString());
                dificultad.preg = dt2.Rows[i]["preg"].ToString();
                dificultad.a = dt2.Rows[i]["a"].ToString();
                dificultad.b = dt2.Rows[i]["b"].ToString();
                dificultad.c = dt2.Rows[i]["c"].ToString();
                dificultad.d = dt2.Rows[i]["d"].ToString();
                dificultad.resp = Convert.ToChar(dt2.Rows[i]["resp"].ToString());
                dificultad.pasage = dt2.Rows[i]["pasage"].ToString();

                lista_porDificultadYCategoria[i] = dificultad;
            }
        }
        void listarFocusedBible(E_focusedBible preg)
        {
                randomQuestions_PorDificultadyCategoría();

                lab_Pregunta.Text = Convert.ToString(enumerate) + ". " + lista_porDificultadYCategoria[numeroPrueba].preg;
                rbtn_a.Text = "a)   " + lista_porDificultadYCategoria[numeroPrueba].a;
                rbtn_b.Text = "b)   " + lista_porDificultadYCategoria[numeroPrueba].b;
                rbtn_c.Text = "c)   " + lista_porDificultadYCategoria[numeroPrueba].c;
                rbtn_d.Text = "d)   " + lista_porDificultadYCategoria[numeroPrueba].d;
                preg.resp = lista_porDificultadYCategoria[numeroPrueba].resp;
                objEntidad.pasage = lista_porDificultadYCategoria[numeroPrueba].pasage;
            

            blockPassage(); //si no existe pasage de referencia, se bloquea este comodin
            enumerate++;
        }


        void randomQuestions_PorDificultadyCategoría()
        {
            Random random2 = new Random();
            retry = 0;

            while (true)
            {
                // numeros aleatorios desde el 1 hasta el tamaño del arreglo
                numeroPrueba = random2.Next(0, noRepetir_PorDificultadyCategoria.Length);
                codPregNoRepetir = lista_porDificultadYCategoria[numeroPrueba].codPreg;
                // si existe el código dentro del arreglo se agrega al arreglo, si no existe se crea el random
                if (Array.Exists(noRepetir_PorDificultadyCategoria, codPreg => codPreg == codPregNoRepetir))
                {
                    if (countUp == noRepetir_PorDificultadyCategoria.Length || retry > reintentos)
                    {
                        break;
                    }
                    else
                    {
                        retry++; // para detener si ya se acabaron las preguntas
                        continue;
                    }
                }
                else //si el código no eiste en el arreglo
                {
                    noRepetir_PorDificultadyCategoria[i] = codPregNoRepetir; //agregar código al arreglo para que nunca se repitan

                    // un numeros aleatorios del 1 al numero de filas se almacena como indice
                    //lista_porDificultad[numeroPrueba];

                    i++;
                    countUp++;
                    break;
                }
            }

        }
        
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            RestartTimer_2Answer();

            if (rbtn_a.Checked == true)
            {
                if (objEntidad.resp == 'a')
                {
                    lab_correctWrong(1);
                }
                else
                {
                    lab_correctWrong(0);
                }
            }
            else
                if (rbtn_b.Checked == true)
            {
                if (objEntidad.resp == 'b')
                {
                    lab_correctWrong(1);
                }
                else
                {
                    lab_correctWrong(0);
                }
            }
            else
                if (rbtn_c.Checked == true)
            {
                if (objEntidad.resp == 'c')
                {
                    lab_correctWrong(1);
                }
                else
                {
                    lab_correctWrong(0);
                }
            }
            else
                if (rbtn_d.Checked == true)
            {
                if (objEntidad.resp == 'd')
                {
                    lab_correctWrong(1);
                }
                else
                {
                    lab_correctWrong(0);
                }
            }


            //Evaluar si perdio o gano
            perder_Ganar();



            // cambio de turno si no ha terminado el juego
            if (opportunities_1 != 0
            && opportunities_2 != 0
            && ((countUp != noRepetir_PorDificultadyCategoria.Length) || (objEntidad.difficulty == "Todas"))
            && retry <= reintentos
            && ((objEntidad.questions2Answer != "Todas") && (enumerate <= Convert.ToInt32(objEntidad.questions2Answer))))
            {
                cambioDeTurno(startingTurn, false);
            }

            // Cambio de Jugador-----------------------------------------------
            if (startingTurn == 1)
            {
                countDown.Start();

                startingTurn = 2; //Player 2
            }
            else
            {
                countDown.Start();
                startingTurn = 1; //Player 1
            }


            btn_Submit.Enabled = false;
        }
        
        private void reproducirSonido(string nombreArchivo, bool loop)
        {
            if (sonido != null)
            {
                sonido.Stop();
            }
            //SystemSounds.Hand.Play(); // Sonido de windows
            try
            {
                sonido = new SoundPlayer(Application.StartupPath + @"\son\" + nombreArchivo);
                if (loop == true)
                {
                    sonido.PlayLooping();
                }
                else
                {
                    sonido.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
        
        void BannerStart(string banner)
        {
            //Thread.Sleep(2000);
            Timer_Banner.Start();
            this.banner = banner;
            Banner = new Banners(banner);
            Banner.Show();

            sonido.Stop();
            if (banner == "Round " + startingRound)  // solo se reproduce el sonido si es un cambio de round
            {
                reproducirSonido("start-ready-go.wav", false);
            }
        }
        void StartAgan()
        {
            if ((startingRound == objEntidad.numRounds && banner != "Round " + startingRound)
                || ((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas"))) // significa que el juego ha terminado
            {
                Timer_2Answer.Stop();
                sonido.Stop();

                DialogResult respuesta = MessageBox.Show("The Game has Finished!\nDo you want to Play Again?", "Game Over", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {

                    reproducirSonido("start-ready-go.wav", false);
                    Thread.Sleep(700);
                    restart = true;
                    reset_PlayAgain();
                    Banner.Hide();

                }
                else
                {
                    Application.Exit();
                }
            }
        }
        void perder_Ganar()
        {
            //condicion para perder
            if (opportunities_1 == 0
                || opportunities_2 == 0
                || ((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas"))
                || retry > reintentos
                || ((objEntidad.questions2Answer != "Todas") && (enumerate > Convert.ToInt32(objEntidad.questions2Answer))) )
            {
                Timer_2Answer.Stop();
                Thread.Sleep(2000);
                reproducirSonido("game-over.wav", false);
                
                Thread.Sleep(2000);

                if (startingRound == objEntidad.numRounds) // si es el ultimo round
                {
                    //condicion para saber quien perdió
                    if (startingTurn == 1)
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        BannerStart(lab_Group2.Text + " Wins");
                    }
                    else
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        BannerStart(lab_Group1.Text + " Wins");
                    }
                }
                else
                    if (((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas"))) // si se acaban las preguntas, se acaba el juego
                {
                    Thread.Sleep(2000);

                    //condicion para saber quien perdió
                    if (score_1 == score_2)
                    {
                        BannerStart("It's a Draw!");
                    }
                    else
                        if (score_2 > score_1)
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        BannerStart(lab_Group2.Text + " Wins");
                    }
                    else
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        BannerStart(lab_Group1.Text + " Wins");
                    }
                }
                else
                {
                    ChangeRound();
                }
            }
        }
        void ChangeRound()
        {
            startingRound++;
            lab_Rounds_Left.Text = startingRound + "/" + objEntidad.numRounds;
            lab_Rounds_Right.Text = startingRound + "/" + objEntidad.numRounds;

            if (startingTurn == 1)
            {
                wins_02++;
                lab_Wins_P2.Text = Convert.ToString(wins_02);
            }
            else
            {
                wins_01++;
                lab_Wins_P1.Text = Convert.ToString(wins_01);
            }

            reset_PlayAgain();
            BannerStart("Round " + startingRound);
        }
        void reset_PlayAgain()
        {
            Timer_2Answer.Stop(); //detener conteo
            countDownTimer = 3;
            countDownTimer2 = objEntidad.time2Answer;
            countDownTimer3 = 2;

            if (restart == true)
            {
                restart = false;

                i = 0;
                countUp = 0;
                enumerate = 1; // para ponerle número a las preguntas
                startingRound = 1;
                startingTurn = 1;
                wins_01 = 0;
                wins_02 = 0;
                score_1 = 0;
                score_2 = 0;
                objEntidad.pasage = "";
                lab_Rounds_Left.Text = startingRound + "/" + objEntidad.numRounds;
                lab_Rounds_Right.Text = startingRound + "/" + objEntidad.numRounds;
                lab_Wins_P1.Text = Convert.ToString(wins_01);
                lab_Wins_P2.Text = Convert.ToString(wins_02);

                Array.Clear(noRepetir_PorDificultadyCategoria, 0, noRepetir_PorDificultadyCategoria.Length); // vaciar arreglo
            }

            opportunities_1 = objEntidad.opportunities;
            opportunities_2 = objEntidad.opportunities;
            countDownComodin_1 = 3;
            countDownComodin_2 = 3;
            countDownPassage_1 = 3;
            countDownPassage_2 = 3;
            Lab_Passage_Shown_1.Text = "";

            Timer_2Answer.Start();

            if (banner == lab_Group1.Text + " Wins" || banner == lab_Group2.Text + " Wins" || banner == "It's a Draw!")
            {
                AfterCountDown(true);
                banner = "Round" + startingRound; // resetear banner
            }



            lab_LifesNum.Text = Convert.ToString(opportunities_1);
            lab_LifesNum2.Text = Convert.ToString(opportunities_2);

            lab_ScoreNum.Text = Convert.ToString(score_1);
            lab_ScoreNum2.Text = Convert.ToString(score_2);

            lab_50_1.Text = "+3";
            lab_50_2.Text = "+3";
            pbx_50_1.Enabled = true;
            pbx_50_2.Enabled = true;
            lab_50_1.Enabled = true;
            lab_50_2.Enabled = true;

            lab_Passage_1.Text = "+3";
            lab_Passage_2.Text = "+3";
            pbx_Passage_1.Enabled = true;
            pbx_Passage_2.Enabled = true;
            lab_Passage_1.Enabled = true;
            lab_Passage_2.Enabled = true;
            pbx_Passage_1.Image = Properties.Resources.Passage_Mouse_Leave;
            pbx_Passage_2.Image = Properties.Resources.Passage_Mouse_Leave;
        }
        
        void uncheckRbtn()
        {
            rbtn_a.Checked = false;
            rbtn_b.Checked = false;
            rbtn_c.Checked = false;
            rbtn_d.Checked = false;
        }
        void makeVisibleRbtn_andEnabled()
        {
            rbtn_a.Visible = true;
            rbtn_a.Enabled = true;
            rbtn_b.Visible = true;
            rbtn_b.Enabled = true;
            rbtn_c.Visible = true;
            rbtn_c.Enabled = true;
            rbtn_d.Visible = true;
        }
        void focoRbtn() //para tener el foco en las respuestas
        {
            if (rbtn_a.Visible == true)
            {
                rbtn_a.Focus();
                rbtn_a.Checked = false;
            }
            else
                if (rbtn_b.Visible == true)
            {
                rbtn_b.Focus();
                rbtn_b.Checked = false;
            }
            else
                if (rbtn_c.Visible == true)
            {
                rbtn_c.Focus();
                rbtn_c.Checked = false;
            }
            else
                if (rbtn_d.Visible == true)
            {
                rbtn_d.Focus();
                rbtn_d.Checked = false;
            }
        }
        
        void correctAnswer()
        {
            if ('a' == objEntidad.resp)
            {
                rbtn_a.ForeColor = Color.FromArgb(228, 161, 24);

                rbtn_b.ForeColor = Color.FromArgb(225, 225, 225);
                rbtn_c.ForeColor = Color.FromArgb(225, 225, 225);
                rbtn_d.ForeColor = Color.FromArgb(225, 225, 225);

                rbtn_b.Enabled = false;
                rbtn_c.Enabled = false;
                rbtn_d.Enabled = false;
            }
            else
                if ('b' == objEntidad.resp)
            {
                rbtn_b.ForeColor = Color.FromArgb(228, 161, 24);

                rbtn_a.ForeColor = Color.FromArgb(225, 225, 225);
                rbtn_c.ForeColor = Color.FromArgb(225, 225, 225);
                rbtn_d.ForeColor = Color.FromArgb(225, 225, 225);

                rbtn_a.Enabled = false;
                rbtn_c.Enabled = false;
                rbtn_d.Enabled = false;
            }
            else
                if ('c' == objEntidad.resp)
            {
                rbtn_c.ForeColor = Color.FromArgb(228, 161, 24);

                rbtn_a.ForeColor = Color.FromArgb(225, 225, 225);
                rbtn_b.ForeColor = Color.FromArgb(225, 225, 225);
                rbtn_d.ForeColor = Color.FromArgb(225, 225, 225);

                rbtn_a.Enabled = false;
                rbtn_b.Enabled = false;
                rbtn_d.Enabled = false;
            }
            else
                if ('d' == objEntidad.resp)
            {
                rbtn_d.ForeColor = Color.FromArgb(228, 161, 24);

                rbtn_a.ForeColor = Color.FromArgb(225, 225, 225);
                rbtn_c.ForeColor = Color.FromArgb(225, 225, 225);
                rbtn_b.ForeColor = Color.FromArgb(225, 225, 225);

                rbtn_a.Enabled = false;
                rbtn_c.Enabled = false;
                rbtn_b.Enabled = false;
            }
        }
        void lab_correctWrong(int answer) // 0 = Wrong, 1 u otro número = Correct
        {
            if (answer == 0)
            {
                correctAnswer();

                Timer_2Answer.Stop(); //detener conteo
                reproducirSonido("retro-lose.wav", false);
                lab_Anuncios.ForeColor = Color.Brown;
                lab_Anuncios.Text = "Wrong Answer";
            }
            else
            {
                correctAnswer();
                Timer_2Answer.Stop(); //detener conteo
                correctAnswerSound();
                lab_Anuncios.ForeColor = Color.FromArgb(228, 161, 24);
                lab_Anuncios.Text = "Correct Answer";
            }
        }
        void answerOriginalColor()
        {
            rbtn_a.ForeColor = Color.FromArgb(135, 135, 135);
            rbtn_b.ForeColor = Color.FromArgb(135, 135, 135);
            rbtn_c.ForeColor = Color.FromArgb(135, 135, 135);
            rbtn_d.ForeColor = Color.FromArgb(135, 135, 135);

            rbtn_a.Enabled = true;
            rbtn_b.Enabled = true;
            rbtn_c.Enabled = true;
            rbtn_d.Enabled = true;
        }
        void correctAnswerSound()
        {
            reproducirSonido("correctAnswer3.wav", false);
            Thread.Sleep(400);
            reproducirSonido("cheering-and-clapping2.wav", false);
        }
        void PlayerFocus(int turno)
        {
            if (turno == 1)
            {
                // si hay cambio de turno y pasage no es igual a N/A o "", Ni se ha acabado el comodin passage
                if (objEntidad.pasage != "N/A" && objEntidad.pasage != "" && lab_Passage_1.Text != "0")
                {
                    lab_Passage_1.Enabled = true;
                }

                //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
                lab_Group1.Font = new Font(lab_Group1.Font.Name, 20, lab_Group1.Font.Style, lab_Group1.Font.Unit);
                //para cambiar el color a gris
                lab_Group1.ForeColor = Color.FromArgb(228, 161, 24);

                lab_Group2.Font = new Font(lab_Group2.Font.Name, 10, lab_Group2.Font.Style, lab_Group2.Font.Unit);
                //para cambiar el color a orange
                lab_Group2.ForeColor = Color.FromArgb(237, 237, 237);


                cambiarColoryJugador(turno);

                Timer_2Answer.Start();

            }
            else // si el turno es 2
            {
                // si hay cambio de turno y pasage no es igual a N/A o "", Ni se ha acabado el comodin passage
                if (objEntidad.pasage != "N/A" && objEntidad.pasage != "" && lab_Passage_2.Text != "0")
                {
                    lab_Passage_2.Enabled = true;
                }

                lab_Group2.Font = new Font(lab_Group2.Font.Name, 20, lab_Group2.Font.Style, lab_Group2.Font.Unit);
                lab_Group2.ForeColor = Color.FromArgb(228, 161, 24);

                lab_Group1.Font = new Font(lab_Group1.Font.Name, 10, lab_Group1.Font.Style, lab_Group1.Font.Unit);
                lab_Group1.ForeColor = Color.FromArgb(237, 237, 237);

                cambiarColoryJugador(turno);

                Timer_2Answer.Start();
            }
        }
        void cambiarColoryJugador(int turno)
        {
            if (turno == 1)
            {
                lab_Lifes.ForeColor = Color.FromArgb(135, 135, 135);
                lab_LifesNum.ForeColor = Color.Brown;
                lab_Score.ForeColor = Color.FromArgb(135, 135, 135);
                lab_ScoreNum.ForeColor = Color.FromArgb(228, 161, 24);
                tlyo_Wins_P1.Visible = true;

                lab_Lifes2.ForeColor = Color.FromArgb(237, 237, 237);
                lab_LifesNum2.ForeColor = Color.FromArgb(237, 237, 237);
                lab_Score2.ForeColor = Color.FromArgb(237, 237, 237);
                lab_ScoreNum2.ForeColor = Color.FromArgb(237, 237, 237);
                tlyo_Wins_P2.Visible = false;
            }
            else // si el turno es 2
            {
                lab_Lifes2.ForeColor = Color.FromArgb(135, 135, 135);
                lab_LifesNum2.ForeColor = Color.Brown;
                lab_Score2.ForeColor = Color.FromArgb(135, 135, 135);
                lab_ScoreNum2.ForeColor = Color.FromArgb(228, 161, 24);
                tlyo_Wins_P2.Visible = true;

                lab_Lifes.ForeColor = Color.FromArgb(237, 237, 237);
                lab_LifesNum.ForeColor = Color.FromArgb(237, 237, 237);
                lab_Score.ForeColor = Color.FromArgb(237, 237, 237);
                lab_ScoreNum.ForeColor = Color.FromArgb(237, 237, 237);
                tlyo_Wins_P1.Visible = false;
            }
        }
        void cambioDeTurno(int turno, bool answerCorrect) // si el turno es uno y la respuesta fue correcta
        {
            // para desactivar el 50% si ya se ha acabado
            if (turno == 1 && lab_50_1.Text == "0")
            {
                lab_50_1.Enabled = false;
                pbx_50_1.Enabled = false;
            }
            else
                if (turno == 2 && lab_50_2.Text == "0")
            {
                lab_50_2.Enabled = false;
                pbx_50_2.Enabled = false;
            }

            // para desactivar el comodinPassage si ya se ha acabado
            if (turno == 1 && lab_Passage_1.Text == "0")
            {
                lab_Passage_1.Enabled = false;
                pbx_Passage_1.Enabled = false;
            }
            else
                if (turno == 2 && lab_Passage_2.Text == "0")
            {
                lab_Passage_2.Enabled = false;
                pbx_Passage_2.Enabled = false;
            }

            //Codigo principal del metodo
            if (turno == 1)
            {
                click50_2 = 0; // reiniciar a 0 para poder usar el comodin 50% en su proximo turno
                pbx_50_2.Enabled = true; // activar comodin anterior al cambiar de turno

                clickPassage_2 = 0; // reiniciar a 0 para poder usar el comodin Passage en su proximo turno
                pbx_Passage_2.Enabled = true; // activar comodin anterior al cambiar de turno
                Lab_Passage_Shown_2.Text = "";
                pbx_Passage_2.Image = Properties.Resources.Passage_Mouse_Leave; // volver a cargar imagen inicial

                if (answerCorrect == true)
                {
                    score_1++;
                    lab_ScoreNum.Text = Convert.ToString(score_1);
                }
                else
                {
                    opportunities_1--;
                    lab_LifesNum.Text = Convert.ToString(opportunities_1);
                }
            }
            else
            {
                click50_1 = 0; // reiniciar a 0 para poder usar el comodin 50% en su proximo turno
                pbx_50_1.Enabled = true; // activar comodin anterior al cambiar de turno

                clickPassage_1 = 0; // reiniciar a 0 para poder usar el comodin Passage en su proximo turno
                pbx_Passage_1.Enabled = true; // activar comodin anterior al cambiar de turno
                Lab_Passage_Shown_1.Text = "";
                pbx_Passage_1.Image = Properties.Resources.Passage_Mouse_Leave; // volver a cargar imagen inicial

                if (answerCorrect == true)
                {
                    score_2++;
                    lab_ScoreNum2.Text = Convert.ToString(score_2);
                }
                else
                {
                    opportunities_2--;
                    lab_LifesNum2.Text = Convert.ToString(opportunities_2);
                }
            }
        }
        
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult salir;
            if (startingTurn == 1)
            {
                salir = MessageBox.Show(lab_Group1.Text + ", Are Sure you want to Exit?", "Warning", MessageBoxButtons.YesNo);
            }
            else
            {
                salir = MessageBox.Show(lab_Group2.Text + ", Are Sure you want to Exit?", "Warning", MessageBoxButtons.YesNo);
            }

            if (salir == DialogResult.Yes)
            {
                Application.Exit();
            }

        }
        
        private void pbx_50_MouseEnter(object sender, EventArgs e)
        {
            if (lab_50_1.Text != "0")
            {
                pbx_50_1.Image = Properties.Resources._50_percent_MouseOn;
            }
        }
        private void pbx_50_MouseLeave(object sender, EventArgs e)
        {
            pbx_50_1.Image = Properties.Resources._50_percent;
        }
        private void pbx_50_Click(object sender, EventArgs e)
        {
            if (lab_50_1.Text != "0")
            {
                countDownComodin_1--;
                lab_50_1.Text = comodin50_1[countDownComodin_1];
                random50();
                pbx_50_1.Enabled = false;
                focoRbtn();

                btn_Submit.Enabled = false; //para evitar que se presione submit sin estar seleccionada ninguna respuesta
                uncheckRbtn();
            }
        }

        private void pbx_50_2_MouseEnter(object sender, EventArgs e)
        {
            if (lab_50_2.Text != "0")
            {
                pbx_50_2.Image = Properties.Resources._50_percent_MouseOn;
            }
        }
        private void pbx_50_2_MouseLeave(object sender, EventArgs e)
        {
            pbx_50_2.Image = Properties.Resources._50_percent;
        }
        private void pbx_50_2_Click(object sender, EventArgs e)
        {
            if (lab_50_2.Text != "0")
            {
                countDownComodin_2--;
                lab_50_2.Text = comodin50_2[countDownComodin_2];
                random50();
                pbx_50_2.Enabled = false;
                focoRbtn();
                btn_Submit.Enabled = false; //para evitar que se presione submit sin estar seleccionada ninguna respuesta

                uncheckRbtn();
            }
        }
        void activarComidin(int turno)
        {
            if (turno == 1)
            {
                pbx_50_2.Visible = false;
                lab_50_2.Visible = false;
                pbx_50_1.Visible = true;
                lab_50_1.Visible = true;
            }
            else
            {
                pbx_50_1.Visible = false;
                lab_50_1.Visible = false;
                pbx_50_2.Visible = true;
                lab_50_2.Visible = true;
            }
        }
        void random50()
        {
            Random random = new Random();
            int i = 0;
            int indice;

            while (i != 2)
            {
                indice = random.Next(0, 3);
                if (objEntidad.resp != disappear50[indice])
                {
                    if (disappear50[indice] == 'a')
                    {
                        if (rbtn_a.Visible == true) // condicion para saber si ya se ha vuelto invisible,
                                                    //para que no lo cuente denuevo
                        {
                            rbtn_a.Enabled = false;
                            rbtn_a.Visible = false;
                            i++;
                        }
                    }
                    else
                        if (disappear50[indice] == 'b')
                    {
                        if (rbtn_b.Visible == true)
                        {
                            rbtn_b.Enabled = false;
                            rbtn_b.Visible = false;
                            i++;
                        }
                    }
                    else
                        if (disappear50[indice] == 'c')
                    {
                        if (rbtn_c.Visible == true)
                        {
                            rbtn_c.Enabled = false;
                            rbtn_c.Visible = false;
                            i++;
                        }
                    }
                    else
                        if (disappear50[indice] == 'd')
                    {
                        if (rbtn_d.Visible == true)
                        {
                            rbtn_d.Enabled = false;
                            rbtn_d.Visible = false;
                            i++;
                        }
                    }
                }
            }
        }
        
        private void countDown_Tick(object sender, EventArgs e)
        {
            if (countDownTimer != 1)
            {
                countDownTimer--;
            }
            else
            {
                countDownTimer = 3;
                countDown.Stop();
                lab_Anuncios.Text = "";

                if (banner == lab_Group1.Text + " Wins" || banner == lab_Group2.Text + " Wins")
                {
                }
                else
                    if (((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas")))
                {
                    perder_Ganar();
                }
                else
                {
                    AfterCountDown();
                }

            }
        }

        void AfterCountDown(bool restart = false)
        {
            if (restart == true)
            {
                Thread.Sleep(1500);
            }

            answerOriginalColor(); // aqui cambian las respuestas al color original
            listarFocusedBible(objEntidad); //lista las preguntas y respuestas
            uncheckRbtn(); //desmarca las respuestas
            makeVisibleRbtn_andEnabled();//pone las resuestas visibles y abilitadas
            focoRbtn(); // se pone el foco las respuestas para poder seleccionarlas con el teclado

            bloquear_Btn_Submit();


            if (startingTurn == 1)
            {
                activarComidin(1);
                activarPassage(1);
                PlayerFocus(1);
                reproducirSonido("levelclearer.wav", true);
            }
            else
            {
                activarComidin(2);
                activarPassage(2);
                PlayerFocus(2);
                reproducirSonido("levelclearer.wav", true);
            }
        }
        


        //eventos para seleccionar a travez del teclado
        private void rbtn_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            selectAnswer(e);
        }
        private void rbtn_b_KeyPress(object sender, KeyPressEventArgs e)
        {
            selectAnswer(e);
        }
        private void rbtn_c_KeyPress(object sender, KeyPressEventArgs e)
        {
            selectAnswer(e);
        }
        private void rbtn_d_KeyPress(object sender, KeyPressEventArgs e)
        {
            selectAnswer(e);
        }
        private void btn_Submit_KeyPress(object sender, KeyPressEventArgs e)
        {
            selectAnswer(e);
        }
        private void btn_Exit_KeyPress(object sender, KeyPressEventArgs e)
        {
            selectAnswer(e);
        }
        


        void selectAnswer(KeyPressEventArgs e)
        {
            // 'e' almacena la tecla presionada
            if (e.KeyChar == (char)27) //si la tecla pesionada es igual a ESC (27)
            {
//                OpenGameSettings();
            }
            else
                if (e.KeyChar == (char)13 && btn_Submit.Enabled == true) //si la tecla pesionada es igual a ENTER (13)
            {
                // si el foco esta en exit entonces se da clic a Exit, pero si esta en otro lado, da clic en Submit
                if (btn_Exit.Focused == true)
                {
                    btn_Exit.PerformClick();
                }
                else
                {
                    btn_Submit.PerformClick();
                }
            }
            else
                if ((e.KeyChar == (char)49 || e.KeyChar == (char)97 || e.KeyChar == (char)65) && rbtn_a.Visible == true)
            {
                rbtn_a.Focus();
                rbtn_a.Checked = true;
                btn_Submit.Enabled = true;
            }
            else
                if ((e.KeyChar == (char)50 || e.KeyChar == (char)98 || e.KeyChar == (char)66) && rbtn_b.Visible == true)
            {
                rbtn_b.Focus();
                rbtn_b.Checked = true;
                btn_Submit.Enabled = true;
            }
            else
                if ((e.KeyChar == (char)51 || e.KeyChar == (char)99 || e.KeyChar == (char)67) && rbtn_c.Visible == true)
            {
                rbtn_c.Focus();
                rbtn_c.Checked = true;
                btn_Submit.Enabled = true;
            }
            else
                if ((e.KeyChar == (char)52 || e.KeyChar == (char)100 || e.KeyChar == (char)68) && rbtn_d.Visible == true)
            {
                rbtn_d.Focus();
                rbtn_d.Checked = true;
                btn_Submit.Enabled = true;
            }
            else
                if ((e.KeyChar == (char)45 || e.KeyChar == (char)47)) // signos '-' y '/' para el comodin 50%
            {
                if (startingTurn == 1 && click50_1 == 0)
                {
                    click50_1++;
                    pbx_50_MouseEnter(this, new EventArgs());
                    pbx_50_Click(this, new EventArgs());
                    Thread.Sleep(200);
                    pbx_50_MouseLeave(this, new EventArgs());
                }
                else
                    if (startingTurn == 2 && click50_2 == 0)
                {
                    click50_2++;
                    pbx_50_2_MouseEnter(this, new EventArgs());
                    pbx_50_2_Click(this, new EventArgs());
                    Thread.Sleep(200);
                    pbx_50_2_MouseLeave(this, new EventArgs());
                }
            }
            else
                if ((e.KeyChar == (char)42 || e.KeyChar == (char)43)) // signos '*' y '+' para el comodin Passage
            {
                if (objEntidad.pasage != "N/A" && objEntidad.pasage != "") // solo da click si existe algun pasaje
                {
                    if (startingTurn == 1 && clickPassage_1 == 0)
                    {
                        clickPassage_1++;
                        pbx_Passage_1_MouseEnter(this, new EventArgs());
                        pbx_Passage_1_Click(this, new EventArgs());
                        Thread.Sleep(200);
                        pbx_Passage_1_MouseLeave(this, new EventArgs());
                    }
                    else
                    if (startingTurn == 2 && clickPassage_2 == 0)
                    {
                        clickPassage_2++;
                        pbx_Passage_2_MouseEnter(this, new EventArgs());
                        pbx_Passage_2_Click(this, new EventArgs());
                        Thread.Sleep(200);
                        pbx_Passage_2_MouseLeave(this, new EventArgs());
                    }
                }
            }

        }
        void bloquear_Btn_Submit() // para hacer el submit solo si se ha elegido una respuesta
        {
            if (rbtn_a.Checked == true)
            {
                btn_Submit.Enabled = true;
            }
            else
                if (rbtn_b.Checked == true)
            {
                btn_Submit.Enabled = true;
            }
            else
                if (rbtn_c.Checked == true)
            {
                btn_Submit.Enabled = true;
            }
            else
                if (rbtn_d.Checked == true)
            {
                btn_Submit.Enabled = true;
            }
            else
            {
                btn_Submit.Enabled = false;
            }
        }
        

        private void rbtn_a_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
                btn_Submit.Enabled = true;

            if (rbtn_a.ForeColor == Color.FromArgb(228, 161, 24))
            {
                rbtn_a.Checked = false;
                btn_Submit.Enabled = false;
            }
        }
        private void rbtn_b_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
                btn_Submit.Enabled = true;

            if (rbtn_b.ForeColor == Color.FromArgb(228, 161, 24))
            {
                rbtn_b.Checked = false;
                btn_Submit.Enabled = false;
            }
        }
        private void rbtn_c_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
                btn_Submit.Enabled = true;

            if (rbtn_c.ForeColor == Color.FromArgb(228, 161, 24))
            {
                rbtn_c.Checked = false;
                btn_Submit.Enabled = false;
            }
        }
        private void rbtn_d_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
                btn_Submit.Enabled = true;

            if (rbtn_d.ForeColor == Color.FromArgb(228, 161, 24))
            {
                rbtn_d.Checked = false;
                btn_Submit.Enabled = false;
            }
        }

        // Tiempo para responder la pregunta----------------------------------------
        private void Timer_2Answer_Tick(object sender, EventArgs e)
        {
            if (countDownTimer2 != 0)
            {
                if (countDownTimer2 <= 3)
                {
                    reproducirSonido("countDown.wav", false);
                }

                lab_Anuncios.Text = Convert.ToString(countDownTimer2);
                countDownTimer2--;
            }
            else
            {
                countDownTimer2 = objEntidad.time2Answer;
                Timer_2Answer.Stop();

                lab_correctWrong(0);


                //Evaluar si perdio o gano--------------------------------------
                perder_Ganar();


                // cambio de turno si no ha terminado el juego
                if (opportunities_1 != 0
                && opportunities_2 != 0
                && ((countUp != noRepetir_PorDificultadyCategoria.Length) || (objEntidad.difficulty == "Todas"))
                && retry <= reintentos
                && ((objEntidad.questions2Answer != "Todas") && (enumerate <= Convert.ToInt32(objEntidad.questions2Answer))))
                {
                    cambioDeTurno(startingTurn, false);
                }


                // Cambio de Jugador------------------------------------------------
                if (startingTurn == 1)
                {
                    countDown.Start();
                    startingTurn = 2; //Player 2
                }
                else
                {
                    countDown.Start();
                    startingTurn = 1; //Player 1
                }
            }
        }
        private void RestartTimer_2Answer()
        {
            Timer_2Answer.Stop();
            countDownTimer2 = objEntidad.time2Answer;
        }

        private void P_focusedBibles_Activated(object sender, EventArgs e)
        {
            if (E_focusedBible.deSettings == true) // para saber si se acaba de salir de settings a pantalla de juego
            {
                Timer_2Answer.Start();

                if (sonido != null)
                {
                    sonido.PlayLooping();
                }

                E_focusedBible.deSettings = false; // desactivando ya que desde este momento no se acaba de salir
            }
        }

        private void Timer_Banner_Tick(object sender, EventArgs e)
        {

            if (countDownTimer3 != 0)
            {
                countDownTimer3--;
            }
            else
            {
                if (banner == "Round " + startingRound)  // solo se reproduce el sonido si es un cambio de round
                {
                    Banner.Hide();
                }
                countDownTimer3 = 3;
                Timer_Banner.Stop();
                StartAgan();
            }
        }
        
        private void pbx_Passage_1_MouseEnter(object sender, EventArgs e)
        {
            if (lab_Passage_1.Text != "0")
            {
                pbx_Passage_1.Image = Properties.Resources.Passage_Mouse_On;
            }
        }
        private void pbx_Passage_1_MouseLeave(object sender, EventArgs e)
        {
            pbx_Passage_1.Image = Properties.Resources.Passage_Mouse_Leave;
        }
        private void pbx_Passage_1_Click(object sender, EventArgs e)
        {
            if (lab_Passage_1.Text != "0")
            {
                countDownPassage_1--;
                lab_Passage_1.Text = comodinPassage_1[countDownPassage_1];
                ShowPassage(1);
                increaseTime2Answer(timeToIncrease); // incrementa 'x' segundos el time2Answer de ese turno
                pbx_Passage_1.Enabled = false;
            }
        }

        private void pbx_Passage_2_MouseEnter(object sender, EventArgs e)
        {
            if (lab_Passage_2.Text != "0")
            {
                pbx_Passage_2.Image = Properties.Resources.Passage_Mouse_On;
            }
        }
        private void pbx_Passage_2_MouseLeave(object sender, EventArgs e)
        {
            pbx_Passage_2.Image = Properties.Resources.Passage_Mouse_Leave;
        }
        private void pbx_Passage_2_Click(object sender, EventArgs e)
        {
            if (lab_Passage_2.Text != "0")
            {
                countDownPassage_2--;
                lab_Passage_2.Text = comodinPassage_2[countDownPassage_2];
                ShowPassage(2);
                increaseTime2Answer(timeToIncrease); // incrementa 'x' segundos el time2Answer de ese turno
                pbx_Passage_2.Enabled = false;
            }
        }

        void ShowPassage(int turno)
        {
            if (turno == 1)
            {
                Lab_Passage_Shown_1.Text = objEntidad.pasage;
                Lab_Passage_Shown_2.Text = "";
            }
            else
            {
                Lab_Passage_Shown_2.Text = objEntidad.pasage;
                Lab_Passage_Shown_1.Text = "";
            }
        }
        void activarPassage(int turno)
        {
            if (turno == 1)
            {
                pbx_Passage_2.Visible = false;
                lab_Passage_2.Visible = false;
                Lab_Passage_Shown_2.Visible = false;
                pbx_Passage_1.Visible = true;
                lab_Passage_1.Visible = true;
                Lab_Passage_Shown_1.Visible = true;
            }
            else
            {
                pbx_Passage_1.Visible = false;
                lab_Passage_1.Visible = false;
                Lab_Passage_Shown_1.Visible = false;
                pbx_Passage_2.Visible = true;
                lab_Passage_2.Visible = true;
                Lab_Passage_Shown_2.Visible = true;
            }
        }
        void increaseTime2Answer(int timeToIncrease) //aumenta el tiempo 'x' segundos al elegir el comodin Passage
        {
            countDownTimer2 += timeToIncrease;
        }

        void blockPassage()
        {
            if (objEntidad.pasage == "N/A" || objEntidad.pasage == "")
            {
                if (startingTurn == 1 && lab_Passage_1.Text != "0") // si es el turno 1 y no se han acabado
                {
                    Lab_Passage_Shown_1.Text = "";
                    pbx_Passage_1.Image = Properties.Resources.Passage_N_A;
                    //lab_Passage_1.Text = comodinPassage_1[countDownPassage_1];
                    pbx_Passage_1.Enabled = false;
                    lab_Passage_1.Enabled = false;
                }
                else
                if (startingTurn == 2 && lab_Passage_2.Text != "0") // si es el turno 2 y no se han acabado
                {
                    Lab_Passage_Shown_2.Text = "";
                    pbx_Passage_2.Image = Properties.Resources.Passage_N_A;
                    //lab_Passage_1.Text = comodinPassage_2[countDownPassage_2];
                    pbx_Passage_2.Enabled = false;
                    lab_Passage_2.Enabled = false;
                }
            }
        }

        private void btn_goToMain_Click(object sender, EventArgs e)
        {
            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

            existe.Show();
            Timer_2Answer.Stop();
            sonido.Stop();
            this.Close(); //Esto cierra la ventana del juego y va a Main
        }
    }
}
