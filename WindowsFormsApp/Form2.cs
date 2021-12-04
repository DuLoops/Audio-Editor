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
using System.Threading;
using static WindowsFormsApp.WaveHeader;

namespace WindowsFormsApp
{
    public partial class Form2 : Form
    {
        //Contains dft buffer.
        Complex[] cArray;
        double[] dArray;
        byte[] byteArray;
        int[] bins;
        int N = 50;
        int filterSize;
        int sampleRate;
        int numThread = 6;
        WaveHeader waveHeader;

        public Form2(Complex[] inputArray, WaveHeader _waveHeader, double[] _dArray)
        {


            InitializeComponent();
            ChartArea ca = dftChart.ChartAreas[0];  // quick reference

            ca.CursorX.IsUserEnabled = true;
            ca.CursorX.IsUserSelectionEnabled = false;
            ca.CursorX.AutoScroll = false;
            ca.AxisX.ScaleView.Zoomable = false;
            lowfilter.Checked = true;
            ca.CursorX.SelectionStart = 0;

            waveHeader = _waveHeader;
            sampleRate = waveHeader.sampleRate;
            cArray = inputArray;
            dArray = _dArray;
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
            if (selected >= cArray.Length / 2)
            {
                selected = cArray.Length / 2;
            }
            dftChart.ChartAreas[0].CursorX.SelectionEnd = selected;
        }

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



        private void filter_click(object sender, EventArgs e)
        {
            if (double.IsNaN(dftChart.ChartAreas[0].CursorX.SelectionEnd))
            {
                MessageBox.Show("Select values first");
                return;
            }
            int[] filter;
            double cutOffFreq = dftChart.ChartAreas[0].CursorX.Position;

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
            convolution(newFilter);
            

        }

        public double[] IDFTfilter(int[] filter) {
            Complex[] complexFilter = new Complex[filter.Length];
            for (int i = 0; i < filter.Length; i++)
            {
                complexFilter[i].re = filter[i];
                complexFilter[i].im = 0;
            }
            return (IDFT(complexFilter));
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

        public void convolution(double[] filter)
        {
            double[] temp = new double[filter.Length];
            double[] newDArray = new double[dArray.Length];
            for (int i = 0; i< cArray.Length; i++)
            {
                for (int j = 0; j < filter.Length; j++)
                {
                    if (i + j <= dArray.Length)
                    {
                        temp[j] = filter[j] * dArray[i + j];

                    }
                }
                newDArray[i] = sumArray(temp);
            }
            dArray = newDArray;
            byteArray = convertDoubleToByteArray(dArray);
            DFTthreading();
            displayDFT();
        }

        public double sumArray(double[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

        static byte[] convertDoubleToByteArray(double[] dArray)
        {
            short[] sArray = new short[dArray.Length];
            sArray = dArray.Select(x => (short)(x)).ToArray();
            List<Byte> bList = new List<Byte>();
            for (int i = 0; i < sArray.Length; i++)
            {
                bList.AddRange(BitConverter.GetBytes(sArray[i]).ToList());
            }
            return bList.ToArray();
        }

        
        public Complex[] DFTthreading()
        {   
            
            
            int dftLen = dArray.Length;
            Complex[] cArray = new Complex[dftLen];
            Thread[] t = new Thread[numThread];
            Mutex mutex = new Mutex();

            t[0] = new Thread(() => DFTthreadProc(cArray, 0, dftLen));
            t[1] = new Thread(() => DFTthreadProc(cArray, 1, dftLen));
            t[2] = new Thread(() => DFTthreadProc(cArray, 2, dftLen));
            t[3] = new Thread(() => DFTthreadProc(cArray, 3, dftLen));
            t[4] = new Thread(() => DFTthreadProc(cArray, 4, dftLen));
            t[5] = new Thread(() => DFTthreadProc(cArray, 5, dftLen));
            t[0].Start();
            t[1].Start();
            t[2].Start();
            t[3].Start();
            t[4].Start();
            t[5].Start();
            t[0].Join();
            t[1].Join();
            t[2].Join();
            t[3].Join();
            t[4].Join();
            t[5].Join();
            return cArray;
        }


        public  void DFTthreadProc(Complex[] cArray, int threadCount, int dftLen) {

            int chunk = (dftLen / numThread);
            Trace.WriteLine("Thread Proc count: "+ threadCount);
            for (int f = threadCount * chunk; f < ((threadCount + 1) * chunk) - 1; f++)
            { 

                cArray[f].im = 0;
                cArray[f].re = 0;
                for (int t = 0; t < dftLen; t++)
                {
                    cArray[f].re += byteArray[t] * Math.Cos(2 * Math.PI * t * f / dftLen);
                    cArray[f].im -= byteArray[t] * Math.Sin(2 * Math.PI * t * f / dftLen);

                }
                cArray[f].im /= dftLen;
                cArray[f].re /= dftLen;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            //Form1.setNewDoubleAndByteArray(dArray, byteArray);
            this.Close();
        }
    }
}
