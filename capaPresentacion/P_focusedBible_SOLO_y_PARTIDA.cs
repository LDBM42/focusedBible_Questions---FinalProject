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
    public partial class P_focusedBible_SOLO_y_PARTIDA : Form
    {
        public P_focusedBible_SOLO_y_PARTIDA(E_focusedBible Configuracion, E_Alumnos Alumno = null)
        {
            objEntidad = Configuracion;
            objEntidadAlumno = Alumno;

            opportunities = objEntidad.opportunities;
            objEntidad.finalResultsSOLO = new string[4];

            InitializeComponent();
        }


        N_Listener objNegoListener = new N_Listener();//-------------------------------------------
        #region Variables y Objetos
        int timeToIncrease = 15; // tiempo que incrementa al elegir el comodin pasage
        Banners Banner;
        P_SOLO_Marcador FinalScore;
        HowToPlay howToPlay;
        SoundPlayer sonido;
        int?[] noRepetir_PorDificultadyCategoria; // para que no se repitan cuando se eligen solo x dificultad
        E_focusedBible[] lista_porDificultadYCategoria; // Para almacenar la lista completa y asi evitar que se repitan
        E_focusedBible objEntidad = new E_focusedBible();
        E_Alumnos objEntidadAlumno = new E_Alumnos();
        N_focusedBible objNego = new N_focusedBible();
        N_AlumnoPartida objNegoAlumno = new N_AlumnoPartida();
        DataSet ds;
        DataTable dt;
        DataTable dtListar;

        bool answerCorrect;
        int numeroPrueba;
        int codPregNoRepetir;
        int click50 = 0; // para saber si el jugador ya ha entrado al evento click de el comodin 50%
        int clickPassage = 0; // para saber si el jugador ya ha entrado al evento click de el Passage
        int totalComodins = 0; // total de comodines usados
        int i = 0;
        int countUp = 0;
        int countDownTimer = 3;
        int countDownTimer2;
        int countDownTimer3 = 2;
        int CountTimePerAnswers = 0; // indica el tiempo que se tomó en contestar todas las preguntas
        int score = 0; // puntuacion inicial
        int wrongAnswer = 0; // Respuestas incorrectas
        int opportunities;
        int startingRound = 1; // ronda inicial
        int valueScore = 2; //valor de cada pregunta
        string banner;
        int wins = 0;
        bool restart = false;
        int enumerate = 1; // para ponerle número a las preguntas
        string[] comodin50 = new string[] { "0", "+1", "+2", "+3" };
        string[] comodinPassage = new string[] { "0", "+1", "+2", "+3" };
        char[] disappear50 = new char[] { 'a', 'b', 'c', 'd' };
        int countDownComodin = 3;
        int countDownPassage = 3;
        int usedPassageComodin = 0; // acumular cantidad de comodines usados
        int used50Comodin = 0; // acumular cantidad de comodines usados
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
            SetDoubleBuffered(tlyo_OportunidadesyPuntos_1);
            SetDoubleBuffered(tlyo_Comodines_1);

            this.BackgroundImage = Properties.Resources.Fondo_SOLO;
            this.BackgroundImageLayout = ImageLayout.Stretch;


            if (objEntidad.enableGameSound == true)
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_ON;
            }
            else
            {
                pbx_Sound.BackgroundImage = Properties.Resources.Sound_MouseLeave_OFF;
            }

            lab_Rounds_Left.Text = startingRound + "/" + objEntidad.numRounds;
            lab_Difficulty.Text = objEntidad.difficulty;
            // asignar categorias y en caso de no haber ninguna colocar Todas ----------------------
            lab_Categoria.Text = objEntidad.categories2Show == null || objEntidad.categories2Show == "" ? "Todas" : objEntidad.categories2Show;
            lab_LifesNum.Text = Convert.ToString(opportunities);
            lab_Jugador.Text = E_Usuario.Nombreusuario;
            countDownTimer2 = objEntidad.time2Answer;
            Timer_2Answer.Start();
            banner = "Ronda" + startingRound;
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


            dtListar = objNegoAlumno.N_listado();

            //ocultar Oportunidades si no se evaluará en base a oportunidades
            if (objEntidad.opportunitiesBoolean == false)
            {
                lab_LifesNum.Visible = false;
                pbx_Opportunity_1.Visible = false;
                lab_Oportunidades1.Visible = false;
                tlyo_OportunidadesyPuntos_1.RowStyles[0].SizeType = SizeType.Percent;
                tlyo_OportunidadesyPuntos_1.RowStyles[1].SizeType = SizeType.Percent;
                tlyo_OportunidadesyPuntos_1.RowStyles[0].Height = 0;
                tlyo_OportunidadesyPuntos_1.RowStyles[1].Height = 100;

                lab_Jugador.TextAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                lab_LifesNum.Visible = true;
                pbx_Opportunity_1.Visible = true;
                lab_Oportunidades1.Visible = true;
                tlyo_OportunidadesyPuntos_1.RowStyles[0].SizeType = SizeType.Percent;
                tlyo_OportunidadesyPuntos_1.RowStyles[1].SizeType = SizeType.Percent;
                tlyo_OportunidadesyPuntos_1.RowStyles[0].Height = 40;
                tlyo_OportunidadesyPuntos_1.RowStyles[1].Height = 60;

                lab_Jugador.TextAlign = ContentAlignment.MiddleRight;
            }


            //optener el id del alumno
            if (dtListar.Rows.Count > 0)
            {
                objEntidadAlumno.Id = Convert.ToInt32(dtListar.Rows[0]["Id"]);
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

            pbx_50.Enabled = false;
            pbx_Passage.Enabled = false;

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

            countDown.Start();

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

            if (banner == "Ronda " + startingRound)  // solo se reproduce el sonido si es un cambio de ronda
            {
                objEntidad.reproducirSonidoJuego("start-ready-go.wav", false);
            }
        }
        void BannerFinalScore()
        {
            Thread.Sleep(1000);
            objEntidad.reproducirSonidoJuego("finalSuccess.wav", false);
            Thread.Sleep(1000);
            Timer_2Answer.Stop();
            objEntidad.StopGameSound();
            objEntidad.winner = E_Usuario.Nombreusuario;
            FinalScore = new P_SOLO_Marcador(objEntidad);
            FinalScore.ShowDialog();

            StartAgan();
        }
        void StartAgan()
        {
            Timer_2Answer.Stop();
            objEntidad.StopGameSound();

            if (FinalScore.DialogResult == DialogResult.OK)
            {
                restart = true;
                reset_PlayAgain();
                restart = true;

                lista_porDificultadYCategoria = new E_focusedBible[objNego.N_NumFilas_PorDificultadYCategoria(objEntidad)];
                Llenar_listaPorDificultadYCategoria(objEntidad);

                objEntidad.reproducirSonidoJuego("start-ready-go.wav", false);
            }
            else
            {
                restart = true;
                reset_PlayAgain();
                restart = true;
                btn_goToMain.PerformClick();
            }
        }


        void FinDelJuego()
        {
            //condicion para terminarse el juego
            if ( opportunities == 0
                || ((countUp == noRepetir_PorDificultadyCategoria.Length) && (objEntidad.difficulty != "Todas"))
                || (enumerate > Convert.ToInt32(objEntidad.questions2Answer)) )
            {
                Timer_2Answer.Stop(); //detener conteo

                objEntidad.reproducirSonidoJuego("game-over.wav", false);


                if (startingRound == objEntidad.numRounds || (countUp == noRepetir_PorDificultadyCategoria.Length)) // si es el ultimo ronda
                {
                    Thread.Sleep(1500);

                    //condicion para la puntuacion del jugador
                    SetFinalResults();

                    if (objEntidad.solo_O_Partida == "PARTIDA")
                    {
                        objEntidadAlumno.Terminado = "True";
                        objNegoAlumno.N_Editar(objEntidadAlumno);

                    }
                    else
                    {
                        BannerFinalScore();
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
            // puntuacion 
            objEntidad.finalResultsSOLO[0] = lab_ScoreNum.Text;

            // respuestas incorrectas
            objEntidad.finalResultsSOLO[1] = wrongAnswer.ToString();
            
            // tiempo total
            objEntidad.finalResultsSOLO[2] = CountTimePerAnswers.ToString();

            totalComodins = usedPassageComodin + used50Comodin; // calcula el total de comodines usados
            // comodines totales
            objEntidad.finalResultsSOLO[3] = Convert.ToString(totalComodins);
        }
        void ChangeRound()
        {
            startingRound++;
            lab_Rounds_Left.Text = startingRound + "/" + objEntidad.numRounds;

            reset_PlayAgain();
            BannerStart("Ronda " + startingRound);

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
                valueScore = 2; //valor de cada pregunta
                wins = 0;
                score = 0;
                wrongAnswer = 0;
                objEntidad.pasage = "";
                objEntidad.winner = ""; // resetear winner
                lab_Rounds_Left.Text = startingRound + "/" + objEntidad.numRounds;
                //resetear cantidad total de comodines
                totalComodins = 0;
                usedPassageComodin = 0;
                used50Comodin = 0;
                CountTimePerAnswers = 0; 

                // vaciar arreglos
                Array.Clear(noRepetir_PorDificultadyCategoria, 0, noRepetir_PorDificultadyCategoria.Length);
                Array.Clear(objEntidad.finalResultsSOLO, 0, objEntidad.finalResultsSOLO.Length);
            }

            opportunities = objEntidad.opportunities;
            countDownComodin = 3;
            countDownPassage = 3;
            Lab_Passage_Shown.Text = "";

            lab_LifesNum.Text = Convert.ToString(opportunities);

            lab_ScoreNum.Text = Convert.ToString(score);

            lab_50.Text = "+3";
            pbx_50.Enabled = true;
            lab_50.Enabled = true;

            lab_Passage.Text = "+3";
            pbx_Passage.Enabled = true;
            lab_Passage.Enabled = true;
            pbx_Passage.Image = Properties.Resources.BTN_PASAGE_Leave;
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

                if (opportunities != 0
                && ((countUp != noRepetir_PorDificultadyCategoria.Length) || (objEntidad.difficulty == "Todas"))
                && (enumerate <= Convert.ToInt32(objEntidad.questions2Answer)))
                {
                    answerCorrect = false;
                    cambioDePreguntas();
                }

            }
            else
            {
                correctAnswer();
                correctAnswerSound();
                lab_Anuncios.ForeColor = Color.FromArgb(84,206,222);
                lab_Anuncios.Text = "✔";

                if (opportunities != 0
                && ((countUp != noRepetir_PorDificultadyCategoria.Length) || (objEntidad.difficulty == "Todas"))
                && (enumerate <= Convert.ToInt32(objEntidad.questions2Answer)))
                {
                    answerCorrect = true;
                    cambioDePreguntas();
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
        void PlayerFocus()
        {
            // si pasage no es igual a N/A o "", Ni se ha acabado el comodin passage
            if (objEntidad.pasage != "N/A" && objEntidad.pasage != "" && lab_Passage.Text != "0")
            {
                lab_Passage.Enabled = true;
            }

            click50 = 0; // reiniciar a 0 para poder usar el comodin 50% en su proximo turno
            pbx_50.Enabled = true; // activar comodin anterior al cambiar de turno
            lab_50.Enabled = true;

            clickPassage = 0; // reiniciar a 0 para poder usar el comodin Passage en su proximo turno
            pbx_Passage.Enabled = true; // activar comodin anterior al cambiar de turno
            lab_Passage.Enabled = true;
            Lab_Passage_Shown.Text = "";
            tlyo_Comodines_1.RowStyles[0].SizeType = SizeType.Percent;
            tlyo_Comodines_1.RowStyles[1].SizeType = SizeType.Percent;
            tlyo_Comodines_1.RowStyles[0].Height = 0;
            tlyo_Comodines_1.RowStyles[1].Height = 47;
            pbx_Passage.Image = Properties.Resources.BTN_PASAGE_Leave; // volver a cargar imagen inicial
        }
        void cambioDePreguntas()
        {
            // para desactivar el 50% si ya se ha acabado
            if (lab_50.Text == "0")
            {
                lab_50.Enabled = false;
                pbx_50.Enabled = false;
            }

            // para desactivar el comodinPassage si ya se ha acabado
            if (lab_Passage.Text == "0")
            {
                lab_Passage.Enabled = false;
                pbx_Passage.Enabled = false;
            }



            if (answerCorrect == true)
            {
                score+=valueScore;
                lab_ScoreNum.Text = Convert.ToString(score);
            }
            else
            {
                opportunities--;
                wrongAnswer++; // Respuestas incorrectas

                lab_LifesNum.Text = Convert.ToString(opportunities);
                FinDelJuego();
            }
        }

        
        private void pbx_50_MouseEnter(object sender, EventArgs e)
        {
            if (lab_50.Text != "0")
            {
                pbx_50.Image = Properties.Resources.BTN_50_Move;
            }
        }
        private void pbx_50_MouseLeave(object sender, EventArgs e)
        {
            pbx_50.Image = Properties.Resources.BTN_50_Leave;
        }
        private void pbx_50_Click(object sender, EventArgs e)
        {
            if (lab_50.Text != "0")
            {
                used50Comodin++;
                countDownComodin--;
                lab_50.Text = comodin50[countDownComodin];
                random50();
                pbx_50.Enabled = false;
                lab_50.Enabled = false;
                focoRbtn();

                btn_Submit.BackgroundImage = Properties.Resources.RESPONDER_Disabled;
                btn_Submit.Enabled = false; //para evitar que se presione submit sin estar seleccionada ninguna respuesta
                uncheckRbtn();
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
                    FinDelJuego();
                    Thread.Sleep(2000);
                    AfterCountDown();
                }// si no se acabó el juego entonces reinicia ciertos elementos de los jugadores
                else if(objEntidad.winner != lab_Jugador.Text)
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


            //resetear color original de la pregunta
            lab_Pregunta.ForeColor = Color.FromArgb(32, 52, 61);

            PlayerFocus();
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
            CountTimePerAnswers += 1; // contar el tiempo total para responder cada pregunta

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


                countDown.Start();
            }
        }
        private void RestartTimer_2Answer()
        {
            Timer_2Answer.Stop();
            countDownTimer2 = objEntidad.time2Answer;
        }

        private void P_focusedBibles_Activated(object sender, EventArgs e)
        {
            if (objEntidad.solo_O_Partida == "PARTIDA")
            {
                ds = objNegoListener.N_Listener_Comando(1);
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Comando"].ToString() == "stop")
                    {
                        btn_goToMain.PerformClick();
                    }
                }
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

                countDownTimer3 = 3;

                if (banner == "Ronda " + startingRound)  // solo se reproduce el sonido si es un cambio de ronda
                {
                    Timer_Banner.Stop();
                    Banner.Hide();
                }

            }
        }
        
        private void pbx_Passage_1_MouseEnter(object sender, EventArgs e)
        {
            if (lab_Passage.Text != "0")
            {
                pbx_Passage.Image = Properties.Resources.BTN_PASAGE_Move;
            }
        }
        private void pbx_Passage_1_MouseLeave(object sender, EventArgs e)
        {
            pbx_Passage.Image = Properties.Resources.BTN_PASAGE_Leave;
        }
        private void pbx_Passage_1_Click(object sender, EventArgs e)
        {
            if (lab_Passage.Text != "0")
            {
                usedPassageComodin++; // acumular cantidad de comodines usados
                countDownPassage--;
                lab_Passage.Text = comodinPassage[countDownPassage];
                ShowPassage(1);
                increaseTime2Answer(timeToIncrease); // incrementa 'x' segundos el time2Answer de ese turno
                pbx_Passage.Enabled = false;
            }
        }

        void ShowPassage(int turno)
        {
            Lab_Passage_Shown.Text = objEntidad.pasage;

            tlyo_Comodines_1.RowStyles[0].SizeType = SizeType.Percent;
            tlyo_Comodines_1.RowStyles[1].SizeType = SizeType.Percent;
            tlyo_Comodines_1.RowStyles[0].Height = 47;
            tlyo_Comodines_1.RowStyles[1].Height = 0;
        }
        void increaseTime2Answer(int timeToIncrease) //aumenta el tiempo 'x' segundos al elegir el comodin Passage
        {
            countDownTimer2 += timeToIncrease;
        }

        void blockPassage()
        {
            if (objEntidad.pasage == "N/A" || objEntidad.pasage == "")
            {
                if (lab_Passage.Text != "0") // si es el turno 1 y no se han acabado
                {
                    Lab_Passage_Shown.Text = "";
                    pbx_Passage.Image = Properties.Resources.BTN_PASAGE_Disabled;
                    pbx_Passage.Enabled = false;
                    lab_Passage.Enabled = false;
                }
            }
        }

        private void btn_goToMain_Click(object sender, EventArgs e)
        {
            if (objEntidad.solo_O_Partida == "PARTIDA")
            {
                objNegoAlumno.N_EliminarAlumno(objEntidadAlumno); // eliminar alumno de la partida por salir
            }

            // para saber si el formulario existe, o sea, si está abierto o cerrado
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "P_Main").SingleOrDefault<Form>();
            
            Timer_2Answer.Stop();
            objEntidad.StopGameSound();

            this.Close(); //Esto cierra la ventana del juego y va a Main
            existe.Show();
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
