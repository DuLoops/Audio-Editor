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
        int[] bins;
        int N = 50;
        int filterSize;
        int sampleRate;

        // Form 2 (Takes In Complex Array, Length Of Buffer and Sample Rate)
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
            sampleRate = 44800;
            cArray = inputArray;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        // Display DFT Value On Graph
        public void displayDFT()
        {
            dftChart.Series[0].Points.Clear();
            for (int i = 0; i < cArray.Length; i++)
            {
                dftChart.Series[0].Points.Add(Math.Sqrt(Math.Pow(cArray[i].re, 2) + Math.Pow(cArray[i].im, 2)));
            }
        }

        // Select Part Of the Graph Function
        private void chart1_Click(object sender, EventArgs e)
        {
            long selected = (long)dftChart.ChartAreas[0].CursorX.Position;
            if (selected >= cArray.Length / 2)
            {
                selected = cArray.Length / 2;
            }
            dftChart.ChartAreas[0].CursorX.SelectionEnd = selected;
        }

        // Perform IDFT On Wave
        public double[] IDFT(Complex[] A)
        {
            int len = A.Length;
            double[] dArray = new double[len];

            for (int t = 0; t < len; t++)
            {
                dArray[t] = 0;
                for (int f = 0; f < len; f++)
                {
                    dArray[t] += A[f].re * Math.Cos(2 * Math.PI * f * t / len) - A[f].im * Math.Sin(2 * Math.PI * f * t / len);
                }
            }
            return dArray;
        }


        // Create Low Pass Filter Button
        private void lowBtn_Click(object sender, EventArgs e)
        {
            if (double.IsNaN(dftChart.ChartAreas[0].CursorX.SelectionEnd))
            {
                MessageBox.Show("Select values first");
                return;
            }
            int[] filter;
            double cutOffFreq = dftChart.ChartAreas[0].CursorX.Position;

            Trace.WriteLine("Cut off Freq: " + cutOffFreq);
            Trace.WriteLine("N: " + N);
            Trace.WriteLine("SampleRate: " + sampleRate + "\nIDFT filter:");

            int filterIndex = (int)Math.Ceiling(cutOffFreq * N / sampleRate);
            if (lowfilter.Checked)
            {
                filter = createLowFilter(filterIndex);
            }
            else
            {
                filter = createHighFilter(filterIndex);
            }
            
            double[] newFilter = IDFTfilter(filter);
            for (int i = 0; i < newFilter.Length; i++)
            {
                Trace.WriteLine(newFilter[i]);
            }
            convertBins(filter);

        }

        // Perform IDFT on Filtered Value
        // Pre: filter as Integer Array
        public double[] IDFTfilter(int[] filter) {
            Complex[] complexFilter = new Complex[filter.Length];
            for (int i = 0; i < filter.Length; i++)
            {
                complexFilter[i].re = filter[i];
                complexFilter[i].im = 0;
            }
            return (IDFT(complexFilter));
        }

        // Check Filter Change
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


        // Perform Low Pass Filter
        public int[] createLowFilter(int filterIndex)
        {
            int[] filter = new int[N];
            int counter = 0;
            for (; counter < filterIndex + 1; counter++)
            {
                filter[counter] = 1;
            }
            for (; counter < N - filterIndex; counter++)
            {
                filter[counter] = 0;
            }
            for (; counter < N; counter++)
            {
                filter[counter] = 1;
            }
            return filter;
        }

        // Perform High Pass Filter
        public int[] createHighFilter(int filterIndex)
        {
            int[] filter = new int[N];
            int counter = 0;
            for (; counter < filterIndex + 1; counter++)
            {
                filter[counter] = 0;
            }
            for (; counter < N - filterIndex; counter++)
            {
                filter[counter] = 1;
            }
            for (; counter < N; counter++)
            {
                filter[counter] = 0;
            }
            return filter;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            N = (int)nVal.Value;
        }
        public void convertBins(int[] filter)
        {
            bins = new int[N];
            for (int i = 0; i< cArray.Length; i++)
            {
                //Trace.WriteLine(cArray[i].re);
            }
            for (int i = 0; i < N; i++)
            {
                //bins[i] = cArray[]
            }
        }
    }
}
