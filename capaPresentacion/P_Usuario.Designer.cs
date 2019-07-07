namespace capaPresentacion
{
    partial class P_Usuario
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
            this.pnl_fondoLogo = new System.Windows.Forms.Panel();
            this.lbl_CountDown = new System.Windows.Forms.Label();
            this.text_Password = new System.Windows.Forms.TextBox();
            this.text_Usuario = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmr_cuadroBlanco = new System.Windows.Forms.Timer(this.components);
            this.rb_Masculino = new System.Windows.Forms.RadioButton();
            this.lbl_Genero = new System.Windows.Forms.TextBox();
            this.rb_Femenino = new System.Windows.Forms.RadioButton();
            this.lbl_nuevoUsuario = new System.Windows.Forms.Label();
            this.text_Rol = new System.Windows.Forms.TextBox();
            this.cbx_Rol = new System.Windows.Forms.ComboBox();
            this.pbx_Usuario = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.pbx_Sound = new System.Windows.Forms.PictureBox();
            this.btn_Crear = new System.Windows.Forms.Button();
            this.pnl_fondoLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Usuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Sound)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_fondoLogo
            // 
            this.pnl_fondoLogo.BackColor = System.Drawing.Color.White;
            this.pnl_fondoLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_fondoLogo.Controls.Add(this.lbl_CountDown);
            this.pnl_fondoLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_fondoLogo.Location = new System.Drawing.Point(0, 0);
            this.pnl_fondoLogo.Name = "pnl_fondoLogo";
            this.pnl_fondoLogo.Size = new System.Drawing.Size(249, 450);
            this.pnl_fondoLogo.TabIndex = 21;
            this.pnl_fondoLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_White_MouseDown);
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
            this.text_Password.Location = new System.Drawing.Point(320, 247);
            this.text_Password.Margin = new System.Windows.Forms.Padding(4);
            this.text_Password.Name = "text_Password";
            this.text_Password.Size = new System.Drawing.Size(408, 37);
            this.text_Password.TabIndex = 20;
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
            this.text_Usuario.Location = new System.Drawing.Point(320, 185);
            this.text_Usuario.Margin = new System.Windows.Forms.Padding(4);
            this.text_Usuario.Name = "text_Usuario";
            this.text_Usuario.Size = new System.Drawing.Size(408, 37);
            this.text_Usuario.TabIndex = 19;
            this.text_Usuario.Text = "USUARIO";
            this.text_Usuario.Click += new System.EventHandler(this.text_Usuario_Click);
            this.text_Usuario.Enter += new System.EventHandler(this.text_Usuario_Enter);
            this.text_Usuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Usuario_KeyPress);
            this.text_Usuario.MouseEnter += new System.EventHandler(this.text_Usuario_MouseEnter);
            this.text_Usuario.MouseLeave += new System.EventHandler(this.text_Usuario_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tmr_cuadroBlanco
            // 
            this.tmr_cuadroBlanco.Interval = 30;
            this.tmr_cuadroBlanco.Tick += new System.EventHandler(this.tmr_cuadroBlanco_Tick);
            // 
            // rb_Masculino
            // 
            this.rb_Masculino.AutoSize = true;
            this.rb_Masculino.Checked = true;
            this.rb_Masculino.Font = new System.Drawing.Font("Catamaran", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Masculino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.rb_Masculino.Location = new System.Drawing.Point(616, 312);
            this.rb_Masculino.Name = "rb_Masculino";
            this.rb_Masculino.Size = new System.Drawing.Size(51, 37);
            this.rb_Masculino.TabIndex = 24;
            this.rb_Masculino.TabStop = true;
            this.rb_Masculino.Text = "M";
            this.rb_Masculino.UseVisualStyleBackColor = true;
            this.rb_Masculino.CheckedChanged += new System.EventHandler(this.rb_Masculino_CheckedChanged);
            // 
            // lbl_Genero
            // 
            this.lbl_Genero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.lbl_Genero.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_Genero.Font = new System.Drawing.Font("Catamaran", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Genero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.lbl_Genero.Location = new System.Drawing.Point(526, 315);
            this.lbl_Genero.Margin = new System.Windows.Forms.Padding(4);
            this.lbl_Genero.Name = "lbl_Genero";
            this.lbl_Genero.ReadOnly = true;
            this.lbl_Genero.Size = new System.Drawing.Size(78, 33);
            this.lbl_Genero.TabIndex = 26;
            this.lbl_Genero.Text = "Genero:";
            // 
            // rb_Femenino
            // 
            this.rb_Femenino.AutoSize = true;
            this.rb_Femenino.Font = new System.Drawing.Font("Catamaran", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Femenino.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.rb_Femenino.Location = new System.Drawing.Point(669, 312);
            this.rb_Femenino.Name = "rb_Femenino";
            this.rb_Femenino.Size = new System.Drawing.Size(46, 37);
            this.rb_Femenino.TabIndex = 27;
            this.rb_Femenino.Text = "F";
            this.rb_Femenino.UseVisualStyleBackColor = true;
            this.rb_Femenino.CheckedChanged += new System.EventHandler(this.rb_Femenino_CheckedChanged);
            // 
            // lbl_nuevoUsuario
            // 
            this.lbl_nuevoUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lbl_nuevoUsuario.Font = new System.Drawing.Font("Catamaran ExtraBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nuevoUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(143)))), ((int)(((byte)(163)))));
            this.lbl_nuevoUsuario.Location = new System.Drawing.Point(390, 116);
            this.lbl_nuevoUsuario.Name = "lbl_nuevoUsuario";
            this.lbl_nuevoUsuario.Size = new System.Drawing.Size(267, 41);
            this.lbl_nuevoUsuario.TabIndex = 17;
            this.lbl_nuevoUsuario.Text = "NUEVO USUARIO";
            this.lbl_nuevoUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // text_Rol
            // 
            this.text_Rol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.text_Rol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_Rol.Font = new System.Drawing.Font("Catamaran", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_Rol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.text_Rol.Location = new System.Drawing.Point(320, 315);
            this.text_Rol.Margin = new System.Windows.Forms.Padding(4);
            this.text_Rol.Name = "text_Rol";
            this.text_Rol.ReadOnly = true;
            this.text_Rol.Size = new System.Drawing.Size(68, 33);
            this.text_Rol.TabIndex = 28;
            this.text_Rol.Text = "Rol:";
            // 
            // cbx_Rol
            // 
            this.cbx_Rol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.cbx_Rol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbx_Rol.Font = new System.Drawing.Font("Catamaran", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_Rol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.cbx_Rol.FormattingEnabled = true;
            this.cbx_Rol.Items.AddRange(new object[] {
            "Admin",
            "Estudiante"});
            this.cbx_Rol.Location = new System.Drawing.Point(366, 310);
            this.cbx_Rol.Name = "cbx_Rol";
            this.cbx_Rol.Size = new System.Drawing.Size(111, 41);
            this.cbx_Rol.TabIndex = 29;
            this.cbx_Rol.Text = "Estudiante";
            // 
            // pbx_Usuario
            // 
            this.pbx_Usuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbx_Usuario.Image = global::capaPresentacion.Properties.Resources.Usuario;
            this.pbx_Usuario.Location = new System.Drawing.Point(455, 22);
            this.pbx_Usuario.Margin = new System.Windows.Forms.Padding(4);
            this.pbx_Usuario.Name = "pbx_Usuario";
            this.pbx_Usuario.Size = new System.Drawing.Size(134, 119);
            this.pbx_Usuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_Usuario.TabIndex = 16;
            this.pbx_Usuario.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::capaPresentacion.Properties.Resources.Focused_bible_landing_Cerrar;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(750, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 22;
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
            this.pbx_Sound.Location = new System.Drawing.Point(721, 10);
            this.pbx_Sound.Name = "pbx_Sound";
            this.pbx_Sound.Size = new System.Drawing.Size(24, 24);
            this.pbx_Sound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_Sound.TabIndex = 23;
            this.pbx_Sound.TabStop = false;
            this.pbx_Sound.Click += new System.EventHandler(this.pbx_Sound_Click);
            this.pbx_Sound.MouseEnter += new System.EventHandler(this.btnMinimize_MouseEnter);
            this.pbx_Sound.MouseLeave += new System.EventHandler(this.btnMinimize_MouseLeave);
            // 
            // btn_Crear
            // 
            this.btn_Crear.BackgroundImage = global::capaPresentacion.Properties.Resources.Boton_Empezar_MouseLeave;
            this.btn_Crear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Crear.FlatAppearance.BorderSize = 0;
            this.btn_Crear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Crear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Crear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Crear.Font = new System.Drawing.Font("Catamaran", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Crear.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Crear.Location = new System.Drawing.Point(311, 373);
            this.btn_Crear.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Crear.Name = "btn_Crear";
            this.btn_Crear.Size = new System.Drawing.Size(408, 47);
            this.btn_Crear.TabIndex = 18;
            this.btn_Crear.Text = "CREAR";
            this.btn_Crear.UseVisualStyleBackColor = false;
            this.btn_Crear.Click += new System.EventHandler(this.btn_Crear_Click);
            this.btn_Crear.MouseEnter += new System.EventHandler(this.btn_Crear_MouseEnter);
            this.btn_Crear.MouseLeave += new System.EventHandler(this.btn_Crear_MouseLeave);
            // 
            // P_Usuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(780, 450);
            this.Controls.Add(this.pnl_fondoLogo);
            this.Controls.Add(this.cbx_Rol);
            this.Controls.Add(this.text_Rol);
            this.Controls.Add(this.rb_Femenino);
            this.Controls.Add(this.lbl_Genero);
            this.Controls.Add(this.rb_Masculino);
            this.Controls.Add(this.lbl_nuevoUsuario);
            this.Controls.Add(this.pbx_Usuario);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbx_Sound);
            this.Controls.Add(this.btn_Crear);
            this.Controls.Add(this.text_Password);
            this.Controls.Add(this.text_Usuario);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "P_Usuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P_Usuario";
            this.Load += new System.EventHandler(this.P_Usuario_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P_Usuario_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.P_Usuario_MouseDown);
            this.pnl_fondoLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Usuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Sound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbx_Usuario;
        private System.Windows.Forms.Panel pnl_fondoLogo;
        private System.Windows.Forms.Label lbl_CountDown;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox pbx_Sound;
        private System.Windows.Forms.Button btn_Crear;
        private System.Windows.Forms.TextBox text_Password;
        private System.Windows.Forms.TextBox text_Usuario;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer tmr_cuadroBlanco;
        private System.Windows.Forms.RadioButton rb_Masculino;
        private System.Windows.Forms.TextBox lbl_Genero;
        private System.Windows.Forms.RadioButton rb_Femenino;
        private System.Windows.Forms.Label lbl_nuevoUsuario;
        private System.Windows.Forms.TextBox text_Rol;
        private System.Windows.Forms.ComboBox cbx_Rol;
    }
}