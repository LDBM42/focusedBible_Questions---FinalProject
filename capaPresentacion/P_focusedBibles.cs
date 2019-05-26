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
    public partial class P_focusedBibles : Form
    {
                
        public P_focusedBibles(string player1 = "Player One", string player2 = "Player Two",
            int numRounds = 0, int time2Answer = 20, int Rounds = 1, string difficulty = "Normal")
        {
            this.difficulty = difficulty;
            this.player1 = player1;
            this.player2 = player2;
            this.Rounds = Rounds;
            this.numRounds = numRounds;
            this.time2Answer = time2Answer;
            InitializeComponent();
        }


        #region Variables y Objetos
        int timeToIncrease = 15;
        string player1;
        string player2;
        int numRounds;
        int time2Answer;
        SoundPlayer sonido;
        int?[] noRepetir;
        int?[] noRepetir_PorDificultad; // para que no se repitan cuando se eligen solo x dificultad
        E_focusedBible[] lista_porDificultad; // Para almacenar la lista completa y asi evitar que se repitan
        int numeroPrueba;
        int click50_1 = 0; // para saber si el jugador 1 ya ha entrado al evento click de el comodin 50%
        int click50_2 = 0; // para saber si el jugador 1 ya ha entrado al evento click de el comodin 50%
        int clickPassage_1 = 0; // para saber si el jugador 1 ya ha entrado al evento click de el Passage_1
        int clickPassage_2 = 0; // para saber si el jugador 1 ya ha entrado al evento click de el Passage_1
        string passage = "";
        int i = 0;
        int countUp = 0;
        int countDownTimer = 3;
        int countDownTimer2;
        int countDownTimer3 = 2;
        int score_1 = 0;
        int lifes_1 = 3;
        int score_2 = 0;
        int lifes_2 = 3;
        int turno = 1;
        int round = 1;
        int Rounds;
        string difficulty;
        string banner;
        int wins_01 = 0;
        int wins_02 = 0;
        bool restart = false;
        int enumerate = 1; // para ponerle número a las preguntas
        string[] comodin50_1 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodin50_2 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage_1 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage_2 = new string[] { "0", "+1", "+2", "+3" };
        char[] desaparecer50 = new char[] { 'a', 'b', 'c', 'd' };
        int countDownComodin_1 = 3;
        int countDownComodin_2 = 3;
        int countDownPassage_1 = 3;
        int countDownPassage_2 = 3;
        Banners Banner;
        Settings settings = new Settings();
        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        HowToPlay howToPlay;
        #endregion



        private void P_focusedBibles_Load(object sender, EventArgs e)
        {
            
            this.AddOwnedForm(settings); //indica que este va a ser el papa del form settings
            lab_Wins_P1.Text = Convert.ToString(wins_01);
            lab_Wins_P2.Text = Convert.ToString(wins_02);
            tlyo_Wins_P1.Visible = true;
            tlyo_Wins_P2.Visible = false;
            lab_Rounds_Left.Text = round + "/" + Rounds;
            lab_Rounds_Right.Text = round + "/" + Rounds;
            lab_Difficulty.Text = difficulty;
            lab_LifesNum.Text = Convert.ToString(lifes_1);
            lab_LifesNum2.Text = Convert.ToString(lifes_2);
            lab_Player1.Text = player1;
            lab_Player2.Text = player2;
            countDownTimer2 = time2Answer;
            Timer_2Answer.Start();
            banner = "Round" + round;
            reproducirSonido("levelclearer.wav", true);
            objEntidad.dificultad = difficulty;
            noRepetir = new int?[objNego.N_NumFilas()]; // el tamaño es el tamaño del numero de filas
            noRepetir_PorDificultad = new int?[objNego.N_NumFilas_PorDificultad(objEntidad)]; // el tamaño es el tamaño del numero de filas
            lista_porDificultad = new E_focusedBible[objNego.N_NumFilas_PorDificultad(objEntidad)];
            Llenar_listaPorDificultad(objEntidad);
            listarFocusedBible(objEntidad);
            focoRbtn();
            bloquear_Btn_Submit();
        }

        void Llenar_listaPorDificultad(E_focusedBible dificultad)
        {
            DataTable dt2 = objNego.N_listadoPor_Dificultad(dificultad);

            // llena todos los atributos de cada objeto del arreglo, creando así una copia de la tabla
            for (int i = 0; i < noRepetir_PorDificultad.Length; i++)
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
                dificultad.dificultad = dt2.Rows[i]["dificultad"].ToString();

                lista_porDificultad[i] = dificultad;
            }

        }
        void listarFocusedBible(E_focusedBible preg)
        {
            if (difficulty == "All")
            {
                randomQuestions();

                DataTable dt = objNego.N_listado(preg);

                if (dt.Rows.Count > 0)
                {
                    lab_Pregunta.Text = Convert.ToString(enumerate) + ". " + dt.Rows[0]["preg"].ToString();
                    rbtn_a.Text = "a)   " + dt.Rows[0]["a"].ToString();
                    rbtn_b.Text = "b)   " + dt.Rows[0]["b"].ToString();
                    rbtn_c.Text = "c)   " + dt.Rows[0]["c"].ToString();
                    rbtn_d.Text = "d)   " + dt.Rows[0]["d"].ToString();
                    preg.resp = Convert.ToChar(dt.Rows[0]["resp"].ToString());
                    passage = dt.Rows[0]["pasage"].ToString();
                }
            }
            else
            {
                randomQuestions_PorDificultad();

                lab_Pregunta.Text = Convert.ToString(enumerate) + ". " + lista_porDificultad[numeroPrueba].preg;
                rbtn_a.Text = "a)   " + lista_porDificultad[numeroPrueba].a;
                rbtn_b.Text = "b)   " + lista_porDificultad[numeroPrueba].b;
                rbtn_c.Text = "c)   " + lista_porDificultad[numeroPrueba].c;
                rbtn_d.Text = "d)   " + lista_porDificultad[numeroPrueba].d;
                preg.resp = lista_porDificultad[numeroPrueba].resp;
                passage = lista_porDificultad[numeroPrueba].pasage;
            }

            bockPassage(); //si no existe pasage de referencia, se bloquea este comodin
            enumerate++;
        }

        void randomQuestions()
        {
            Random random = new Random();

            if (countUp == noRepetir.Length)  // si se acaban las preguntas, se acaba el juego
            {
                DialogResult respuesta = MessageBox.Show("The Game has Finished!\nDo you want to Play Again!", "Game Over", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    restart = true;
                    reset_PlayAgain();
                    Banner.Hide();
                }
                else
                {
                    Application.Exit();
                }
            }

            while (true)
            {
                // numeros aleatorios desde el 1 hasta el tamaño del arreglo
                numeroPrueba = random.Next(1, noRepetir.Length + 1);
                // si existe el código dentro del arreglo se agrega al arreglo, si no existe se crea el random
                if (Array.Exists(noRepetir, codPreg => codPreg == numeroPrueba))
                {
                    if (countUp == noRepetir.Length)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else //si el código no eiste en el arreglo
                {
                    noRepetir[i] = numeroPrueba; //agregar código al arreglo para que nunca se repitan
                    objEntidad.codPreg = numeroPrueba; // numeros aleatorios del 1 al numero de filas
                    i++;
                    countUp++;
                    break;
                }
            }
        }
        void randomQuestions_PorDificultad()
        {
            Random random2 = new Random();

            perder_Ganar();

            while (true)
            {
                // numeros aleatorios desde el 1 hasta el tamaño del arreglo
                numeroPrueba = random2.Next(0, noRepetir_PorDificultad.Length);
                // si existe el código dentro del arreglo se agrega al arreglo, si no existe se crea el random
                if (Array.Exists(noRepetir_PorDificultad, codPreg => codPreg == numeroPrueba))
                {
                    if (countUp == noRepetir_PorDificultad.Length)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else //si el código no eiste en el arreglo
                {
                    noRepetir_PorDificultad[i] = numeroPrueba; //agregar código al arreglo para que nunca se repitan

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


            // Cambio de Jugador
            if (turno == 1)
            {
                countDown.Start();

                turno = 2; //Player 2
            }
            else
            {
                countDown.Start();
                turno = 1; //Player 1
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
            Thread.Sleep(2000);
            Timer_2Answer.Stop();
            sonido.Stop();
            this.banner = banner;
            Banner = new Banners(banner);
            Banner.Show();
            Timer_Banner.Start();
            if (banner == "Round " + round)  // solo se reproduce el sonido si es un cambio de round
            {
                reproducirSonido("start-ready-go.wav", false);
            }
        }
        void StartAgan()
        {
            if ((round == Rounds && banner != "Round " + round) || ((countUp == noRepetir_PorDificultad.Length) && (difficulty != "All"))) // significa que el juego ha terminado
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
            if (lifes_1 == 0 || lifes_2 == 0 || ((countUp == noRepetir_PorDificultad.Length) && (difficulty != "All")))
            {
                Timer_2Answer.Stop(); //detener conteo
                reproducirSonido("game-over.wav", false);


                if (round == Rounds) // si es el ultimo round
                {
                    Thread.Sleep(1500);

                    //condicion para saber quien perdió
                    if (turno == 1)
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        BannerStart(lab_Player2.Text + " Wins");
                    }
                    else
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        BannerStart(lab_Player1.Text + " Wins");
                    }
                }
                else
                    if (((countUp == noRepetir_PorDificultad.Length) && (difficulty != "All"))) // si se acaban las preguntas, se acaba el juego
                {
                    Thread.Sleep(1500);

                    //condicion para saber quien perdió
                    if (score_1 == score_2)
                    {
                        BannerStart("It's a Draw!");
                    }
                    else
                        if (score_2 > score_1)
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        BannerStart(lab_Player2.Text + " Wins");
                    }
                    else
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        BannerStart(lab_Player1.Text + " Wins");
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
            round++;
            lab_Rounds_Left.Text = round + "/" + Rounds;
            lab_Rounds_Right.Text = round + "/" + Rounds;

            if (turno == 1)
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
            BannerStart("Round " + round);
        }
        void reset_PlayAgain()
        {
            Timer_2Answer.Stop(); //detener conteo
            countDownTimer = 3;
            countDownTimer2 = time2Answer;
            countDownTimer3 = 2;

            if (restart == true)
            {
                restart = false;

                i = 0;
                countUp = 0;
                enumerate = 1; // para ponerle número a las preguntas
                round = 1;
                turno = 1;
                wins_01 = 0;
                wins_02 = 0;
                score_1 = 0;
                score_2 = 0;
                passage = "";
                lab_Rounds_Left.Text = round + "/" + Rounds;
                lab_Rounds_Right.Text = round + "/" + Rounds;
                lab_Wins_P1.Text = Convert.ToString(wins_01);
                lab_Wins_P2.Text = Convert.ToString(wins_02);

                Array.Clear(noRepetir, 0, noRepetir.Length); // vaciar arreglo
                Array.Clear(noRepetir_PorDificultad, 0, noRepetir_PorDificultad.Length); // vaciar arreglo
            }

            lifes_1 = 3;
            lifes_2 = 3;
            countDownComodin_1 = 3;
            countDownComodin_2 = 3;
            countDownPassage_1 = 3;
            countDownPassage_2 = 3;
            Lab_Passage_Shown_1.Text = "";

            Timer_2Answer.Start();

            if (banner == lab_Player1.Text + " Wins" || banner == lab_Player2.Text + " Wins" || banner == "It's a Draw!")
            {
                AfterCountDown(true);
                banner = "Round" + round; // resetear banner
            }



            lab_LifesNum.Text = Convert.ToString(lifes_1);
            lab_LifesNum2.Text = Convert.ToString(lifes_2);

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

                reproducirSonido("retro-lose.wav", false);
                lab_Anuncios.ForeColor = Color.Brown;
                lab_Anuncios.Text = "Wrong Answer";

                if (countUp != noRepetir_PorDificultad.Length)
                {
                    cambioDeTurno(turno, false);
                }

            }
            else
            {
                correctAnswer();
                correctAnswerSound();
                lab_Anuncios.ForeColor = Color.FromArgb(228, 161, 24);
                lab_Anuncios.Text = "Correct Answer";

                if (countUp != noRepetir_PorDificultad.Length)
                {
                    cambioDeTurno(turno, true);
                }
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
                if (passage != "N/A" && passage != "" && lab_Passage_1.Text != "0")
                {
                    lab_Passage_1.Enabled = true;
                }

                //para poder cambiar el tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
                lab_Player1.Font = new Font(lab_Player1.Font.Name, 20, lab_Player1.Font.Style, lab_Player1.Font.Unit);
                //para cambiar el color a gris
                lab_Player1.ForeColor = Color.FromArgb(228, 161, 24);

                lab_Player2.Font = new Font(lab_Player2.Font.Name, 10, lab_Player2.Font.Style, lab_Player2.Font.Unit);
                //para cambiar el color a orange
                lab_Player2.ForeColor = Color.FromArgb(237, 237, 237);


                cambiarColoryJugador(turno);

                Timer_2Answer.Start();

            }
            else // si el turno es 2
            {
                // si hay cambio de turno y pasage no es igual a N/A o "", Ni se ha acabado el comodin passage
                if (passage != "N/A" && passage != "" && lab_Passage_2.Text != "0")
                {
                    lab_Passage_2.Enabled = true;
                }

                lab_Player2.Font = new Font(lab_Player2.Font.Name, 20, lab_Player2.Font.Style, lab_Player2.Font.Unit);
                lab_Player2.ForeColor = Color.FromArgb(228, 161, 24);

                lab_Player1.Font = new Font(lab_Player1.Font.Name, 10, lab_Player1.Font.Style, lab_Player1.Font.Unit);
                lab_Player1.ForeColor = Color.FromArgb(237, 237, 237);

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

            //Codigo del metodo
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
                    lifes_1--;
                    lab_LifesNum.Text = Convert.ToString(lifes_1);
                    perder_Ganar();
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
                    lifes_2--;
                    lab_LifesNum2.Text = Convert.ToString(lifes_2);
                    perder_Ganar();
                }
            }
        }
        
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult salir;
            if (turno == 1)
            {
                salir = MessageBox.Show(lab_Player1.Text + ", Are Sure you want to Exit?", "Warning", MessageBoxButtons.YesNo);
            }
            else
            {
                salir = MessageBox.Show(lab_Player2.Text + ", Are Sure you want to Exit?", "Warning", MessageBoxButtons.YesNo);
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
                if (objEntidad.resp != desaparecer50[indice])
                {
                    if (desaparecer50[indice] == 'a')
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
                        if (desaparecer50[indice] == 'b')
                    {
                        if (rbtn_b.Visible == true)
                        {
                            rbtn_b.Enabled = false;
                            rbtn_b.Visible = false;
                            i++;
                        }
                    }
                    else
                        if (desaparecer50[indice] == 'c')
                    {
                        if (rbtn_c.Visible == true)
                        {
                            rbtn_c.Enabled = false;
                            rbtn_c.Visible = false;
                            i++;
                        }
                    }
                    else
                        if (desaparecer50[indice] == 'd')
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

                if (banner == lab_Player1.Text + " Wins" || banner == lab_Player2.Text + " Wins")
                {
                }
                else
                    if (((countUp == noRepetir_PorDificultad.Length) && (difficulty != "All")))
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


            if (turno == 1)
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
                OpenSettings();
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
                if (turno == 1 && click50_1 == 0)
                {
                    click50_1++;
                    pbx_50_MouseEnter(this, new EventArgs());
                    pbx_50_Click(this, new EventArgs());
                    Thread.Sleep(200);
                    pbx_50_MouseLeave(this, new EventArgs());
                }
                else
                    if (turno == 2 && click50_2 == 0)
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
                if (passage != "N/A" && passage != "") // solo da click si existe algun pasaje
                {
                    if (turno == 1 && clickPassage_1 == 0)
                    {
                        clickPassage_1++;
                        pbx_Passage_1_MouseEnter(this, new EventArgs());
                        pbx_Passage_1_Click(this, new EventArgs());
                        Thread.Sleep(200);
                        pbx_Passage_1_MouseLeave(this, new EventArgs());
                    }
                    else
                    if (turno == 2 && clickPassage_2 == 0)
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
        }
        private void rbtn_b_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
                btn_Submit.Enabled = true;
        }
        private void rbtn_c_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
                btn_Submit.Enabled = true;
        }
        private void rbtn_d_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
                btn_Submit.Enabled = true;
        }
        

        private void Btn_Settings_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }
        private void OpenSettings()
        {
            settings.numRounds = Rounds;
            settings.difficulty = difficulty;
            settings.time2Answer = time2Answer;
            // mostrar los nombres que estan jugando
            settings.tbx_Player1.Text = lab_Player1.Text;
            settings.tbx_Player2.Text = lab_Player2.Text;

            Timer_2Answer.Stop();
            sonido.Stop();

            try
            {   // para saber si el formulario existe, o sea si está abierto o cerrado
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "Settings").SingleOrDefault<Form>();

                if (existe != null)

                {
                    settings.ShowDialog();
                }
            }
            catch (Exception)
            {
                settings.ShowDialog();
            }

        }
        private void Btn_Settings_MouseEnter(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Settings_MouseUp;
        }
        private void Btn_Settings_MouseLeave(object sender, EventArgs e)
        {
            Btn_Settings.Image = Properties.Resources.Settings;
        }
        

        private void Timer_2Answer_Tick(object sender, EventArgs e)
        {
            if (countDownTimer2 != 0)
            {
                lab_Anuncios.Text = Convert.ToString(countDownTimer2);
                countDownTimer2--;
            }
            else
            {
                countDownTimer2 = time2Answer;
                Timer_2Answer.Stop();

                lab_correctWrong(0);

                // Cambio de Jugador
                if (turno == 1)
                {
                    countDown.Start();
                    turno = 2; //Player 2
                }
                else
                {
                    countDown.Start();
                    turno = 1; //Player 1
                }
            }
        }
        private void RestartTimer_2Answer()
        {
            Timer_2Answer.Stop();
            countDownTimer2 = time2Answer;
        }

        private void P_focusedBibles_Activated(object sender, EventArgs e)
        {
            Timer_2Answer.Start();

            if (sonido != null)
            {
                sonido.PlayLooping();
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
                if (banner == "Round " + round)  // solo se reproduce el sonido si es un cambio de round
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
                Lab_Passage_Shown_1.Text = passage;
                Lab_Passage_Shown_2.Text = "";
            }
            else
            {
                Lab_Passage_Shown_2.Text = passage;
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

        void bockPassage()
        {
            if (passage == "N/A" || passage == "")
            {
                if (turno == 1 && lab_Passage_1.Text != "0") // si es el turno 1 y no se han acabado
                {
                    Lab_Passage_Shown_1.Text = "";
                    pbx_Passage_1.Image = Properties.Resources.Passage_N_A;
                    //lab_Passage_1.Text = comodinPassage_1[countDownPassage_1];
                    pbx_Passage_1.Enabled = false;
                    lab_Passage_1.Enabled = false;
                }
                else
                if (turno == 2 && lab_Passage_2.Text != "0") // si es el turno 2 y no se han acabado
                {
                    Lab_Passage_Shown_2.Text = "";
                    pbx_Passage_2.Image = Properties.Resources.Passage_N_A;
                    //lab_Passage_1.Text = comodinPassage_2[countDownPassage_2];
                    pbx_Passage_2.Enabled = false;
                    lab_Passage_2.Enabled = false;
                }
            }
        }

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = HowToPlay.GetInscance();
            howToPlay.Show();
            howToPlay.BringToFront();

            Timer_2Answer.Stop();
            sonido.Stop();
        }

    }
}
