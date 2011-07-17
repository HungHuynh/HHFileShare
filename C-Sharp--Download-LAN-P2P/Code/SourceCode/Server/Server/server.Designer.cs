namespace Server
{
    partial class server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(server));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ButClear = new System.Windows.Forms.Button();
            this.dataDownload = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtgvFile = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgv = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Butstop = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BoxPort = new System.Windows.Forms.TextBox();
            this.ButQuit = new System.Windows.Forms.Button();
            this.ButListen = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.TrayIcon.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataDownload)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvFile)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.TrayIcon;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Server";
            // 
            // TrayIcon
            // 
            this.TrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.endToolStripMenuItem,
            this.toolStripSeparator1});
            this.TrayIcon.Name = "contextMenuStrip1";
            this.TrayIcon.Size = new System.Drawing.Size(139, 54);
            this.TrayIcon.Text = "TrayIcon";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Image = global::Server.Properties.Resources.restart;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.startToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.startToolStripMenuItem.Text = "&Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // endToolStripMenuItem
            // 
            this.endToolStripMenuItem.Image = global::Server.Properties.Resources.GT2_OD_quit;
            this.endToolStripMenuItem.Name = "endToolStripMenuItem";
            this.endToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.endToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.endToolStripMenuItem.Text = "&End";
            this.endToolStripMenuItem.Click += new System.EventHandler(this.endToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // tabPage2
            // 
            this.tabPage2.AllowDrop = true;
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1046, 579);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Download";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox3.Controls.Add(this.ButClear);
            this.groupBox3.Controls.Add(this.dataDownload);
            this.groupBox3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox3.Location = new System.Drawing.Point(11, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1027, 554);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Client Download :";
            // 
            // ButClear
            // 
            this.ButClear.BackColor = System.Drawing.Color.Azure;
            this.ButClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButClear.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButClear.Location = new System.Drawing.Point(476, 521);
            this.ButClear.Name = "ButClear";
            this.ButClear.Size = new System.Drawing.Size(75, 25);
            this.ButClear.TabIndex = 3;
            this.ButClear.Text = "Clear all";
            this.ButClear.UseVisualStyleBackColor = false;
            this.ButClear.Click += new System.EventHandler(this.ButClear_Click);
            // 
            // dataDownload
            // 
            this.dataDownload.AllowUserToDeleteRows = false;
            this.dataDownload.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataDownload.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataDownload.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dataDownload.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataDownload.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataDownload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataDownload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.Column11,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.Column12,
            this.Column15,
            this.Column16,
            this.Column17,
            this.dataGridViewCheckBoxColumn2});
            this.dataDownload.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dataDownload.GridColor = System.Drawing.SystemColors.Desktop;
            this.dataDownload.Location = new System.Drawing.Point(13, 39);
            this.dataDownload.MultiSelect = false;
            this.dataDownload.Name = "dataDownload";
            this.dataDownload.ReadOnly = true;
            this.dataDownload.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataDownload.RowHeadersVisible = false;
            this.dataDownload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataDownload.Size = new System.Drawing.Size(1001, 476);
            this.dataDownload.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 50F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Client Load";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 50F;
            this.dataGridViewTextBoxColumn4.HeaderText = "IP Load";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.FillWeight = 50F;
            this.Column11.HeaderText = "Port Load";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 50F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Client Share";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 50F;
            this.dataGridViewTextBoxColumn6.HeaderText = "IP Share";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.FillWeight = 50F;
            this.Column12.HeaderText = "Port Share";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.FillWeight = 50F;
            this.Column15.HeaderText = "Status";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.FillWeight = 120F;
            this.Column16.HeaderText = "File Name";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // Column17
            // 
            this.Column17.FillWeight = 50F;
            this.Column17.HeaderText = "Size (Byte)";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.FillWeight = 40F;
            this.dataGridViewCheckBoxColumn2.HeaderText = "Finish";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1046, 579);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Client - File";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox2.Controls.Add(this.dtgvFile);
            this.groupBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox2.Location = new System.Drawing.Point(466, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 471);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File State :";
            // 
            // dtgvFile
            // 
            this.dtgvFile.AllowUserToDeleteRows = false;
            this.dtgvFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvFile.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dtgvFile.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dtgvFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgvFile.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column13,
            this.Column3,
            this.Column4,
            this.Column14,
            this.Column5});
            this.dtgvFile.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dtgvFile.GridColor = System.Drawing.SystemColors.Desktop;
            this.dtgvFile.Location = new System.Drawing.Point(7, 22);
            this.dtgvFile.MultiSelect = false;
            this.dtgvFile.Name = "dtgvFile";
            this.dtgvFile.ReadOnly = true;
            this.dtgvFile.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dtgvFile.RowHeadersVisible = false;
            this.dtgvFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvFile.Size = new System.Drawing.Size(559, 435);
            this.dtgvFile.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 200F;
            this.Column1.HeaderText = "File Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 80F;
            this.Column2.HeaderText = "Size (Byte)";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.FillWeight = 60F;
            this.Column13.HeaderText = "User ";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 59.08629F;
            this.Column3.HeaderText = "IP";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 59.08629F;
            this.Column4.HeaderText = "Port ";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.FillWeight = 40F;
            this.Column14.HeaderText = "Share ";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 50F;
            this.Column5.HeaderText = "Download";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox1.Controls.Add(this.dtgv);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox1.Location = new System.Drawing.Point(11, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 471);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client State :";
            // 
            // dtgv
            // 
            this.dtgv.AllowUserToDeleteRows = false;
            this.dtgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgv.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dtgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.dtgv.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dtgv.Location = new System.Drawing.Point(7, 22);
            this.dtgv.MultiSelect = false;
            this.dtgv.Name = "dtgv";
            this.dtgv.ReadOnly = true;
            this.dtgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dtgv.RowHeadersVisible = false;
            this.dtgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgv.Size = new System.Drawing.Size(420, 435);
            this.dtgv.TabIndex = 0;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "UserName";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "PassWord";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "IP Adress";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.FillWeight = 70F;
            this.Column9.HeaderText = "Listen Port";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.FillWeight = 40F;
            this.Column10.HeaderText = "Status";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.Butstop);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BoxPort);
            this.panel1.Controls.Add(this.ButQuit);
            this.panel1.Controls.Add(this.ButListen);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel1.Location = new System.Drawing.Point(11, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1027, 74);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Server.Properties.Resources.restart;
            this.pictureBox2.Location = new System.Drawing.Point(928, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(99, 74);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // Butstop
            // 
            this.Butstop.BackColor = System.Drawing.Color.Azure;
            this.Butstop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Butstop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Butstop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Butstop.Location = new System.Drawing.Point(93, 43);
            this.Butstop.Name = "Butstop";
            this.Butstop.Size = new System.Drawing.Size(82, 25);
            this.Butstop.TabIndex = 1;
            this.Butstop.Text = "Stop Listening ";
            this.Butstop.UseVisualStyleBackColor = false;
            this.Butstop.Click += new System.EventHandler(this.Butstop_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox1.Location = new System.Drawing.Point(526, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(183, 18);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Defaute Listening on Port : 8080.";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.AliceBlue;
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(122, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = " Server Start Listening on Port (Port Defaute : 8080) : ";
            // 
            // BoxPort
            // 
            this.BoxPort.BackColor = System.Drawing.SystemColors.Info;
            this.BoxPort.Location = new System.Drawing.Point(387, 11);
            this.BoxPort.Name = "BoxPort";
            this.BoxPort.Size = new System.Drawing.Size(112, 20);
            this.BoxPort.TabIndex = 0;
            this.BoxPort.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // ButQuit
            // 
            this.ButQuit.BackColor = System.Drawing.Color.Azure;
            this.ButQuit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButQuit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButQuit.Location = new System.Drawing.Point(703, 43);
            this.ButQuit.Name = "ButQuit";
            this.ButQuit.Size = new System.Drawing.Size(82, 25);
            this.ButQuit.TabIndex = 2;
            this.ButQuit.Text = "Quit Server ";
            this.ButQuit.UseVisualStyleBackColor = false;
            this.ButQuit.Click += new System.EventHandler(this.button2_Click);
            // 
            // ButListen
            // 
            this.ButListen.BackColor = System.Drawing.Color.Azure;
            this.ButListen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButListen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButListen.Location = new System.Drawing.Point(399, 42);
            this.ButListen.Name = "ButListen";
            this.ButListen.Size = new System.Drawing.Size(88, 26);
            this.ButListen.TabIndex = 0;
            this.ButListen.Text = "Start Listening";
            this.ButListen.UseVisualStyleBackColor = false;
            this.ButListen.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1054, 606);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.AliceBlue;
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1046, 579);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "About";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Image = global::Server.Properties.Resources.restart;
            this.pictureBox1.Location = new System.Drawing.Point(424, 335);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 195);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(295, 223);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(459, 96);
            this.panel3.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(455, 96);
            this.label7.TabIndex = 5;
            this.label7.Text = "Author  : -  0812231_Nguyễn Hoàng Đăng Khoa\r\n              -  0812195_Huỳnh Phi H" +
                "ùng.\r\nVersion:   v1.0 Beta\r\n\r\n";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(261, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(526, 146);
            this.panel2.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Lucida Handwriting", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(524, 144);
            this.label6.TabIndex = 4;
            this.label6.Text = " INFORMATION ABOUT \r\nSOFTWARE DOWNLOAD\r\n           ---o0o---";
            // 
            // server
            // 
            this.AcceptButton = this.ButListen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CancelButton = this.ButQuit;
            this.ClientSize = new System.Drawing.Size(1054, 606);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "server";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.server_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.server_FormClosing);
            this.Resize += new System.EventHandler(this.server_Resize);
            this.TrayIcon.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataDownload)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvFile)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip TrayIcon;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataDownload;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dtgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dtgvFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BoxPort;
        private System.Windows.Forms.Button ButQuit;
        private System.Windows.Forms.Button ButListen;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ButClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button Butstop;
    }
}

