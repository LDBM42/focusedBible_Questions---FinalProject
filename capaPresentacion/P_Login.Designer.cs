namespace capaPresentacion
{
    partial class P_Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_Login));
            this.timerUser = new System.Windows.Forms.Timer(this.components);
            this.timerFondoLogo = new System.Windows.Forms.Timer(this.components);
            this.pnl_fondoLogo = new System.Windows.Forms.Panel();
            this.lbl_CountDown = new System.Windows.Forms.Label();
            this.text_Password = new System.Windows.Forms.TextBox();
            this.text_Usuario = new System.Windows.Forms.TextBox();
            this.llab_nuevoUsuario = new System.Windows.Forms.LinkLabel();
            this.lbl_Login = new System.Windows.Forms.Label();
            this.timerPassword = new System.Windows.Forms.Timer(this.components);
            this.pbx_Usuario = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.pbx_Sound = new System.Windows.Forms.PictureBox();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.cbx_autoLogin = new System.Windows.Forms.CheckBox();
            this.pnl_fondoLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Usuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Sound)).BeginInit();
            this.SuspendLayout();
            // 
            // timerUser
            // 
            this.timerUser.Tick += new System.EventHandler(this.timerUser_Tick);
            // 
            // timerFondoLogo
            // 
            this.timerFondoLogo.Interval = 30;
            this.timerFondoLogo.Tick += new System.EventHandler(this.timerFondoLogo_Tick);
            // 
            // pnl_fondoLogo
            // 
            this.pnl_fondoLogo.BackColor = System.Drawing.Color.White;
            this.pnl_fondoLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_fondoLogo.Controls.Add(this.lbl_CountDown);
            this.pnl_fondoLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_fondoLogo.Location = new System.Drawing.Point(0, 0);
            this.pnl_fondoLogo.Name = "pnl_fondoLogo";
            this.pnl_fondoLogo.Size = new System.Drawing.Size(287, 396);
            this.pnl_fondoLogo.TabIndex = 13;
            this.pnl_fondoLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_Azul_MouseDown);
            // 
            // lbl_CountDown
            // 
            this.lbl_CountDown.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CountDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CountDown.Location = new System.Drawing.Point(13, 23);
            this.lbl_CountDown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_CountDown.Name = "lbl_CountDown";
            this.lbl_CountDown.Size = new System.Drawing.Size(68, 41);
            this.lbl_CountDown.TabIndex = 2;
            this.lbl_CountDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_CountDown.Visible = false;
            // 
            // text_Password
            // 
            this.text_Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.text_Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_Password.Font = new System.Drawing.Font("Catamaran", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.text_Password.Location = new System.Drawing.Point(318, 249);
            this.text_Password.Margin = new System.Windows.Forms.Padding(4);
            this.text_Password.Name = "text_Password";
            this.text_Password.Size = new System.Drawing.Size(408, 37);
            this.text_Password.TabIndex = 1;
            this.text_Password.Text = "CONTRASEÑA";
            this.text_Password.Click += new System.EventHandler(this.text_Password_Click);
            this.text_Password.TextChanged += new System.EventHandler(this.text_Password_TextChanged);
            this.text_Password.Enter += new System.EventHandler(this.text_Password_Enter);
            this.text_Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Password_KeyPress);
            this.text_Password.MouseEnter += new System.EventHandler(this.text_Password_MouseEnter);
            this.text_Password.MouseLeave += new System.EventHandler(this.text_Password_MouseLeave);
            // 
            // text_Usuario
            // 
            this.text_Usuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.text_Usuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_Usuario.Font = new System.Drawing.Font("Catamaran", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_Usuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.text_Usuario.Location = new System.Drawing.Point(318, 185);
            this.text_Usuario.Margin = new System.Windows.Forms.Padding(4);
            this.text_Usuario.Name = "text_Usuario";
            this.text_Usuario.Size = new System.Drawing.Size(408, 37);
            this.text_Usuario.TabIndex = 0;
            this.text_Usuario.Text = "USUARIO";
            this.text_Usuario.Click += new System.EventHandler(this.text_Usuario_Click);
            this.text_Usuario.Enter += new System.EventHandler(this.text_Usuario_Enter);
            this.text_Usuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Usuario_KeyPress);
            this.text_Usuario.MouseEnter += new System.EventHandler(this.text_Usuario_MouseEnter);
            this.text_Usuario.MouseLeave += new System.EventHandler(this.text_Usuario_MouseLeave);
            // 
            // llab_nuevoUsuario
            // 
            this.llab_nuevoUsuario.ActiveLinkColor = System.Drawing.Color.Brown;
            this.llab_nuevoUsuario.Font = new System.Drawing.Font("Catamaran", 10F);
            this.llab_nuevoUsuario.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llab_nuevoUsuario.LinkColor = System.Drawing.Color.DodgerBlue;
            this.llab_nuevoUsuario.Location = new System.Drawing.Point(390, 357);
            this.llab_nuevoUsuario.Name = "llab_nuevoUsuario";
            this.llab_nuevoUsuario.Size = new System.Drawing.Size(253, 30);
            this.llab_nuevoUsuario.TabIndex = 4;
            this.llab_nuevoUsuario.TabStop = true;
            this.llab_nuevoUsuario.Text = "CREAR UN USUARIO";
            this.llab_nuevoUsuario.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.llab_nuevoUsuario.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llab_nuevoUsuario_LinkClicked);
            // 
            // lbl_Login
            // 
            this.lbl_Login.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Login.Font = new System.Drawing.Font("Catamaran ExtraBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(143)))), ((int)(((byte)(163)))));
            this.lbl_Login.Location = new System.Drawing.Point(383, 113);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(267, 41);
            this.lbl_Login.TabIndex = 19;
            this.lbl_Login.Text = "LOGIN";
            this.lbl_Login.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerPassword
            // 
            this.timerPassword.Tick += new System.EventHandler(this.timerPassword_Tick);
            // 
            // pbx_Usuario
            // 
            this.pbx_Usuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbx_Usuario.Image = global::capaPresentacion.Properties.Resources.Usuario;
            this.pbx_Usuario.Location = new System.Drawing.Point(449, 19);
            this.pbx_Usuario.Margin = new System.Windows.Forms.Padding(4);
            this.pbx_Usuario.Name = "pbx_Usuario";
            this.pbx_Usuario.Size = new System.Drawing.Size(134, 119);
            this.pbx_Usuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_Usuario.TabIndex = 18;
            this.pbx_Usuario.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::capaPresentacion.Properties.Resources.Focused_bible_landing_Cerrar;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(746, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 14;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // pbx_Sound
            // 
            this.pbx_Sound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbx_Sound.BackgroundImage = global::capaPresentacion.Properties.Resources.Sound_MouseLeave_ON;
            this.pbx_Sound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbx_Sound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbx_Sound.Location = new System.Drawing.Point(718, 10);
            this.pbx_Sound.Name = "pbx_Sound";
            this.pbx_Sound.Size = new System.Drawing.Size(24, 24);
            this.pbx_Sound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_Sound.TabIndex = 15;
            this.pbx_Sound.TabStop = false;
            this.pbx_Sound.Click += new System.EventHandler(this.pbx_Sound_Click);
            this.pbx_Sound.MouseEnter += new System.EventHandler(this.pbx_Sound_MouseEnter);
            this.pbx_Sound.MouseLeave += new System.EventHandler(this.pbx_Sound_MouseLeave);
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackgroundImage = global::capaPresentacion.Properties.Resources.Boton_Empezar_MouseLeave;
            this.btnEntrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEntrar.FlatAppearance.BorderSize = 0;
            this.btnEntrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEntrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntrar.Font = new System.Drawing.Font("Catamaran", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.White;
            this.btnEntrar.Location = new System.Drawing.Point(312, 305);
            this.btnEntrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(408, 47);
            this.btnEntrar.TabIndex = 3;
            this.btnEntrar.Text = "ENTRAR";
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            this.btnEntrar.MouseEnter += new System.EventHandler(this.btnEntrar_MouseEnter);
            this.btnEntrar.MouseLeave += new System.EventHandler(this.btnEntrar_MouseLeave);
            // 
            // cbx_autoLogin
            // 
            this.cbx_autoLogin.AutoSize = true;
            this.cbx_autoLogin.Font = new System.Drawing.Font("Catamaran", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_autoLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.cbx_autoLogin.Location = new System.Drawing.Point(615, 253);
            this.cbx_autoLogin.Name = "cbx_autoLogin";
            this.cbx_autoLogin.Size = new System.Drawing.Size(115, 28);
            this.cbx_autoLogin.TabIndex = 2;
            this.cbx_autoLogin.Text = "AUTO LOGIN";
            this.cbx_autoLogin.UseVisualStyleBackColor = true;
            // 
            // P_Login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(780, 396);
            this.Controls.Add(this.cbx_autoLogin);
            this.Controls.Add(this.pnl_fondoLogo);
            this.Controls.Add(this.lbl_Login);
            this.Controls.Add(this.pbx_Usuario);
            this.Controls.Add(this.llab_nuevoUsuario);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbx_Sound);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.text_Password);
            this.Controls.Add(this.text_Usuario);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "P_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Activated += new System.EventHandler(this.P_Login_Activated);
            this.Load += new System.EventHandler(this.P_Login_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P_Login_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.P_Login_MouseDown);
            this.pnl_fondoLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Usuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Sound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerUser;
        private System.Windows.Forms.Timer timerFondoLogo;
        private System.Windows.Forms.Panel pnl_fondoLogo;
        private System.Windows.Forms.Label lbl_CountDown;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox pbx_Sound;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.TextBox text_Password;
        private System.Windows.Forms.TextBox text_Usuario;
        private System.Windows.Forms.LinkLabel llab_nuevoUsuario;
        private System.Windows.Forms.Label lbl_Login;
        private System.Windows.Forms.PictureBox pbx_Usuario;
        private System.Windows.Forms.Timer timerPassword;
        private System.Windows.Forms.CheckBox cbx_autoLogin;
    }
}