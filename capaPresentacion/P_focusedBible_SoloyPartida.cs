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
    public partial class P_focusedBible_SoloyPartida : Form
    {
        public P_focusedBible_SoloyPartida(E_focusedBible Configuracion)
        {
            objEntidad = Configuracion;

            opportunities = objEntidad.opportunities;

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
        bool answerCorrect;
        int numeroPrueba;
        int codPregNoRepetir;
        int click50 = 0; // para saber si el jugador(es) 1 ya ha entrado al evento click de el comodin 50%
        int clickPassage = 0; // para saber si el jugador(es) 1 ya ha entrado al evento click de el Passage_1
        int i = 0;
        int countUp = 0;
        int countDownTimer = 3;
        int countDownTimer2;
        int countDownTimer3 = 2;
        int score = 0; // puntuacion inicial
        int valueScore = 2; //valor de cada pregunta
        int opportunities;
        int startingTurn = 1; // turno inicial
        int startingRound = 1; // ronda inicial
        string banner;
        int wins = 0;
        bool restart = false;
        int enumerate = 1; // para ponerle número a las preguntas
        string[] comodin50 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage = new string[] { "0", "+1", "+2", "+3" };
        char[] disappear50 = new char[] { 'a', 'b', 'c', 'd' };
        int countDownComodin = 3;
        int countDownPassage = 3;
        #endregion



        private void P_focusedBibles_Load(object sender, EventArgs e)
        {
            lab_Wins_P1.Text = Convert.ToString(wins);
            tlyo_Wins_P1.Visible = true;
            lab_Rounds_Left.Text = startingRound + "/" + objEntidad.numRounds;
            lab_Difficulty.Text = objEntidad.difficulty;
            lab_Categoria.Text = objEntidad.catEvangelios_yOtros[0];
            lab_LifesNum.Text = Convert.ToString(opportunities);

            lab_Group1.Text = E_Usuario.Nombreusuario;

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


            //ocultar Oportunidades si no se evaluará en base a oportunidades
            if (objEntidad.opportunitiesBoolean == false)
            {
                lab_LifesNum.Visible = false;
                lab_Lifes.Visible = false;
            }
            else
            {
                lab_LifesNum.Visible = true;
                lab_Lifes.Visible = true;
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


            cambioDePregunta();
            countDown.Start();

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
        void StartAgan()
        {
            if ((startingRound == objEntidad.numRounds && banner != "Round " + startingRound)
                || ((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas"))
                || (enumerate > Convert.ToInt32(objEntidad.questions2Answer))) // significa que el juego ha terminado
            {
                Timer_2Answer.Stop();
                objEntidad.StopGameSound();

                DialogResult respuesta = MessageBox.Show("The Game has Finished!\nDo you want to Play Again?", "Game Over", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {

                    objEntidad.reproducirSonidoJuego("start-ready-go.wav", false);
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
            if ( opportunities == 0
                || ((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas"))
                || (enumerate > Convert.ToInt32(objEntidad.questions2Answer)) )
            {
                Timer_2Answer.Stop(); //detener conteo
                objEntidad.reproducirSonidoJuego("game-over.wav", false);
                
                if (startingRound == objEntidad.numRounds) // si es el ultimo round
                {
                    Thread.Sleep(1500);

                    BannerStart("Juego Terminado");
                }
                else
                    if (((countUp == noRepetir_PorDificultadyCategoria.Length))) // si se acaban las preguntas, se acaba el juego
                {
                    Thread.Sleep(1500);

                    BannerStart("Juego Terminado");
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

            wins++;
            lab_Wins_P1.Text = Convert.ToString(wins);

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
                wins = 0;
                score = 0;
                objEntidad.pasage = "";
                lab_Rounds_Left.Text = startingRound + "/" + objEntidad.numRounds;
                lab_Wins_P1.Text = Convert.ToString(wins);

                Array.Clear(noRepetir_PorDificultadyCategoria, 0, noRepetir_PorDificultadyCategoria.Length); // vaciar arreglo
            }

            opportunities = objEntidad.opportunities;
            countDownComodin = 3;
            countDownPassage = 3;
            Lab_Passage_Shown_1.Text = "";

            Timer_2Answer.Start();


            if (banner == "Juego Terminado")
            {
                AfterCountDown(true);
                banner = "Round" + startingRound; // resetear banner
            }



            lab_LifesNum.Text = Convert.ToString(opportunities);

            lab_ScoreNum.Text = Convert.ToString(score);

            lab_50_1.Text = "+3";
            pbx_50_1.Enabled = true;
            lab_50_1.Enabled = true;

            lab_Passage_1.Text = "+3";
            pbx_Passage_1.Enabled = true;
            lab_Passage_1.Enabled = true;
            pbx_Passage_1.Image = Properties.Resources.Passage_Mouse_Leave;



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

                objEntidad.reproducirSonidoJuego("retro-lose.wav", false);
                lab_Anuncios.ForeColor = Color.Brown;
                lab_Anuncios.Text = "Wrong Answer";

                if (opportunities != 0
                && ((countUp != noRepetir_PorDificultadyCategoria.Length) || (objEntidad.difficulty == "Todas"))
                && (enumerate <= Convert.ToInt32(objEntidad.questions2Answer)))
                {
                    answerCorrect = false;
                }

            }
            else
            {
                correctAnswer();
                correctAnswerSound();
                lab_Anuncios.ForeColor = Color.FromArgb(228, 161, 24);
                lab_Anuncios.Text = "Correct Answer";

                if (opportunities != 0
                && ((countUp != noRepetir_PorDificultadyCategoria.Length) || (objEntidad.difficulty == "Todas"))
                && (enumerate <= Convert.ToInt32(objEntidad.questions2Answer)))
                {
                    answerCorrect = true;
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
        void cambioDePregunta()
        {
            // para desactivar el 50% si ya se ha acabado
            if (lab_50_1.Text == "0")
            {
                lab_50_1.Enabled = false;
                pbx_50_1.Enabled = false;
            }

            // para desactivar el comodinPassage si ya se ha acabado
            if (lab_Passage_1.Text == "0")
            {
                lab_Passage_1.Enabled = false;
                pbx_Passage_1.Enabled = false;
            }

            //Codigo principal del metodo cambio de turno
            if (answerCorrect == true)
            {
                score+=valueScore;
                lab_ScoreNum.Text = Convert.ToString(score);
            }
            else
            {
                opportunities--;
                lab_LifesNum.Text = Convert.ToString(opportunities);
                perder_Ganar();
            }
            
            click50 = 0; // reiniciar a 0 para poder usar el comodin 50% en su proximo turno
            pbx_50_1.Enabled = true; // activar comodin anterior al cambiar de turno

            clickPassage = 0; // reiniciar a 0 para poder usar el comodin Passage en su proximo turno
            pbx_Passage_1.Enabled = true; // activar comodin anterior al cambiar de turno
            Lab_Passage_Shown_1.Text = "";
            pbx_Passage_1.Image = Properties.Resources.Passage_Mouse_Leave; // volver a cargar imagen inicial

        }
        
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult salir;
            salir = MessageBox.Show(lab_Group1.Text + ", Are Sure you want to Exit?", "Warning", MessageBoxButtons.YesNo);
            

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
                countDownComodin--;
                lab_50_1.Text = comodin50[countDownComodin];
                random50();
                pbx_50_1.Enabled = false;
                focoRbtn();

                btn_Submit.Enabled = false; //para evitar que se presione submit sin estar seleccionada ninguna respuesta
                uncheckRbtn();
            }
        }
        void activarComidin()
        {
            pbx_50_1.Visible = true;
            lab_50_1.Visible = true;
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
                else if(banner != "Juego Terminado")
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

            
            uncheckRbtn(); //desmarca las respuestas
            makeVisibleRbtn_andEnabled();//pone las resuestas visibles y abilitadas
            focoRbtn(); // se pone el foco las respuestas para poder seleccionarlas con el teclado

            bloquear_Btn_Submit();

            activarComidin();
            activarPassage();
            objEntidad.reproducirSonidoJuego("levelclearer.wav", true);

            listarFocusedBible(objEntidad); //lista las preguntas y respuestas
            
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
                if (click50 == 0)
                {
                    click50++;
                    pbx_50_MouseEnter(this, new EventArgs());
                    pbx_50_Click(this, new EventArgs());
                    Thread.Sleep(200);
                    pbx_50_MouseLeave(this, new EventArgs());
                }
            }
            else
                if ((e.KeyChar == (char)42 || e.KeyChar == (char)43)) // signos '*' y '+' para el comodin Passage
            {
                if (objEntidad.pasage != "N/A" && objEntidad.pasage != "") // solo da click si existe algun pasaje
                {
                    if (clickPassage == 0)
                    {
                        clickPassage++;
                        pbx_Passage_1_MouseEnter(this, new EventArgs());
                        pbx_Passage_1_Click(this, new EventArgs());
                        Thread.Sleep(200);
                        pbx_Passage_1_MouseLeave(this, new EventArgs());
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
            }
        }
        private void RestartTimer_2Answer()
        {
            Timer_2Answer.Stop();
            countDownTimer2 = objEntidad.time2Answer;
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

               if (banner == "Round " + startingRound)  // solo se reproduce el sonido si es un cambio de round
                {
                    Banner.Hide();
                }
                else
                {
                    countDownTimer3 = 3;
                    Timer_Banner.Stop();
                    StartAgan();
                }

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
                countDownPassage--;
                lab_Passage_1.Text = comodinPassage[countDownPassage];
                ShowPassage();
                increaseTime2Answer(timeToIncrease); // incrementa 'x' segundos el time2Answer de ese turno
                pbx_Passage_1.Enabled = false;
            }
        }
        
        void ShowPassage()
        {
            Lab_Passage_Shown_1.Text = objEntidad.pasage;
        }
        void activarPassage()
        {
            pbx_Passage_1.Visible = true;
            lab_Passage_1.Visible = true;
            Lab_Passage_Shown_1.Visible = true;
        }
        void increaseTime2Answer(int timeToIncrease) //aumenta el tiempo 'x' segundos al elegir el comodin Passage
        {
            countDownTimer2 += timeToIncrease;
        }

        void blockPassage()
        {
            if (objEntidad.pasage == "N/A" || objEntidad.pasage == "")
            {
                if (lab_Passage_1.Text != "0") // si no se han acabado
                {
                    Lab_Passage_Shown_1.Text = "";
                    pbx_Passage_1.Image = Properties.Resources.Passage_N_A;
                    pbx_Passage_1.Enabled = false;
                    lab_Passage_1.Enabled = false;
                }
            }
        }

        private void btn_goToMain_Click(object sender, EventArgs e)
        {
            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();

            existe.Show();
            Timer_2Answer.Stop();
            objEntidad.StopGameSound();
            this.Close(); //Esto cierra la ventana del juego y va a Main
        }
    }
}
