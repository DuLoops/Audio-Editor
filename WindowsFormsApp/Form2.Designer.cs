
namespace WindowsFormsApp
{
    partial class Form2
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
            this.dftChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.saveBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nVal = new System.Windows.Forms.NumericUpDown();
            this.lowfilter = new System.Windows.Forms.RadioButton();
            this.highfilter = new System.Windows.Forms.RadioButton();
            this.filter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dftChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nVal)).BeginInit();
            this.SuspendLayout();
            // 
            // dftChart
            // 
            chartArea1.Name = "ChartArea1";
            this.dftChart.ChartAreas.Add(chartArea1);
            this.dftChart.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dftChart.Location = new System.Drawing.Point(11, 115);
            this.dftChart.Name = "dftChart";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.dftChart.Series.Add(series1);
            this.dftChart.Size = new System.Drawing.Size(741, 367);
            this.dftChart.TabIndex = 0;
            this.dftChart.Text = "chart1";
            this.dftChart.Click += new System.EventHandler(this.chart1_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(539, 34);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(160, 50);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Save/IDFT";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nVal);
            this.groupBox1.Controls.Add(this.lowfilter);
            this.groupBox1.Controls.Add(this.highfilter);
            this.groupBox1.Controls.Add(this.filter);
            this.groupBox1.Location = new System.Drawing.Point(59, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(248, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // nVal
            // 
            this.nVal.Location = new System.Drawing.Point(147, 20);
            this.nVal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nVal.Name = "nVal";
            this.nVal.Size = new System.Drawing.Size(96, 22);
            this.nVal.TabIndex = 3;
            this.nVal.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // lowfilter
            // 
            this.lowfilter.AutoSize = true;
            this.lowfilter.Location = new System.Drawing.Point(18, 32);
            this.lowfilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lowfilter.Name = "lowfilter";
            this.lowfilter.Size = new System.Drawing.Size(98, 20);
            this.lowfilter.TabIndex = 2;
            this.lowfilter.TabStop = true;
            this.lowfilter.Text = "Low - Pass";
            this.lowfilter.UseVisualStyleBackColor = true;
            this.lowfilter.CheckedChanged += new System.EventHandler(this.lowfilter_CheckedChanged);
            // 
            // highfilter
            // 
            this.highfilter.AutoSize = true;
            this.highfilter.Location = new System.Drawing.Point(18, 66);
            this.highfilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.highfilter.Name = "highfilter";
            this.highfilter.Size = new System.Drawing.Size(102, 20);
            this.highfilter.TabIndex = 3;
            this.highfilter.TabStop = true;
            this.highfilter.Text = "High - Pass";
            this.highfilter.UseVisualStyleBackColor = true;
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(147, 50);
            this.filter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(96, 46);
            this.filter.TabIndex = 0;
            this.filter.Text = "Filter";
            this.filter.UseVisualStyleBackColor = true;
            this.filter.Click += new System.EventHandler(this.filter_click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 493);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.dftChart);
            this.Name = "Form2";
            this.Text = "DFT";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dftChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nVal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart dftChart;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button filter;
        private System.Windows.Forms.RadioButton highfilter;
        private System.Windows.Forms.RadioButton lowfilter;
        private System.Windows.Forms.NumericUpDown nVal;
    }
}