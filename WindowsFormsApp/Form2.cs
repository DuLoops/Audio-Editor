using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;//needed to perform P/Invoke - win32 calls
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace WindowsFormsApp
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            ChartArea ca = dftChart.ChartAreas[0];  // quick reference

            ca.CursorX.IsUserEnabled = true;
            ca.CursorX.IsUserSelectionEnabled = true;
            ca.CursorX.AutoScroll = true;
            ca.CursorX.AutoScroll = false;
            ca.AxisX.ScaleView.Zoomable = false;
            ca.AxisX.ScrollBar.Enabled = true;
            ca.AxisX.Minimum = 0;
            ca.AxisX.Maximum = Double.NaN;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void displayDFT(Complex[] cArray)
        {
            dftChart.Series[0].Points.Clear();
            for (int i = 0; i < cArray.Length; i++)
            {
                dftChart.Series[0].Points.Add(Math.Sqrt(Math.Pow(cArray[i].re, 2) + Math.Pow(cArray[i].im, 2)));
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        public void IDFT(Complex[] A)
        {
            int len = A.Length;
            double[] S = new double[len];

            for (int t = 0; t < len; t++)
            {
                S[t] = 0;
                for (int f = 0; f < len; f++)
                {
                    S[t] += A[f].re * Math.Cos(2 * Math.PI * f * t / len) - A[f].im * Math.Sin(2 * Math.PI * f * t / len);
                }
            }
            //doubleArray = S;
        }
    }
}
