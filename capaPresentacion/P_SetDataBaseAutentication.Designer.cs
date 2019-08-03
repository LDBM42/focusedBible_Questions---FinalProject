namespace capaPresentacion
{
    partial class P_SetDataBaseAutentication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SetDataBaseAutentication));
            this.pnl_fondoLogo = new System.Windows.Forms.Panel();
            this.lbl_CountDown = new System.Windows.Forms.Label();
            this.text_Password = new System.Windows.Forms.TextBox();
            this.text_Usuario = new System.Windows.Forms.TextBox();
            this.tmr_cuadroBlanco = new System.Windows.Forms.Timer(this.components);
            this.lbl_nuevoUsuario = new System.Windows.Forms.Label();
            this.text_hostName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.pbx_Sound = new System.Windows.Forms.PictureBox();
            this.btn_Crear = new System.Windows.Forms.Button();
            this.pnl_fondoLogo.SuspendLayout();
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
            this.text_Password.TabIndex = 2;
            this.text_Password.Text = "CONTRASEÑA";
            this.text_Password.Click += new System.EventHandler(this.text_Password_Click);
            this.text_Password.TextChanged += new System.EventHandler(this.text_Password_TextChanged);
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
            this.text_Usuario.TabIndex = 1;
            this.text_Usuario.Text = "USUARIO";
            this.text_Usuario.Click += new System.EventHandler(this.text_Usuario_Click);
            this.text_Usuario.Enter += new System.EventHandler(this.text_Usuario_Enter);
            this.text_Usuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Usuario_KeyPress);
            this.text_Usuario.MouseEnter += new System.EventHandler(this.text_Usuario_MouseEnter);
            this.text_Usuario.MouseLeave += new System.EventHandler(this.text_Usuario_MouseLeave);
            // 
            // tmr_cuadroBlanco
            // 
            this.tmr_cuadroBlanco.Interval = 30;
            this.tmr_cuadroBlanco.Tick += new System.EventHandler(this.tmr_cuadroBlanco_Tick);
            // 
            // lbl_nuevoUsuario
            // 
            this.lbl_nuevoUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lbl_nuevoUsuario.Font = new System.Drawing.Font("Catamaran ExtraBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nuevoUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(143)))), ((int)(((byte)(163)))));
            this.lbl_nuevoUsuario.Location = new System.Drawing.Point(330, 33);
            this.lbl_nuevoUsuario.Name = "lbl_nuevoUsuario";
            this.lbl_nuevoUsuario.Size = new System.Drawing.Size(388, 41);
            this.lbl_nuevoUsuario.TabIndex = 17;
            this.lbl_nuevoUsuario.Text = "BASE DE DATOS EXTERNA";
            this.lbl_nuevoUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // text_hostName
            // 
            this.text_hostName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.text_hostName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_hostName.Font = new System.Drawing.Font("Catamaran", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_hostName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(52)))), ((int)(((byte)(61)))));
            this.text_hostName.Location = new System.Drawing.Point(320, 120);
            this.text_hostName.Margin = new System.Windows.Forms.Padding(4);
            this.text_hostName.Name = "text_hostName";
            this.text_hostName.Size = new System.Drawing.Size(408, 37);
            this.text_hostName.TabIndex = 0;
            this.text_hostName.Text = "NOMBRE DE EQUIPO";
            this.text_hostName.Click += new System.EventHandler(this.text_hostName_Click);
            this.text_hostName.Enter += new System.EventHandler(this.text_hostName_Enter);
            this.text_hostName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_hostName_KeyPress);
            this.text_hostName.MouseEnter += new System.EventHandler(this.text_hostName_MouseEnter);
            this.text_hostName.MouseLeave += new System.EventHandler(this.text_hostName_MouseLeave);
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
            this.btn_Crear.Location = new System.Drawing.Point(320, 363);
            this.btn_Crear.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Crear.Name = "btn_Crear";
            this.btn_Crear.Size = new System.Drawing.Size(408, 47);
            this.btn_Crear.TabIndex = 3;
            this.btn_Crear.Text = "GUARDAR";
            this.btn_Crear.UseVisualStyleBackColor = false;
            this.btn_Crear.Click += new System.EventHandler(this.btn_Guardar_Click);
            this.btn_Crear.MouseEnter += new System.EventHandler(this.btn_Guardar_MouseEnter);
            this.btn_Crear.MouseLeave += new System.EventHandler(this.btn_Guardar_MouseLeave);
            // 
            // P_SetDataBaseAutentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(780, 450);
            this.Controls.Add(this.pnl_fondoLogo);
            this.Controls.Add(this.text_hostName);
            this.Controls.Add(this.lbl_nuevoUsuario);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbx_Sound);
            this.Controls.Add(this.btn_Crear);
            this.Controls.Add(this.text_Password);
            this.Controls.Add(this.text_Usuario);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "P_SetDataBaseAutentication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P_Usuario";
            this.Load += new System.EventHandler(this.P_Usuario_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P_Usuario_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.P_Usuario_MouseDown);
            this.pnl_fondoLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Sound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnl_fondoLogo;
        private System.Windows.Forms.Label lbl_CountDown;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox pbx_Sound;
        private System.Windows.Forms.Button btn_Crear;
        private System.Windows.Forms.TextBox text_Password;
        private System.Windows.Forms.TextBox text_Usuario;
        private System.Windows.Forms.Timer tmr_cuadroBlanco;
        private System.Windows.Forms.Label lbl_nuevoUsuario;
        private System.Windows.Forms.TextBox text_hostName;
    }
}