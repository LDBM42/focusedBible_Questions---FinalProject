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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmr_cuadroAzul = new System.Windows.Forms.Timer(this.components);
            this.pnl_Azul = new System.Windows.Forms.Panel();
            this.lbl_CountDown = new System.Windows.Forms.Label();
            this.pbx_logo = new System.Windows.Forms.PictureBox();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.text_Password = new System.Windows.Forms.TextBox();
            this.text_Usuario = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnMinimize = new System.Windows.Forms.PictureBox();
            this.lbl_Login = new System.Windows.Forms.Label();
            this.pnl_Azul.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tmr_cuadroAzul
            // 
            this.tmr_cuadroAzul.Interval = 30;
            this.tmr_cuadroAzul.Tick += new System.EventHandler(this.tmr_cuadroAzul_Tick);
            // 
            // pnl_Azul
            // 
            this.pnl_Azul.BackColor = System.Drawing.Color.White;
            this.pnl_Azul.Controls.Add(this.lbl_CountDown);
            this.pnl_Azul.Controls.Add(this.pbx_logo);
            this.pnl_Azul.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Azul.Location = new System.Drawing.Point(0, 0);
            this.pnl_Azul.Name = "pnl_Azul";
            this.pnl_Azul.Size = new System.Drawing.Size(287, 396);
            this.pnl_Azul.TabIndex = 13;
            this.pnl_Azul.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_Azul_MouseDown);
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
            // pbx_logo
            // 
            this.pbx_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbx_logo.Location = new System.Drawing.Point(57, 97);
            this.pbx_logo.Margin = new System.Windows.Forms.Padding(4);
            this.pbx_logo.Name = "pbx_logo";
            this.pbx_logo.Size = new System.Drawing.Size(168, 145);
            this.pbx_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_logo.TabIndex = 0;
            this.pbx_logo.TabStop = false;
            this.pbx_logo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbx_logo_MouseDown);
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackColor = System.Drawing.Color.DimGray;
            this.btnEntrar.FlatAppearance.BorderSize = 0;
            this.btnEntrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnEntrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntrar.ForeColor = System.Drawing.Color.LightGray;
            this.btnEntrar.Location = new System.Drawing.Point(312, 319);
            this.btnEntrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(408, 40);
            this.btnEntrar.TabIndex = 10;
            this.btnEntrar.Text = "ENTRAR";
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // text_Password
            // 
            this.text_Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.text_Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.text_Password.ForeColor = System.Drawing.Color.DimGray;
            this.text_Password.Location = new System.Drawing.Point(318, 251);
            this.text_Password.Margin = new System.Windows.Forms.Padding(4);
            this.text_Password.Name = "text_Password";
            this.text_Password.Size = new System.Drawing.Size(408, 23);
            this.text_Password.TabIndex = 12;
            this.text_Password.Text = "PASSWORD";
            this.text_Password.Click += new System.EventHandler(this.text_Password_Click);
            this.text_Password.TextChanged += new System.EventHandler(this.text_Password_TextChanged);
            this.text_Password.Enter += new System.EventHandler(this.text_Password_Enter);
            this.text_Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Password_KeyPress);
            this.text_Password.MouseEnter += new System.EventHandler(this.text_Password_MouseEnter);
            this.text_Password.MouseLeave += new System.EventHandler(this.text_Password_MouseLeave);
            // 
            // text_Usuario
            // 
            this.text_Usuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.text_Usuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_Usuario.ForeColor = System.Drawing.Color.DimGray;
            this.text_Usuario.Location = new System.Drawing.Point(318, 185);
            this.text_Usuario.Margin = new System.Windows.Forms.Padding(4);
            this.text_Usuario.Name = "text_Usuario";
            this.text_Usuario.Size = new System.Drawing.Size(408, 23);
            this.text_Usuario.TabIndex = 11;
            this.text_Usuario.Text = "USUARIO";
            this.text_Usuario.Click += new System.EventHandler(this.text_Usuario_Click);
            this.text_Usuario.Enter += new System.EventHandler(this.text_Usuario_Enter);
            this.text_Usuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_Usuario_KeyPress);
            this.text_Usuario.MouseEnter += new System.EventHandler(this.text_Usuario_MouseEnter);
            this.text_Usuario.MouseLeave += new System.EventHandler(this.text_Usuario_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::capaPresentacion.Properties.Resources.UsuarioBig;
            this.pictureBox1.Location = new System.Drawing.Point(449, 32);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::capaPresentacion.Properties.Resources.close_gray;
            this.btnClose.Location = new System.Drawing.Point(750, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 14;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.Image = global::capaPresentacion.Properties.Resources.minimize_gray;
            this.btnMinimize.Location = new System.Drawing.Point(722, 6);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(24, 24);
            this.btnMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimize.TabIndex = 15;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // lbl_Login
            // 
            this.lbl_Login.AutoSize = true;
            this.lbl_Login.BackColor = System.Drawing.Color.Gray;
            this.lbl_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_Login.Location = new System.Drawing.Point(465, 119);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(119, 38);
            this.lbl_Login.TabIndex = 9;
            this.lbl_Login.Text = "LOGIN";
            // 
            // P_Login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(780, 396);
            this.Controls.Add(this.lbl_Login);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnl_Azul);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.text_Password);
            this.Controls.Add(this.text_Usuario);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "P_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.P_Login_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P_Login_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.P_Login_MouseDown);
            this.pnl_Azul.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer tmr_cuadroAzul;
        private System.Windows.Forms.Panel pnl_Azul;
        private System.Windows.Forms.Label lbl_CountDown;
        private System.Windows.Forms.PictureBox pbx_logo;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox btnMinimize;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.TextBox text_Password;
        private System.Windows.Forms.TextBox text_Usuario;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_Login;
    }
}