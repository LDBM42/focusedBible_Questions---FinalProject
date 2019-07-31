namespace capaPresentacion
{
    partial class HowToPlay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToPlay));
            this.btn_Cerrar = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axAcroPDF = new AxAcroPDFLib.AxAcroPDF();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Cerrar
            // 
            this.btn_Cerrar.BackgroundImage = global::capaPresentacion.Properties.Resources.Boton_Empezar_MouseLeave;
            this.btn_Cerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Cerrar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cerrar.FlatAppearance.BorderSize = 0;
            this.btn_Cerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Cerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cerrar.Font = new System.Drawing.Font("Catamaran Black", 15F, System.Drawing.FontStyle.Bold);
            this.btn_Cerrar.ForeColor = System.Drawing.Color.White;
            this.btn_Cerrar.Location = new System.Drawing.Point(0, 323);
            this.btn_Cerrar.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Cerrar.Name = "btn_Cerrar";
            this.btn_Cerrar.Size = new System.Drawing.Size(706, 25);
            this.btn_Cerrar.TabIndex = 13;
            this.btn_Cerrar.Text = "CERRAR";
            this.btn_Cerrar.UseVisualStyleBackColor = false;
            this.btn_Cerrar.Click += new System.EventHandler(this.btn_Cerrar_Click);
            this.btn_Cerrar.MouseEnter += new System.EventHandler(this.btn_Cerrar_MouseEnter);
            this.btn_Cerrar.MouseLeave += new System.EventHandler(this.btn_Cerrar_MouseLeave);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(706, 360);
            this.pnlMain.TabIndex = 15;
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_Cerrar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.axAcroPDF, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(706, 360);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // axAcroPDF
            // 
            this.axAcroPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAcroPDF.Enabled = true;
            this.axAcroPDF.Location = new System.Drawing.Point(3, 3);
            this.axAcroPDF.Name = "axAcroPDF";
            this.axAcroPDF.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF.OcxState")));
            this.axAcroPDF.Size = new System.Drawing.Size(700, 307);
            this.axAcroPDF.TabIndex = 18;
            // 
            // HowToPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(706, 360);
            this.Controls.Add(this.pnlMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "HowToPlay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HowToPlay";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HowToPlay_Load);
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cerrar;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF;
    }
}