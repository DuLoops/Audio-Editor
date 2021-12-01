
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dftChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.saveBtn = new System.Windows.Forms.Button();
            this.filterBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dftChart)).BeginInit();
            this.SuspendLayout();
            // 
            // dftChart
            // 
            chartArea1.Name = "ChartArea1";
            this.dftChart.ChartAreas.Add(chartArea1);
            this.dftChart.Cursor = System.Windows.Forms.Cursors.Arrow;
            legend1.Name = "Legend1";
            this.dftChart.Legends.Add(legend1);
            this.dftChart.Location = new System.Drawing.Point(32, 144);
            this.dftChart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dftChart.Name = "dftChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.dftChart.Series.Add(series1);
            this.dftChart.Size = new System.Drawing.Size(687, 392);
            this.dftChart.TabIndex = 0;
            this.dftChart.Text = "chart1";
            this.dftChart.Click += new System.EventHandler(this.chart1_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(640, 42);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(180, 62);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Save/IDFT";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // filterBtn
            // 
            this.filterBtn.Location = new System.Drawing.Point(99, 31);
            this.filterBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(184, 72);
            this.filterBtn.TabIndex = 2;
            this.filterBtn.Text = "Filter";
            this.filterBtn.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.filterBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.dftChart);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dftChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart dftChart;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button filterBtn;
    }
}