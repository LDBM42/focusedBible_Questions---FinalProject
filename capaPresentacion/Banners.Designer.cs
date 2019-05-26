namespace capaPresentacion
{
    partial class Banners
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
            this.lab_banner = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lab_banner
            // 
            this.lab_banner.BackColor = System.Drawing.Color.Transparent;
            this.lab_banner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_banner.Font = new System.Drawing.Font("Permanent Marker", 110F);
            this.lab_banner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lab_banner.Location = new System.Drawing.Point(0, 0);
            this.lab_banner.Name = "lab_banner";
            this.lab_banner.Size = new System.Drawing.Size(1274, 688);
            this.lab_banner.TabIndex = 0;
            this.lab_banner.Text = "Player Two WIns!";
            this.lab_banner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Banners
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.ClientSize = new System.Drawing.Size(1274, 688);
            this.ControlBox = false;
            this.Controls.Add(this.lab_banner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Banners";
            this.Opacity = 0.9D;
            this.Text = "Banners";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Banners_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lab_banner;
    }
}