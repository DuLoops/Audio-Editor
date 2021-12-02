
namespace WindowsFormsApp
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cut = new System.Windows.Forms.Button();
            this.Paste = new System.Windows.Forms.Button();
            this.copy = new System.Windows.Forms.Button();
            this.recordBtn = new System.Windows.Forms.Button();
            this.endBtn = new System.Windows.Forms.Button();
            this.recordedRadio = new System.Windows.Forms.CheckBox();
            this.importedRadio = new System.Windows.Forms.CheckBox();
            this.chartRecorded = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.directoryEntry2 = new System.DirectoryServices.DirectoryEntry();
            this.directoryEntry3 = new System.DirectoryServices.DirectoryEntry();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.windowBtn = new System.Windows.Forms.Button();
            this.benchmark = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRecorded)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1519, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWaveToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // openWaveToolStripMenuItem
            // 
            this.openWaveToolStripMenuItem.Name = "openWaveToolStripMenuItem";
            this.openWaveToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.openWaveToolStripMenuItem.Text = "Open .wav";
            this.openWaveToolStripMenuItem.Click += new System.EventHandler(this.openWaveToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Chocolate;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(77, 528);
            this.chart1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1309, 282);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.Location = new System.Drawing.Point(452, 96);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "DFT/Filter";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.dft_click);
            // 
            // playBtn
            // 
            this.playBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.playBtn.Location = new System.Drawing.Point(81, 53);
            this.playBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(106, 37);
            this.playBtn.TabIndex = 5;
            this.playBtn.Text = "Play";
            this.playBtn.UseVisualStyleBackColor = false;
            this.playBtn.Click += new System.EventHandler(this.play_click);
            // 
            // pauseBtn
            // 
            this.pauseBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.pauseBtn.Location = new System.Drawing.Point(234, 53);
            this.pauseBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(109, 38);
            this.pauseBtn.TabIndex = 6;
            this.pauseBtn.Text = "Pause";
            this.pauseBtn.UseVisualStyleBackColor = false;
            this.pauseBtn.Click += new System.EventHandler(this.pause_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Cut);
            this.groupBox1.Controls.Add(this.Paste);
            this.groupBox1.Controls.Add(this.copy);
            this.groupBox1.Location = new System.Drawing.Point(990, 53);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(429, 114);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit";
            // 
            // Cut
            // 
            this.Cut.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Cut.Location = new System.Drawing.Point(315, 25);
            this.Cut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cut.Name = "Cut";
            this.Cut.Size = new System.Drawing.Size(98, 74);
            this.Cut.TabIndex = 2;
            this.Cut.Text = "Cut";
            this.Cut.UseVisualStyleBackColor = false;
            this.Cut.Click += new System.EventHandler(this.Cut_Click);
            // 
            // Paste
            // 
            this.Paste.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Paste.Location = new System.Drawing.Point(172, 25);
            this.Paste.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(98, 74);
            this.Paste.TabIndex = 1;
            this.Paste.Text = "Paste";
            this.Paste.UseVisualStyleBackColor = false;
            this.Paste.Click += new System.EventHandler(this.Paste_Click);
            // 
            // copy
            // 
            this.copy.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.copy.Location = new System.Drawing.Point(30, 25);
            this.copy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(98, 74);
            this.copy.TabIndex = 0;
            this.copy.Text = "Copy";
            this.copy.UseVisualStyleBackColor = false;
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // recordBtn
            // 
            this.recordBtn.BackColor = System.Drawing.Color.Coral;
            this.recordBtn.Location = new System.Drawing.Point(77, 157);
            this.recordBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.recordBtn.Name = "recordBtn";
            this.recordBtn.Size = new System.Drawing.Size(109, 38);
            this.recordBtn.TabIndex = 8;
            this.recordBtn.Text = "Record";
            this.recordBtn.UseVisualStyleBackColor = false;
            this.recordBtn.Click += new System.EventHandler(this.recordBtn_Click);
            // 
            // endBtn
            // 
            this.endBtn.BackColor = System.Drawing.Color.Coral;
            this.endBtn.Location = new System.Drawing.Point(234, 157);
            this.endBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.endBtn.Name = "endBtn";
            this.endBtn.Size = new System.Drawing.Size(109, 38);
            this.endBtn.TabIndex = 9;
            this.endBtn.Text = "End";
            this.endBtn.UseVisualStyleBackColor = false;
            this.endBtn.Click += new System.EventHandler(this.endBtn_Click);
            // 
            // recordedRadio
            // 
            this.recordedRadio.AutoSize = true;
            this.recordedRadio.BackColor = System.Drawing.SystemColors.Control;
            this.recordedRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.recordedRadio.Location = new System.Drawing.Point(81, 227);
            this.recordedRadio.Name = "recordedRadio";
            this.recordedRadio.Size = new System.Drawing.Size(103, 24);
            this.recordedRadio.TabIndex = 14;
            this.recordedRadio.Text = "Recorded";
            this.recordedRadio.UseVisualStyleBackColor = false;
            this.recordedRadio.CheckedChanged += new System.EventHandler(this.recorded_CheckedChanged);
            // 
            // importedRadio
            // 
            this.importedRadio.AutoSize = true;
            this.importedRadio.BackColor = System.Drawing.SystemColors.Control;
            this.importedRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.importedRadio.Location = new System.Drawing.Point(77, 528);
            this.importedRadio.Name = "importedRadio";
            this.importedRadio.Size = new System.Drawing.Size(96, 24);
            this.importedRadio.TabIndex = 15;
            this.importedRadio.Text = "Imported";
            this.importedRadio.UseVisualStyleBackColor = false;
            // 
            // chartRecorded
            // 
            this.chartRecorded.BackColor = System.Drawing.Color.DarkSalmon;
            chartArea2.Name = "ChartArea1";
            this.chartRecorded.ChartAreas.Add(chartArea2);
            this.chartRecorded.Location = new System.Drawing.Point(81, 227);
            this.chartRecorded.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chartRecorded.Name = "chartRecorded";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series1";
            this.chartRecorded.Series.Add(series2);
            this.chartRecorded.Size = new System.Drawing.Size(1305, 293);
            this.chartRecorded.TabIndex = 16;
            this.chartRecorded.Text = "chart2";
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // windowBtn
            // 
            this.windowBtn.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.windowBtn.Location = new System.Drawing.Point(588, 96);
            this.windowBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.windowBtn.Name = "windowBtn";
            this.windowBtn.Size = new System.Drawing.Size(111, 38);
            this.windowBtn.TabIndex = 17;
            this.windowBtn.Text = "Windowing";
            this.windowBtn.UseCompatibleTextRendering = true;
            this.windowBtn.UseVisualStyleBackColor = false;
            this.windowBtn.Click += new System.EventHandler(this.window_click);
            // 
            // benchmark
            // 
            this.benchmark.Location = new System.Drawing.Point(452, 157);
            this.benchmark.Name = "benchmark";
            this.benchmark.Size = new System.Drawing.Size(111, 38);
            this.benchmark.TabIndex = 18;
            this.benchmark.Text = "Bencmark DFT";
            this.benchmark.UseVisualStyleBackColor = true;
            this.benchmark.Click += new System.EventHandler(this.benchmark_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1519, 948);
            this.Controls.Add(this.benchmark);
            this.Controls.Add(this.windowBtn);
            this.Controls.Add(this.recordedRadio);
            this.Controls.Add(this.chartRecorded);
            this.Controls.Add(this.importedRadio);
            this.Controls.Add(this.endBtn);
            this.Controls.Add(this.recordBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pauseBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = " Karaoke Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRecorded)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWaveToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button pauseBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Cut;
        private System.Windows.Forms.Button Paste;
        private System.Windows.Forms.Button copy;
        private System.Windows.Forms.Button recordBtn;
        private System.Windows.Forms.Button endBtn;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
// <<<<<<< dujin
        private System.Windows.Forms.CheckBox recordedRadio;
        private System.Windows.Forms.CheckBox importedRadio;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRecorded;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.DirectoryServices.DirectoryEntry directoryEntry2;
        private System.DirectoryServices.DirectoryEntry directoryEntry3;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Button windowBtn;
        private System.Windows.Forms.Button benchmark;
        // =======
        //         private System.Windows.Forms.Button button2;
        // >>>>>>> master
    }
}

