namespace capaPresentacion
{
    partial class P_Splash
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
            this.lab_splash = new System.Windows.Forms.Label();
            this.TerminarSplash = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lab_splash
            // 
            this.lab_splash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_splash.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_splash.Location = new System.Drawing.Point(0, 0);
            this.lab_splash.Name = "lab_splash";
            this.lab_splash.Size = new System.Drawing.Size(1292, 735);
            this.lab_splash.TabIndex = 0;
            this.lab_splash.Text = "Splash";
            this.lab_splash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TerminarSplash
            // 
            this.TerminarSplash.Interval = 2000;
            this.TerminarSplash.Tick += new System.EventHandler(this.TerminarSplash_Tick);
            // 
            // P_Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1292, 735);
            this.Controls.Add(this.lab_splash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.P_Splash_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lab_splash;
        private System.Windows.Forms.Timer TerminarSplash;
    }
}