
namespace WindowsFormsApp
{
    partial class Form3
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dftChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DFT = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dftChart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea7.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea7);
            this.chart1.Location = new System.Drawing.Point(482, 77);
            this.chart1.Name = "chart1";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Name = "Series1";
            this.chart1.Series.Add(series7);
            this.chart1.Size = new System.Drawing.Size(410, 330);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // dftChart
            // 
            chartArea8.Name = "ChartArea1";
            this.dftChart.ChartAreas.Add(chartArea8);
            this.dftChart.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dftChart.Location = new System.Drawing.Point(43, 77);
            this.dftChart.Name = "dftChart";
            series8.ChartArea = "ChartArea1";
            series8.Name = "Series1";
            this.dftChart.Series.Add(series8);
            this.dftChart.Size = new System.Drawing.Size(410, 330);
            this.dftChart.TabIndex = 4;
            this.dftChart.Text = "chart1";
            this.dftChart.Click += new System.EventHandler(this.dftChart_Click);
            // 
            // DFT
            // 
            this.DFT.AutoSize = true;
            this.DFT.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.DFT.Location = new System.Drawing.Point(167, 29);
            this.DFT.Name = "DFT";
            this.DFT.Size = new System.Drawing.Size(172, 24);
            this.DFT.TabIndex = 5;
            this.DFT.Text = "Frequency Domain";
            this.DFT.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(626, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Time Domain";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 449);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DFT);
            this.Controls.Add(this.dftChart);
            this.Controls.Add(this.chart1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dftChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart dftChart;
        private System.Windows.Forms.Label DFT;
        private System.Windows.Forms.Label label1;
    }
}