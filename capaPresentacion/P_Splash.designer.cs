﻿namespace capaPresentacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_Splash));
            this.TerminarSplash = new System.Windows.Forms.Timer(this.components);
            this.pbx_splash = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_splash)).BeginInit();
            this.SuspendLayout();
            // 
            // TerminarSplash
            // 
            this.TerminarSplash.Tick += new System.EventHandler(this.TerminarSplash_Tick);
            // 
            // pbx_splash
            // 
            this.pbx_splash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbx_splash.Image = global::capaPresentacion.Properties.Resources.Splash01;
            this.pbx_splash.Location = new System.Drawing.Point(0, 0);
            this.pbx_splash.Name = "pbx_splash";
            this.pbx_splash.Size = new System.Drawing.Size(1292, 735);
            this.pbx_splash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbx_splash.TabIndex = 0;
            this.pbx_splash.TabStop = false;
            // 
            // P_Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(18)))), ((int)(((byte)(21)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1292, 735);
            this.Controls.Add(this.pbx_splash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.P_Splash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_splash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TerminarSplash;
        private System.Windows.Forms.PictureBox pbx_splash;
    }
}