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
        Complex[] cArray;
        int N = 100;
        int filterSize;
        int sampleRate;
        public Form2(Complex[] inputArray, int len, int sRate)
        {


            InitializeComponent();
            ChartArea ca = dftChart.ChartAreas[0];  // quick reference

            ca.CursorX.IsUserEnabled = true;
            ca.CursorX.IsUserSelectionEnabled = false;
            ca.CursorX.AutoScroll = false;
            ca.AxisX.ScaleView.Zoomable = false;
            lowfilter.Checked = true;
            ca.CursorX.SelectionStart = 0;
            nVal.Value = N;
            nVal.Maximum = 300;
            filterSize = len;
            sampleRate = len;
            cArray = inputArray;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void displayDFT()
        {
            dftChart.Series[0].Points.Clear();
            for (int i = 0; i < cArray.Length; i++)
            {
                dftChart.Series[0].Points.Add(Math.Sqrt(Math.Pow(cArray[i].re, 2) + Math.Pow(cArray[i].im, 2)));
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            long selected = (long)dftChart.ChartAreas[0].CursorX.Position;
            dftChart.ChartAreas[0].CursorX.SelectionEnd = selected;
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



        private void lowBtn_Click(object sender, EventArgs e)
        {
            if (double.IsNaN(dftChart.ChartAreas[0].CursorX.SelectionEnd))
            {
                MessageBox.Show("Select values first");
                return;
            }
            int[] filter;
            double cutOffFreq = dftChart.ChartAreas[0].CursorX.Position;

            Trace.WriteLine(cutOffFreq);
            Trace.WriteLine(N);
            Trace.WriteLine(sampleRate);

            int filterIndex = (int)Math.Ceiling(cutOffFreq * N / sampleRate);
            Trace.WriteLine(filterIndex);
            if (lowfilter.Checked)
            {
                filter = createLowFilter(filterIndex);
            }
            else
            {
                filter = createHighFilter(filterIndex);
            }
            for (int i = 0; i < filter.Length; i++)
            {
                //Trace.WriteLine(filter[i]);
            }


        }

        private void lowfilter_CheckedChanged(object sender, EventArgs e)
        {
            if(highfilter.Checked)
            {
                lowfilter.Checked = false;
                dftChart.ChartAreas[0].CursorX.SelectionStart = cArray.Length / 2;


            } else
            {
                highfilter.Checked = false;
                dftChart.ChartAreas[0].CursorX.SelectionStart = 0;

            }

        }

        public int[] createLowFilter(int filterIndex)
        {
            int[] filter = new int[filterSize];
            int counter = 0;
            for (; counter < filterIndex + 1; counter++)
            {
                filter[counter] = 1;
            }
            for (; counter < filterSize - filterIndex + 1; counter++)
            {
                filter[counter] = 0;
            }
            for (; counter < filterSize; counter++)
            {
                filter[counter] = 1;
            }
            return filter;
        }

        public int[] createHighFilter(int filterIndex)
        {
            int[] filter = new int[filterSize];
            int counter = 0;
            for (; counter < filterIndex + 1; counter++)
            {
                filter[counter] = 0;
            }
            for (; counter < filterSize - filterIndex + 1; counter++)
            {
                filter[counter] = 1;
            }
            for (; counter < filterSize; counter++)
            {
                filter[counter] = 0;
            }
            return filter;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            N = (int)nVal.Value;
        }
    }
}
