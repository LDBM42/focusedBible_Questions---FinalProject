namespace capaPresentacion
{
    partial class P_PARTIDA_PROFE_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lab_duo = new System.Windows.Forms.Label();
            this.tbx_codigoPartida = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_IniciarDebate = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.dgvAlumnos = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel20 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_Settings = new System.Windows.Forms.PictureBox();
            this.btn_goToMain = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.pB_logo = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.timer_ActualizarEstadoLista = new System.Windows.Forms.Timer(this.components);
            this.Alumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Connected = new System.Windows.Forms.DataGridViewImageColumn();
            this.Terminado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Finish = new System.Windows.Forms.DataGridViewImageColumn();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlumnos)).BeginInit();
            this.tableLayoutPanel18.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Settings)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_logo)).BeginInit();
            this.tableLayoutPanel21.SuspendLayout();
            this.SuspendLayout();
            // 
            // lab_duo
            // 
            this.lab_duo.AutoSize = true;
            this.lab_duo.BackColor = System.Drawing.Color.Transparent;
            this.lab_duo.Font = new System.Drawing.Font("Avenir Next", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_duo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(143)))), ((int)(((byte)(163)))));
            this.lab_duo.Location = new System.Drawing.Point(4, 38);
            this.lab_duo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_duo.Name = "lab_duo";
            this.lab_duo.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.lab_duo.Size = new System.Drawing.Size(369, 89);
            this.lab_duo.TabIndex = 0;
            this.lab_duo.Text = "PARTIDA (Profe)";
            this.lab_duo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_codigoPartida
            // 
            this.tbx_codigoPartida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbx_codigoPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_codigoPartida.Location = new System.Drawing.Point(619, 150);
            this.tbx_codigoPartida.Name = "tbx_codigoPartida";
            this.tbx_codigoPartida.Size = new System.Drawing.Size(254, 45);
            this.tbx_codigoPartida.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Avenir Next", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(91)))));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "JUGAROR / GRUPO 1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.tableLayoutPanel15, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.dgvAlumnos, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(4, 202);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.44579F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.55422F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(608, 423);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Controls.Add(this.btn_IniciarDebate, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.btn_cancelar, 1, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(4, 348);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(600, 71);
            this.tableLayoutPanel15.TabIndex = 1;
            // 
            // btn_IniciarDebate
            // 
            this.btn_IniciarDebate.BackgroundImage = global::capaPresentacion.Properties.Resources.Boton_Empezar_MouseLeave;
            this.btn_IniciarDebate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_IniciarDebate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_IniciarDebate.FlatAppearance.BorderSize = 0;
            this.btn_IniciarDebate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_IniciarDebate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_IniciarDebate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_IniciarDebate.Font = new System.Drawing.Font("Avenir Next", 22.2F, System.Drawing.FontStyle.Bold);
            this.btn_IniciarDebate.ForeColor = System.Drawing.Color.White;
            this.btn_IniciarDebate.Location = new System.Drawing.Point(3, 3);
            this.btn_IniciarDebate.Name = "btn_IniciarDebate";
            this.btn_IniciarDebate.Size = new System.Drawing.Size(294, 65);
            this.btn_IniciarDebate.TabIndex = 1;
            this.btn_IniciarDebate.Text = "EMPEZAR";
            this.btn_IniciarDebate.UseVisualStyleBackColor = false;
            this.btn_IniciarDebate.Click += new System.EventHandler(this.btn_IniciarDebate_Click);
            this.btn_IniciarDebate.MouseEnter += new System.EventHandler(this.btn_IniciarPartida_MouseEnter);
            this.btn_IniciarDebate.MouseLeave += new System.EventHandler(this.btn_IniciarPartida_MouseLeave);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.BackgroundImage = global::capaPresentacion.Properties.Resources.Boton_Empezar_MouseLeave;
            this.btn_cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancelar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancelar.FlatAppearance.BorderSize = 0;
            this.btn_cancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Avenir Next", 22.2F, System.Drawing.FontStyle.Bold);
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(303, 3);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(294, 65);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "CANCELAR";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // dgvAlumnos
            // 
            this.dgvAlumnos.AllowUserToAddRows = false;
            this.dgvAlumnos.AllowUserToDeleteRows = false;
            this.dgvAlumnos.AllowUserToResizeColumns = false;
            this.dgvAlumnos.AllowUserToResizeRows = false;
            this.dgvAlumnos.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlumnos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlumnos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAlumnos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvAlumnos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Avenir Next Demi Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlumnos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlumnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlumnos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Alumno,
            this.Id,
            this.Estado,
            this.Connected,
            this.Terminado,
            this.Finish});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Avenir Next Demi Bold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlumnos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAlumnos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlumnos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAlumnos.Enabled = false;
            this.dgvAlumnos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(91)))));
            this.dgvAlumnos.Location = new System.Drawing.Point(3, 3);
            this.dgvAlumnos.MultiSelect = false;
            this.dgvAlumnos.Name = "dgvAlumnos";
            this.dgvAlumnos.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Avenir Next Demi Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(91)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlumnos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAlumnos.RowHeadersVisible = false;
            this.dgvAlumnos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            this.dgvAlumnos.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAlumnos.RowTemplate.Height = 24;
            this.dgvAlumnos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAlumnos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlumnos.ShowCellErrors = false;
            this.dgvAlumnos.ShowCellToolTips = false;
            this.dgvAlumnos.ShowEditingIcon = false;
            this.dgvAlumnos.ShowRowErrors = false;
            this.dgvAlumnos.Size = new System.Drawing.Size(602, 338);
            this.dgvAlumnos.TabIndex = 14;
            this.dgvAlumnos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAlumnos_CellFormatting);
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.Controls.Add(this.tableLayoutPanel11, 0, 2);
            this.tableLayoutPanel18.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.tbx_codigoPartida, 1, 1);
            this.tableLayoutPanel18.Controls.Add(this.tableLayoutPanel20, 1, 0);
            this.tableLayoutPanel18.Controls.Add(this.lab_duo, 0, 1);
            this.tableLayoutPanel18.Controls.Add(this.tableLayoutPanel7, 1, 2);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(29, 33);
            this.tableLayoutPanel18.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 4;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.812589F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.96732F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.56197F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.658126F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(1232, 668);
            this.tableLayoutPanel18.TabIndex = 0;
            // 
            // tableLayoutPanel20
            // 
            this.tableLayoutPanel20.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel20.ColumnCount = 5;
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel20.Controls.Add(this.Btn_Settings, 2, 0);
            this.tableLayoutPanel20.Controls.Add(this.btn_goToMain, 4, 0);
            this.tableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel20.Location = new System.Drawing.Point(620, 4);
            this.tableLayoutPanel20.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            this.tableLayoutPanel20.RowCount = 1;
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel20.Size = new System.Drawing.Size(608, 30);
            this.tableLayoutPanel20.TabIndex = 3;
            // 
            // Btn_Settings
            // 
            this.Btn_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Settings.Image = global::capaPresentacion.Properties.Resources.Focused_bible_landing_02;
            this.Btn_Settings.Location = new System.Drawing.Point(531, 3);
            this.Btn_Settings.Name = "Btn_Settings";
            this.Btn_Settings.Size = new System.Drawing.Size(30, 24);
            this.Btn_Settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Btn_Settings.TabIndex = 1;
            this.Btn_Settings.TabStop = false;
            this.Btn_Settings.Click += new System.EventHandler(this.Btn_Settings_Click);
            this.Btn_Settings.MouseEnter += new System.EventHandler(this.Btn_Settings_MouseEnter);
            this.Btn_Settings.MouseLeave += new System.EventHandler(this.Btn_Settings_MouseLeave);
            // 
            // btn_goToMain
            // 
            this.btn_goToMain.BackColor = System.Drawing.Color.Transparent;
            this.btn_goToMain.BackgroundImage = global::capaPresentacion.Properties.Resources.Focused_bible_SOLO_07;
            this.btn_goToMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_goToMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_goToMain.FlatAppearance.BorderSize = 0;
            this.btn_goToMain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_goToMain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_goToMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_goToMain.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Bold);
            this.btn_goToMain.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_goToMain.Location = new System.Drawing.Point(573, 3);
            this.btn_goToMain.Name = "btn_goToMain";
            this.btn_goToMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_goToMain.Size = new System.Drawing.Size(32, 24);
            this.btn_goToMain.TabIndex = 15;
            this.btn_goToMain.UseVisualStyleBackColor = false;
            this.btn_goToMain.Click += new System.EventHandler(this.btn_goToMain_Click);
            this.btn_goToMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btn_goToMain_KeyPress);
            this.btn_goToMain.MouseEnter += new System.EventHandler(this.btn_goToMain_MouseEnter);
            this.btn_goToMain.MouseLeave += new System.EventHandler(this.btn_goToMain_MouseLeave);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.70395F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.05921F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.116189F));
            this.tableLayoutPanel7.Controls.Add(this.pB_logo, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(620, 202);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.86784F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.13216F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(608, 423);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // pB_logo
            // 
            this.pB_logo.BackColor = System.Drawing.Color.Transparent;
            this.pB_logo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pB_logo.Image = global::capaPresentacion.Properties.Resources.Focused_bible_landing_04;
            this.pB_logo.Location = new System.Drawing.Point(367, 255);
            this.pB_logo.Margin = new System.Windows.Forms.Padding(4);
            this.pB_logo.Name = "pB_logo";
            this.pB_logo.Size = new System.Drawing.Size(193, 121);
            this.pB_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pB_logo.TabIndex = 0;
            this.pB_logo.TabStop = false;
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel21.ColumnCount = 3;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel21.Controls.Add(this.tableLayoutPanel18, 1, 1);
            this.tableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel21.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel21.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 3;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(1292, 735);
            this.tableLayoutPanel21.TabIndex = 14;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Finish";
            this.dataGridViewImageColumn1.Image = global::capaPresentacion.Properties.Resources.status_offline;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // timer_ActualizarEstadoLista
            // 
            this.timer_ActualizarEstadoLista.Interval = 2000;
            this.timer_ActualizarEstadoLista.Tick += new System.EventHandler(this.timer_ActualizarEstadoLista_Tick);
            // 
            // Alumno
            // 
            this.Alumno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Alumno.DataPropertyName = "Alumno";
            this.Alumno.FillWeight = 70F;
            this.Alumno.HeaderText = "Alumnos";
            this.Alumno.Name = "Alumno";
            this.Alumno.ReadOnly = true;
            this.Alumno.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Alumno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Estado
            // 
            this.Estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Estado.DataPropertyName = "Estado";
            this.Estado.FillWeight = 15F;
            this.Estado.HeaderText = "Conectado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Estado.Visible = false;
            // 
            // Connected
            // 
            this.Connected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Connected.FillWeight = 15F;
            this.Connected.HeaderText = "Conectado";
            this.Connected.Image = global::capaPresentacion.Properties.Resources.status_offline;
            this.Connected.Name = "Connected";
            this.Connected.ReadOnly = true;
            this.Connected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Terminado
            // 
            this.Terminado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Terminado.DataPropertyName = "Terminado";
            this.Terminado.FillWeight = 15F;
            this.Terminado.HeaderText = "Terminado";
            this.Terminado.Name = "Terminado";
            this.Terminado.ReadOnly = true;
            this.Terminado.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Terminado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Terminado.Visible = false;
            // 
            // Finish
            // 
            this.Finish.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Finish.FillWeight = 15F;
            this.Finish.HeaderText = "Terminado";
            this.Finish.Image = global::capaPresentacion.Properties.Resources.status_offline;
            this.Finish.Name = "Finish";
            this.Finish.ReadOnly = true;
            this.Finish.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // P_PARTIDA_PROFE_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1292, 735);
            this.Controls.Add(this.tableLayoutPanel21);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_PARTIDA_PROFE_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Questions focusedBible";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.P_PARTIDA_PROFE_Main_Activated);
            this.Load += new System.EventHandler(this.P_Debate_Main_Load);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlumnos)).EndInit();
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.tableLayoutPanel20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Settings)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pB_logo)).EndInit();
            this.tableLayoutPanel21.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox Btn_Settings;
        private System.Windows.Forms.Button btn_goToMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel20;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.Label lab_duo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_codigoPartida;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.Button btn_IniciarDebate;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Timer timer_ActualizarEstadoLista;
        private System.Windows.Forms.DataGridView dgvAlumnos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.PictureBox pB_logo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewImageColumn Connected;
        private System.Windows.Forms.DataGridViewTextBoxColumn Terminado;
        private System.Windows.Forms.DataGridViewImageColumn Finish;
    }
}