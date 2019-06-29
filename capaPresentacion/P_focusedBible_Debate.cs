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
            objEntidad.finalResults = new string[2, 3];

            InitializeComponent();
        }


        #region Variables y Objetos
        int timeToIncrease = 15; // tiempo que incrementa al elegir el comodin pasage
        Banners Banner;
        P_Debate_Ganador Winner;
        HowToPlay howToPlay;
        SoundPlayer sonido;
        int?[] noRepetir_PorDificultadyCategoria; // para que no se repitan cuando se eligen solo x dificultad
        E_focusedBible[] lista_porDificultadYCategoria; // Para almacenar la lista completa y asi evitar que se repitan
        E_focusedBible objEntidad = new E_focusedBible();
        N_focusedBible objNego = new N_focusedBible();
        bool answerCorrect;
        int numeroPrueba;
        int codPregNoRepetir;
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
        int startingRound = 1; // ronda inicial
        int valueScore = 2; //valor de cada pregunta
        int startingTurn = 1; // turno inicial
        bool reboundTurn = false; // para saber si se está jugando la partida de rebote
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
        int usedPassageComodin_1 = 0; // acumular cantidad de comodines usados
        int usedPassageComodin_2 = 0; // acumular cantidad de comodines usados
        int used50Comodin_1 = 0; // acumular cantidad de comodines usados
        int used50Comodin_2 = 0; // acumular cantidad de comodines usados
        #endregion



        // Las siguentes dos funciones son para
        //evitar los problemas de Buffer por tener layouts transparentes
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        #endregion
        #region .. code for Flucuring ..

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion



        private void P_focusedBibles_Load(object sender, EventArgs e)
        {
            //Evita el Buffer lag al cargar la imagen de fondo
            SetDoubleBuffered(tableLayoutPanel13);
            SetDoubleBuffered(tableLayoutPanel15);
            SetDoubleBuffered(tableLayoutPanel16);
            SetDoubleBuffered(tableLayoutPanel17);
            SetDoubleBuffered(tableLayoutPanel18);
            SetDoubleBuffered(tableLayoutPanel19);
            SetDoubleBuffered(tableLayoutPanel20);
            SetDoubleBuffered(tableLayoutPanel21);
            SetDoubleBuffered(tableLayoutPanel22);
            SetDoubleBuffered(tableLayoutPanel23);
            SetDoubleBuffered(tableLayoutPanel24);
            SetDoubleBuffered(tableLayoutPanel25);
            SetDoubleBuffered(tlyo_Grupo1);
            SetDoubleBuffered(tableLayoutPanel27);
            SetDoubleBuffered(tlyo_Grupo2);
            SetDoubleBuffered(tableLayoutPanel29);
            SetDoubleBuffered(tlyo_Comodines_1);
            SetDoubleBuffered(tlyo_Comodines_2);

            this.BackgroundImage = Properties.Resources.Fondo_DUO_Jugador_1;
            this.BackgroundImageLayout = ImageLayout.Stretch;


            if (objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }

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
            objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
            objEntidad.difficulty = objEntidad.difficulty;
            objEntidad.catEvangelios_yOtros = objEntidad.catEvangelios_yOtros;
            objEntidad.catLibro = objEntidad.catLibro;
            objEntidad.catNuevoAntiguo = objEntidad.catNuevoAntiguo;
            noRepetir_PorDificultadyCategoria = new int?[objNego.N_NumFilas_PorDificultadYCategoria(objEntidad)]; // el valor devuelto es tamaño del numero de filas
            lista_porDificultadYCategoria = new E_focusedBible[objNego.N_NumFilas_PorDificultadYCategoria(objEntidad)];
            objEntidad.questions2Answer = noRepetir_PorDificultadyCategoria.Length.ToString(); // igualar questions2Answer a la cantidad de preguntas
            Llenar_listaPorDificultadYCategoria(objEntidad);
            listarFocusedBible(objEntidad);
            focoRbtn();
            bloquear_Btn_Submit();

            SetFinalResults(); // almacena los resultados finales de los jugadores


            //ocultar Oportunidades si no se evaluará en base a oportunidades
            if (objEntidad.opportunitiesBoolean == false)
            {
                lab_LifesNum.Visible = false;
                lab_LifesNum2.Visible = false;
                pbx_Opportunity_1.Visible = false;
                pbx_Opportunity_2.Visible = false;
            }
            else
            {
                lab_LifesNum.Visible = true;
                lab_LifesNum2.Visible = true;
                pbx_Opportunity_1.Visible = true;
                pbx_Opportunity_2.Visible = true;
            }

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

            perder_Ganar();

            while (true)
            {
                // numeros aleatorios desde el 1 hasta el tamaño del arreglo
                numeroPrueba = random2.Next(0, noRepetir_PorDificultadyCategoria.Length);
                codPregNoRepetir = lista_porDificultadYCategoria[numeroPrueba].codPreg;
                // si existe el código dentro del arreglo se agrega al arreglo, si no existe se crea el random
                if (Array.Exists(noRepetir_PorDificultadyCategoria, codPreg => codPreg == codPregNoRepetir))
                {
                    if ((countUp == noRepetir_PorDificultadyCategoria.Length && (objEntidad.difficulty != "Todas"))
                        || (enumerate > Convert.ToInt32(objEntidad.questions2Answer)))
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
                    noRepetir_PorDificultadyCategoria[i] = codPregNoRepetir; //agregar código al arreglo para que nunca se repitan
                    
                    i++;
                    countUp++;
                    break;
                }
            }

        }
        
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            RestartTimer_2Answer();

            pbx_50_1.Enabled = false;
            pbx_Passage_1.Enabled = false;
            pbx_50_2.Enabled = false;
            pbx_Passage_2.Enabled = false;

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

            cambioDeJugador();

            btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
            btn_Submit.Enabled = false;
        }
        
        void BannerStart(string banner)
        {
            Thread.Sleep(2000);
            Timer_2Answer.Stop();
            objEntidad.StopGameSound();
            this.banner = banner;
            Banner = new Banners(banner);
            Banner.Show();
            Timer_Banner.Start();

            if (banner == "Round " + startingRound)  // solo se reproduce el sonido si es un cambio de round
            {
                objEntidad.reproducirSonidoJuego("start-ready-go.wav", false);
            }
        }
        void BannerWinner()
        {
            Thread.Sleep(1000);
            objEntidad.reproducirSonidoJuego("finalSuccess.wav", false);
            Thread.Sleep(1000);
            Timer_2Answer.Stop();
            objEntidad.StopGameSound();
            Winner = new P_Debate_Ganador(objEntidad);
            Winner.ShowDialog();

            StartAgan();
        }
        void StartAgan()
        {
            Timer_2Answer.Stop();
            objEntidad.StopGameSound();

            if (Winner.DialogResult == DialogResult.OK)
            {
                restart = true;
                reset_PlayAgain();

                objEntidad.reproducirSonidoJuego("start-ready-go.wav", false);
            }
            else
            {
                //RESOLVER MACOOOOOOOOOOOOOOOOOOOOOOOOOOOOO DE QUE NO SE CIERRA Y SIGUE TRABAJANDOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                btn_goToMain.PerformClick();
                this.Close();
            }
        }
        void cambioDeJugador()
        {

            if (reboundTurn == false) // si no hay REBOTE. Por lo que se tiene que cambiar de turno
            {
                if (startingTurn == 1)
                {
                    startingTurn = 2; //Group 2
                }
                else
                {
                    startingTurn = 1; //Group 1
                }
            }

            countDown.Start();
        }
        void perder_Ganar()
        {
            //condicion para perder
            if ( opportunities_1 == 0
                || opportunities_2 == 0
                || ((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas"))
                || (enumerate > Convert.ToInt32(objEntidad.questions2Answer)) )
            {
                Timer_2Answer.Stop(); //detener conteo

                objEntidad.reproducirSonidoJuego("game-over.wav", false);


                if (startingRound == objEntidad.numRounds) // si es el ultimo round
                {
                    Thread.Sleep(1500);

                    //condicion para saber quien perdió
                    if (startingTurn == 1)
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        objEntidad.winner = lab_Group2.Text;
                        SetFinalResults();
                        BannerWinner();
                    }
                    else
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        objEntidad.winner = lab_Group1.Text;
                        SetFinalResults();
                        BannerWinner();
                    }
                }
                else
                    if (((countUp == noRepetir_PorDificultadyCategoria.Length))) // si se acaban las preguntas, se acaba el juego
                {
                    Thread.Sleep(1500);

                    //condicion para saber quien perdió
                    if (score_1 == score_2)
                    {
                        objEntidad.winner = "Es un empate!";
                        SetFinalResults();
                        BannerWinner();
                    }
                    else
                        if (score_2 > score_1)
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        objEntidad.winner = lab_Group2.Text;
                        SetFinalResults();
                        BannerWinner();
                    }
                    else
                    {
                        //MessageBox.Show(lab_Player1.Text + " Lose!\n\n" + lab_Player2.Text + " Wins\nLifes: " + lifes_2 + "\nScore: " + score_2);
                        objEntidad.winner = lab_Group1.Text;
                        SetFinalResults();
                        BannerWinner();
                    }
                }
                else
                {
                    ChangeRound();
                }
            }
        }
        void SetFinalResults()
        {
            // puntuacion grupo 1 y 2
            objEntidad.finalResults[0, 0] = lab_ScoreNum.Text;
            objEntidad.finalResults[1, 0] = lab_ScoreNum2.Text;

            // comodin pasage grupo 1 y 2
            objEntidad.finalResults[0, 1] = usedPassageComodin_1.ToString();
            objEntidad.finalResults[1, 1] = usedPassageComodin_2.ToString();

            // comodin 50% grupo 1 y 2
            objEntidad.finalResults[0, 2] = used50Comodin_1.ToString();
            objEntidad.finalResults[1, 2] = used50Comodin_2.ToString();
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
        // resetea todo para volver a jugar denuevo
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
                valueScore = 2; //valor de cada pregunta
                reboundTurn = false; // para saber si se está jugando la partida de rebote
                wins_01 = 0;
                wins_02 = 0;
                score_1 = 0;
                score_2 = 0;
                objEntidad.pasage = "";
                objEntidad.winner = ""; // resetear winner
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

            tlyo_Comodines_1.RowStyles[0].SizeType = SizeType.Percent;
            tlyo_Comodines_1.RowStyles[1].SizeType = SizeType.Percent;
            tlyo_Comodines_1.RowStyles[0].Height = 0;
            tlyo_Comodines_1.RowStyles[1].Height = 47;
            tlyo_Comodines_2.RowStyles[0].SizeType = SizeType.Percent;
            tlyo_Comodines_2.RowStyles[1].SizeType = SizeType.Percent;
            tlyo_Comodines_2.RowStyles[0].Height = 0;
            tlyo_Comodines_2.RowStyles[1].Height = 47;

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
            pbx_Passage_1.Image = Properties.Resources.BTN_PASAGE_Leave;
            pbx_Passage_2.Image = Properties.Resources.BTN_PASAGE_Leave;

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
                rbtn_a.ForeColor = Color.FromArgb(70, 172, 195);

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
                rbtn_b.ForeColor = Color.FromArgb(70, 172, 195);

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
                rbtn_c.ForeColor = Color.FromArgb(70, 172, 195);

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
                rbtn_d.ForeColor = Color.FromArgb(70, 172, 195);

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

                objEntidad.reproducirSonidoJuego("retro-lose.wav", false);
                lab_Anuncios.ForeColor = Color.FromArgb(222,79,49);
                lab_Anuncios.Text = "✘";

                if (opportunities_1 != 0
                && opportunities_2 != 0
                && ((countUp != noRepetir_PorDificultadyCategoria.Length) || (objEntidad.difficulty == "Todas"))
                && (enumerate <= Convert.ToInt32(objEntidad.questions2Answer)))
                {
                    answerCorrect = false;
                    cambioDeTurno(startingTurn);
                }

            }
            else
            {
                correctAnswer();
                correctAnswerSound();
                lab_Anuncios.ForeColor = Color.FromArgb(84,206,222);
                lab_Anuncios.Text = "✔";

                if (opportunities_1 != 0
                && opportunities_2 != 0
                && ((countUp != noRepetir_PorDificultadyCategoria.Length) || (objEntidad.difficulty == "Todas"))
                && (enumerate <= Convert.ToInt32(objEntidad.questions2Answer)))
                {
                    answerCorrect = true;
                    cambioDeTurno(startingTurn);
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
            objEntidad.reproducirSonidoJuego("correctAnswer3.wav", false);
            Thread.Sleep(400);
            objEntidad.reproducirSonidoJuego("cheering-and-clapping2.wav", false);
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

                //para poder cambiar el ;104;108tamaño de la fuente hay que instanciarla y pasarle los parametros siguientes.
        //        lab_Group1.Font = new Font(lab_Group1.Font.Name, 20, lab_Group1.Font.Style, lab_Group1.Font.Unit);
                //para cambiar el color a BLANCO
                lab_Group1.ForeColor = Color.White;
                lab_FijoGrupo1.ForeColor = Color.White;
                lab_Oportunidades1.ForeColor = Color.White;
                lab_Puntos1.ForeColor = Color.White;

                //        lab_Group2.Font = new Font(lab_Group2.Font.Name, 10, lab_Group2.Font.Style, lab_Group2.Font.Unit);
                //para cambiar el color a GRIS
                lab_Group2.ForeColor = Color.FromArgb(95,104,108);
                lab_FijoGrupo2.ForeColor = Color.FromArgb(95, 104, 108);
                lab_Oportunidades2.ForeColor = Color.FromArgb(95, 104, 108);
                lab_Puntos2.ForeColor = Color.FromArgb(95, 104, 108);

                cambiarColoryJugador(turno);

            }
            else // si el turno es 2
            {
                // si hay cambio de turno y pasage no es igual a N/A o "", Ni se ha acabado el comodin passage
                if (objEntidad.pasage != "N/A" && objEntidad.pasage != "" && lab_Passage_2.Text != "0")
                {
                    lab_Passage_2.Enabled = true;
                }

        //       lab_Group2.Font = new Font(lab_Group2.Font.Name, 20, lab_Group2.Font.Style, lab_Group2.Font.Unit);
                lab_Group2.ForeColor = Color.White;
                lab_FijoGrupo2.ForeColor = Color.White;
                lab_Oportunidades2.ForeColor = Color.White;
                lab_Puntos2.ForeColor = Color.White;

                //       lab_Group1.Font = new Font(lab_Group1.Font.Name, 10, lab_Group1.Font.Style, lab_Group1.Font.Unit);
                lab_Group1.ForeColor = Color.FromArgb(95,104,108);
                lab_FijoGrupo1.ForeColor = Color.FromArgb(95, 104, 108);
                lab_Oportunidades1.ForeColor = Color.FromArgb(95, 104, 108);
                lab_Puntos1.ForeColor = Color.FromArgb(95, 104, 108);

                cambiarColoryJugador(turno);

            }
        }
        void cambiarColoryJugador(int turno)
        {
            if (turno == 1)
            {
                this.BackgroundImage = Properties.Resources.Fondo_DUO_Jugador_1;
                
                pbx_Grupo1.BackgroundImage = Properties.Resources.Grupo1_ImagenDuo;

                pbx_Opportunity_1.BackgroundImage = Properties.Resources.Focused_bible_SOLO_04;
                lab_LifesNum.ForeColor = Color.White;
                pbx_Score_1.BackgroundImage = Properties.Resources.Focused_bible_SOLO_06;
                lab_ScoreNum.ForeColor = Color.White;
                tlyo_Wins_P1.Visible = true;



                
                pbx_Grupo2.BackgroundImage = Properties.Resources.Grupo2_ImagenDuo_OFF;

                pbx_Opportunity_2.BackgroundImage = Properties.Resources.Focused_bible_SOLO_04_OFF;
                lab_LifesNum2.ForeColor = Color.FromArgb(95,104,108);
                pbx_Score_2.BackgroundImage = Properties.Resources.Focused_bible_SOLO_06_OFF;
                lab_ScoreNum2.ForeColor = Color.FromArgb(95,104,108);
                tlyo_Wins_P2.Visible = false;
            }
            else // si el turno es 2
            {
                this.BackgroundImage = Properties.Resources.Fondo_DUO_Jugador_2;

                pbx_Grupo2.BackgroundImage = Properties.Resources.Grupo2_ImagenDuo;

                pbx_Opportunity_2.BackgroundImage = Properties.Resources.Focused_bible_SOLO_04;
                lab_LifesNum2.ForeColor = Color.White;
                pbx_Score_2.BackgroundImage = Properties.Resources.Focused_bible_SOLO_06;
                lab_ScoreNum2.ForeColor = Color.White;
                tlyo_Wins_P2.Visible = true;


                pbx_Grupo1.BackgroundImage = Properties.Resources.Grupo1_ImagenDuo_OFF;

                pbx_Opportunity_1.BackgroundImage = Properties.Resources.Focused_bible_SOLO_04_OFF;
                lab_LifesNum.ForeColor = Color.FromArgb(95,104,108);
                pbx_Score_1.BackgroundImage = Properties.Resources.Focused_bible_SOLO_06_OFF;
                lab_ScoreNum.ForeColor = Color.FromArgb(95,104,108);
                tlyo_Wins_P1.Visible = false;
            }
        }
        void cambioDeTurno(int turno)
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

            //Codigo principal del metodo cambio de turno
            if (turno == 1)
            {
                click50_2 = 0; // reiniciar a 0 para poder usar el comodin 50% en su proximo turno
                pbx_50_2.Enabled = true; // activar comodin anterior al cambiar de turno

                clickPassage_2 = 0; // reiniciar a 0 para poder usar el comodin Passage en su proximo turno
                pbx_Passage_2.Enabled = true; // activar comodin anterior al cambiar de turno
                Lab_Passage_Shown_2.Text = "";
                tlyo_Comodines_2.RowStyles[0].SizeType = SizeType.Percent;
                tlyo_Comodines_2.RowStyles[1].SizeType = SizeType.Percent;
                tlyo_Comodines_2.RowStyles[0].Height = 0;
                tlyo_Comodines_2.RowStyles[1].Height = 47;
                pbx_Passage_2.Image = Properties.Resources.BTN_PASAGE_Leave; // volver a cargar imagen inicial

                if (answerCorrect == true)
                {
                    score_1+=valueScore;
                    lab_ScoreNum.Text = Convert.ToString(score_1);
                }
                else
                {
                    opportunities_1--;
                    lab_LifesNum.Text = Convert.ToString(opportunities_1);
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
                tlyo_Comodines_1.RowStyles[0].SizeType = SizeType.Percent;
                tlyo_Comodines_1.RowStyles[1].SizeType = SizeType.Percent;
                tlyo_Comodines_1.RowStyles[0].Height = 0;
                tlyo_Comodines_1.RowStyles[1].Height = 47;
                pbx_Passage_1.Image = Properties.Resources.BTN_PASAGE_Leave; // volver a cargar imagen inicial

                if (answerCorrect == true)
                {
                    score_2+=valueScore;
                    lab_ScoreNum2.Text = Convert.ToString(score_2);
                }
                else
                {
                    opportunities_2--;
                    lab_LifesNum2.Text = Convert.ToString(opportunities_2);
                    perder_Ganar();
                }
            }
        }

        
        private void pbx_50_MouseEnter(object sender, EventArgs e)
        {
            if (lab_50_1.Text != "0")
            {
                pbx_50_1.Image = Properties.Resources.BTN_50_Move;
            }
        }
        private void pbx_50_MouseLeave(object sender, EventArgs e)
        {
            pbx_50_1.Image = Properties.Resources.BTN_50_Leave;
        }
        private void pbx_50_Click(object sender, EventArgs e)
        {
            if (lab_50_1.Text != "0")
            {
                used50Comodin_1++;
                countDownComodin_1--;
                lab_50_1.Text = comodin50_1[countDownComodin_1];
                random50();
                pbx_50_1.Enabled = false;
                focoRbtn();

                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
                btn_Submit.Enabled = false; //para evitar que se presione submit sin estar seleccionada ninguna respuesta
                uncheckRbtn();
            }
        }

        private void pbx_50_2_MouseEnter(object sender, EventArgs e)
        {
            if (lab_50_2.Text != "0")
            {
                pbx_50_2.Image = Properties.Resources.BTN_50_Move;
            }
        }
        private void pbx_50_2_MouseLeave(object sender, EventArgs e)
        {
            pbx_50_2.Image = Properties.Resources.BTN_50_Leave;
        }
        private void pbx_50_2_Click(object sender, EventArgs e)
        {
            if (lab_50_2.Text != "0")
            {
                used50Comodin_2++; // acumular cantidad de comodines usados
                countDownComodin_2--;
                lab_50_2.Text = comodin50_2[countDownComodin_2];
                random50();
                pbx_50_2.Enabled = false;
                focoRbtn();
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
                btn_Submit.Enabled = false; //para evitar que se presione submit sin estar seleccionada ninguna respuesta

                uncheckRbtn();
            }
        }
        void activarComidin(int turno)
        {
            if (turno == 1)
            {
                tlyo_Comodines.ColumnStyles[0].SizeType = SizeType.Percent;
                tlyo_Comodines.ColumnStyles[1].SizeType = SizeType.Percent;
                tlyo_Comodines.ColumnStyles[0].Width = 100;
                tlyo_Comodines.ColumnStyles[1].Width = 0;
            }
            else
            {
                tlyo_Comodines.ColumnStyles[0].SizeType = SizeType.Percent;
                tlyo_Comodines.ColumnStyles[1].SizeType = SizeType.Percent;
                tlyo_Comodines.ColumnStyles[0].Width = 0;
                tlyo_Comodines.ColumnStyles[1].Width = 100;
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
        
        // conteo para cambiar de turno
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

                
                if (((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas"))
                || (enumerate > Convert.ToInt32(objEntidad.questions2Answer)))
                {
                    perder_Ganar();
                }// si no se acabó el juego entonces reinicia ciertos elementos de los jugadores
                else if(objEntidad.winner != lab_Group1.Text && objEntidad.winner != lab_Group2.Text && objEntidad.winner != "Es un empate!")
                {
                     AfterCountDown();
                }
            }
        }

        void AfterCountDown()
        {

            Timer_2Answer.Start();


            answerOriginalColor(); // aqui cambian las respuestas al color original

            
            uncheckRbtn(); //desmarca las respuestas
            makeVisibleRbtn_andEnabled();//pone las resuestas visibles y abilitadas
            focoRbtn(); // se pone el foco las respuestas para poder seleccionarlas con el teclado

            bloquear_Btn_Submit();




            /**REBOTE**/
            if (objEntidad.rebound == true)
            {
                if (reboundTurn == true)
                {
                    if (startingTurn == 1)
                    {
                        activarComidin(1);
                        activarPassage(1);
                        PlayerFocus(1);
                        objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
                    }
                    else
                    {
                        activarComidin(2);
                        activarPassage(2);
                        PlayerFocus(2);
                        objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
                    }

                    valueScore = 2;
                    listarFocusedBible(objEntidad); //lista las preguntas y respuestas
                    reboundTurn = false; // salir del rebote a su turno real
;
                }
                else // si es el turno normal
                {
                    if (answerCorrect == true)
                    {
                        if (startingTurn == 1)
                        {
                            activarComidin(1);
                            activarPassage(1);
                            PlayerFocus(1);
                            objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
                        }
                        else
                        {
                            activarComidin(2);
                            activarPassage(2);
                            PlayerFocus(2);
                            objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
                        }

                        valueScore = 2;
                        listarFocusedBible(objEntidad); //lista las preguntas y respuestas
                    }
                    else
                    {
                        if (restart == false) // no es un reinicio
                        {
                            objEntidad.reproducirSonidoJuego("rebound.wav", false);
                            Thread.Sleep(100);

                            if (startingTurn == 1)
                            {
                                activarComidin(1);
                                activarPassage(1);
                                PlayerFocus(1);
                                objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
                            }
                            else
                            {
                                activarComidin(2);
                                activarPassage(2);
                                PlayerFocus(2);
                                objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
                            }

                            valueScore = 1;
                            reboundTurn = true;
                        }
                        else
                        {
                            listarFocusedBible(objEntidad); //lista las preguntas y respuestas
                        }
                    }
                }

            }//--------------------------------------------------------------------------
            else // si rebote está desactivado
            {


                if (startingTurn == 1)
                {
                    activarComidin(1);
                    activarPassage(1);
                    PlayerFocus(1);
                    objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
                }
                else
                {
                    activarComidin(2);
                    activarPassage(2);
                    PlayerFocus(2);
                    objEntidad.reproducirSonidoJuego("levelclearer.wav", true);
                }

                listarFocusedBible(objEntidad); //lista las preguntas y respuestas
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
                if (btn_goToMain.Focused == true)
                {
                    btn_goToMain.PerformClick();
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
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }
            else
                if ((e.KeyChar == (char)50 || e.KeyChar == (char)98 || e.KeyChar == (char)66) && rbtn_b.Visible == true)
            {
                rbtn_b.Focus();
                rbtn_b.Checked = true;
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }
            else
                if ((e.KeyChar == (char)51 || e.KeyChar == (char)99 || e.KeyChar == (char)67) && rbtn_c.Visible == true)
            {
                rbtn_c.Focus();
                rbtn_c.Checked = true;
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }
            else
                if ((e.KeyChar == (char)52 || e.KeyChar == (char)100 || e.KeyChar == (char)68) && rbtn_d.Visible == true)
            {
                rbtn_d.Focus();
                rbtn_d.Checked = true;
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
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
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }
            else
                if (rbtn_b.Checked == true)
            {
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }
            else
                if (rbtn_c.Checked == true)
            {
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }
            else
                if (rbtn_d.Checked == true)
            {
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }
            else
            {
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
                btn_Submit.Enabled = false;
            }
        }
        

        private void rbtn_a_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
            {
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }

            if (rbtn_a.ForeColor == Color.FromArgb(70, 172, 195))
            {
                rbtn_a.Checked = false;
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
                btn_Submit.Enabled = false;
            }
        }
        private void rbtn_b_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
            {
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }

            if (rbtn_b.ForeColor == Color.FromArgb(70, 172, 195))
            {
                rbtn_b.Checked = false;
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
                btn_Submit.Enabled = false;
            }
        }
        private void rbtn_c_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
            {
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }

            if (rbtn_c.ForeColor == Color.FromArgb(70, 172, 195))
            {
                rbtn_c.Checked = false;
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
                btn_Submit.Enabled = false;
            }
        }
        private void rbtn_d_CheckedChanged(object sender, EventArgs e)
        {
            if (btn_Submit.Enabled == false)
            {
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
                btn_Submit.Enabled = true;
            }

            if (rbtn_d.ForeColor == Color.FromArgb(70, 172, 195))
            {
                rbtn_d.Checked = false;
                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
                btn_Submit.Enabled = false;
            }
        }

        // Tiempo para responder la pregunta----------------------------------------
        private void Timer_2Answer_Tick(object sender, EventArgs e)
        {
            lab_Anuncios.ForeColor = Color.White;

            if (countDownTimer2 != 0)
            {
                if (countDownTimer2 <= 3)
                {
                    objEntidad.reproducirSonidoJuego("countDown.wav", false);
                }

                lab_Anuncios.Text = Convert.ToString(countDownTimer2);
                countDownTimer2--;
            }
            else
            {
                countDownTimer2 = objEntidad.time2Answer;
                Timer_2Answer.Stop();

                lab_correctWrong(0);


                // Cambio de Jugador------------------------------------------------
                cambioDeJugador();
            }
        }
        private void RestartTimer_2Answer()
        {
            Timer_2Answer.Stop();
            countDownTimer2 = objEntidad.time2Answer;
        }

        private void P_focusedBibles_Activated(object sender, EventArgs e)
        {
            //Timer_2Answer.Start();

            //if (sonido != null)
            //{
            //    sonido.PlayLooping();
            //}
          
        }

        private void Timer_Banner_Tick(object sender, EventArgs e)
        {
            if (countDownTimer3 != 0)
            {
                countDownTimer3--;
            }
            else
            {

                countDownTimer3 = 3;

                if (banner == "Round " + startingRound)  // solo se reproduce el sonido si es un cambio de round
                {
                    Timer_Banner.Stop();
                    Banner.Hide();
                }

            }
        }
        
        private void pbx_Passage_1_MouseEnter(object sender, EventArgs e)
        {
            if (lab_Passage_1.Text != "0")
            {
                pbx_Passage_1.Image = Properties.Resources.BTN_PASAGE_Move;
            }
        }
        private void pbx_Passage_1_MouseLeave(object sender, EventArgs e)
        {
            pbx_Passage_1.Image = Properties.Resources.BTN_PASAGE_Leave;
        }
        private void pbx_Passage_1_Click(object sender, EventArgs e)
        {
            if (lab_Passage_1.Text != "0")
            {
                usedPassageComodin_1++; // acumular cantidad de comodines usados
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
                pbx_Passage_2.Image = Properties.Resources.BTN_PASAGE_Move;
            }
        }
        private void pbx_Passage_2_MouseLeave(object sender, EventArgs e)
        {
            pbx_Passage_2.Image = Properties.Resources.BTN_PASAGE_Leave;
        }
        private void pbx_Passage_2_Click(object sender, EventArgs e)
        {
            if (lab_Passage_2.Text != "0")
            {
                usedPassageComodin_2++; // acumular cantidad de comodines usados
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

                tlyo_Comodines_1.RowStyles[0].SizeType = SizeType.Percent;
                tlyo_Comodines_1.RowStyles[1].SizeType = SizeType.Percent;
                tlyo_Comodines_1.RowStyles[0].Height = 47;
                tlyo_Comodines_1.RowStyles[1].Height = 0;
            }
            else
            {
                Lab_Passage_Shown_2.Text = objEntidad.pasage;
                Lab_Passage_Shown_1.Text = "";

                tlyo_Comodines_2.RowStyles[0].SizeType = SizeType.Percent;
                tlyo_Comodines_2.RowStyles[1].SizeType = SizeType.Percent;
                tlyo_Comodines_2.RowStyles[0].Height = 47;
                tlyo_Comodines_2.RowStyles[1].Height = 0;
            }
        }
        void activarPassage(int turno)
        {
            if (turno == 1)
            {
                tlyo_Comodines.ColumnStyles[0].SizeType = SizeType.Percent;
                tlyo_Comodines.ColumnStyles[1].SizeType = SizeType.Percent;
                tlyo_Comodines.ColumnStyles[0].Width = 100;
            }
            else
            {
                tlyo_Comodines.ColumnStyles[0].SizeType = SizeType.Percent;
                tlyo_Comodines.ColumnStyles[1].SizeType = SizeType.Percent;
                tlyo_Comodines.ColumnStyles[0].Width = 0;
                tlyo_Comodines.ColumnStyles[1].Width = 100;
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
                    pbx_Passage_1.Image = Properties.Resources.BTN_PASAGE_Disabled;
                    //lab_Passage_1.Text = comodinPassage_1[countDownPassage_1];
                    pbx_Passage_1.Enabled = false;
                    lab_Passage_1.Enabled = false;
                }
                else
                if (startingTurn == 2 && lab_Passage_2.Text != "0") // si es el turno 2 y no se han acabado
                {
                    Lab_Passage_Shown_2.Text = "";
                    pbx_Passage_2.Image = Properties.Resources.BTN_PASAGE_Disabled;
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

            this.Close(); //Esto cierra la ventana del juego y va a Main
            existe.Show();
            Timer_2Answer.Stop();
            objEntidad.StopGameSound();
            reboundTurn = false;
        }

        private void btn_Submit_MouseEnter(object sender, EventArgs e)
        {
            btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Enter;
        }

        private void btn_Submit_MouseLeave(object sender, EventArgs e)
        {
            btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Leave;
        }

        private void pbx_Sound_Click(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                objEntidad.StopGameSound();
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_OFF;
                objEntidad.enableGameSound = false;
                objEntidad.enableButtonSound = false;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_ON;
                objEntidad.enableGameSound = true;
                objEntidad.enableButtonSound = true;
                objEntidad.reproducirSonidoJuego("levelclearer.wav", true);

            }
        }

        private void pbx_Sound_MouseEnter(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseEnter_OFF;
            }
        }

        private void pbx_Sound_MouseLeave(object sender, EventArgs e)
        {
            if (objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }
        }

        private void btn_how2Play_Click(object sender, EventArgs e)
        {
            howToPlay = new HowToPlay();
            howToPlay.ShowDialog();
        }

        private void btn_how2Play_MouseEnter(object sender, EventArgs e)
        {
            btn_how2Play.BackgroundImage = Properties.Resources.Focused_bible_landing_03_MOUSE_ENTER;
        }

        private void btn_how2Play_MouseLeave(object sender, EventArgs e)
        {
            btn_how2Play.BackgroundImage = Properties.Resources.Focused_bible_landing_03_1;
        }

        private void btn_goToMain_MouseEnter(object sender, EventArgs e)
        {
            btn_goToMain.BackgroundImage = Properties.Resources.Focused_bible_SOLO_07_MouseEnter;
        }

        private void btn_goToMain_MouseLeave(object sender, EventArgs e)
        {
            btn_goToMain.BackgroundImage = Properties.Resources.Focused_bible_SOLO_07;
        }
    }
}
