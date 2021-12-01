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
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace WindowsFormsApp
{
    public struct Complex
    {
        public double re;
        public double im;
    }

    public unsafe partial class Form1 : Form
    {
        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern int start();

        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr getBuffer();

        [DllImport("DLL.dll", CallingConvention = CallingConvention.Cdecl ,CharSet = CharSet.Auto)]
        public static extern void setSaveBuffer(byte* ptr, long dLen, int sps, int bps, int nBlock, int wbps);

        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern uint getBufferLen();

        [DllImport("DLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern void clipBuffer(byte* newbuffer, long dLen);

        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern void play();

        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern void record();
        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern void end();
        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern void pause();

        WaveHeader waveHeader = new WaveHeader();
        private readonly Stack<ZoomFrame> _zoomFrames = new Stack<ZoomFrame>();
        byte[] byteArray;
        double[] doubleArray;
        long userSelectionStart, userSelectionEnd;
        Boolean copied = false;

        public void readHeader(string filename)
        {
            BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open));
            waveHeader.reset();
            if (byteArray != null)
            {
                Array.Clear(byteArray, 0, byteArray.Length);
                Array.Clear(doubleArray, 0, doubleArray.Length);
            }
            waveHeader.chunkID = reader.ReadInt32();
            waveHeader.chunkSize = reader.ReadInt32();
            waveHeader.format = reader.ReadInt32();
            waveHeader.subchunk1ID = reader.ReadInt32();
            waveHeader.subchunk1Size = reader.ReadInt32();
            waveHeader.audioFormat = reader.ReadInt16();
            waveHeader.numChannels = reader.ReadInt16();
            waveHeader.sampleRate = reader.ReadInt32();
            waveHeader.byteRate = reader.ReadInt32();
            waveHeader.blockAligh = reader.ReadInt16();
            waveHeader.bps = reader.ReadInt16();
            waveHeader.subchunk2ID = reader.ReadInt32();
            waveHeader.subchunk2Size = reader.ReadInt32();
            byteArray = reader.ReadBytes(waveHeader.subchunk2Size);

            short[] shortArray = new short[waveHeader.subchunk2Size / waveHeader.blockAligh];

            for (int i = 0; i < shortArray.Length; i++)
            {
                shortArray[i] = BitConverter.ToInt16(byteArray, i * waveHeader.blockAligh);
            }
            doubleArray = shortArray.Select(x => (double)x).ToArray();
            //setBuffer((IntPtr)byteArray);
 
            fixed (byte* byteP = byteArray)
            {
                setSaveBuffer(byteP, byteArray.Length, waveHeader.sampleRate, waveHeader.byteRate, waveHeader.blockAligh, waveHeader.bps);
            }
        }

        public void displayWave()
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < doubleArray.Length; i++)
            {
                chart1.Series[0].Points.Add(doubleArray[i]);
            }
        }

        public Complex[] DFT()
        {
            long start =  userSelectionStart = (long)chart1.ChartAreas[0].CursorX.SelectionStart;
            long end = userSelectionEnd = (long)chart1.ChartAreas[0].CursorX.SelectionEnd;
            if (end > doubleArray.Length)
            {
                end = doubleArray.Length - 1;
            }

            int dftLen = (int)(end - start);
            Complex[] cArray = new Complex[dftLen];
            
            for (int f = 0; f < dftLen; f++)
            {
                cArray[f].im = 0;
                cArray[f].re = 0;
                for (int t = 0; t < dftLen; t++)
                {
                    cArray[f].re += byteArray[t] * Math.Cos(2 * Math.PI * t * f / dftLen);
                    cArray[f].im -= byteArray[t] * Math.Sin(2 * Math.PI * t * f / dftLen);
                }
                cArray[f].re /= dftLen;
                cArray[f].im /= dftLen;
            }
            
            return cArray;
        }


        public void styleChart()
        {
            ChartArea ca = chart1.ChartAreas[0];  // quick reference

            ca.CursorX.IsUserEnabled = true;
            ca.CursorX.IsUserSelectionEnabled = true;
            ca.CursorX.AutoScroll = true;
            ca.CursorX.AutoScroll = false;
            ca.AxisX.ScaleView.Zoomable = false;
            ca.AxisX.ScrollBar.Enabled = true;
            ca.AxisX.Minimum = 0;
            ca.AxisX.Maximum = Double.NaN;
            this.chart1.MouseWheel += chart1_MouseWheel;
        }


        private class ZoomFrame
        {
            public double XStart { get; set; }
            public double XFinish { get; set; }
            public double YStart { get; set; }
            public double YFinish { get; set; }
        }
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;

            try
            {
                if (e.Delta < 0)
                {
                    if (0 < _zoomFrames.Count)
                    {
                        var frame = _zoomFrames.Pop();
                        if (_zoomFrames.Count == 0)
                        {
                            xAxis.ScaleView.ZoomReset();
                        }
                        else
                        {
                            xAxis.ScaleView.Zoom(frame.XStart, frame.XFinish);
                        }
                    }
                }
                else if (e.Delta > 0)
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;


                    _zoomFrames.Push(new ZoomFrame { XStart = xMin, XFinish = xMax});

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;


                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                }
            }
            catch { }
        }


        public Form1()
        {
            InitializeComponent();
            styleChart();
            loadBtn.Enabled = false;

            unsafe
            {
                start();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openWaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Wave File ((.wav)|*.wav;";
            if (open.ShowDialog() != DialogResult.OK) return;
            readHeader(open.FileName);
            //waveViewer1.WaveStream = new NAudio.Wave.WaveFileReader(open.FileName);
            displayWave();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }


        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void pause_Click(object sender, EventArgs e)
        {
            pause();
        }

        static byte[] GetBytesAlt(double[] dArray)
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

        private void copy_Click(object sender, EventArgs e)
        {
            copied = true;
            userSelectionStart = (long)chart1.ChartAreas[0].CursorX.SelectionStart;
            userSelectionEnd= (long)chart1.ChartAreas[0].CursorX.SelectionEnd;
            if (userSelectionEnd > doubleArray.Length)
            {
                userSelectionEnd = doubleArray.Length - 1;
            }
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            long pasteDest = (long)chart1.ChartAreas[0].CursorX.SelectionStart;
            double[] temp = new double[doubleArray.Length + (userSelectionEnd - userSelectionStart)];
            long tempCounter = 0;
            long copyCounter = userSelectionStart;
            for (int i = 0; i < doubleArray.Length; i++)
            {
                if(i == pasteDest)
                {
                    while (copyCounter < userSelectionEnd)
                    {
                        temp[tempCounter++] = doubleArray[copyCounter++];
                    }
                }
                temp[tempCounter++] = doubleArray[i];
            }
            doubleArray = temp;
            byteArray = GetBytesAlt(doubleArray);
            waveHeader.subchunk2Size = byteArray.Length;

            fixed (byte* byteP = byteArray)
            {
                clipBuffer(byteP, byteArray.Length);
            }

            displayWave();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            ChartArea ca = chart1.ChartAreas[0];
            long start = (long)ca.CursorX.SelectionStart;
            long end = (long)ca.CursorX.SelectionEnd;
            if (end > doubleArray.Length)
            {
                end = doubleArray.Length - 1;
            }
            double[] temp = new double[doubleArray.Length - (end - start)];
            int tempCounter = 0;
            for (int i = 0; i < doubleArray.Length; i++)
            {
                while (i >= start && i < end) { i++; }
                temp[tempCounter++] = doubleArray[i];
            }
            doubleArray = temp;
            byteArray = GetBytesAlt(doubleArray);
            waveHeader.subchunk2Size = byteArray.Length;

            fixed (byte* byteP = byteArray)
            {
                clipBuffer(byteP, byteArray.Length);
            }

            displayWave();
            ca.CursorX.SetSelectionPosition(0, 0);
        }
        private void play_click(object sender, EventArgs e)
        {
            unsafe
            {
                play();
            }
        }

        private void recordBtn_Click(object sender, EventArgs e)
        {
            record();
        }

        private void endBtn_Click(object sender, EventArgs e)
        {
            end();
            loadAudio();
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
        }

        public void loadAudio()
        {
            IntPtr bufferP = getBuffer();
            uint bufferLen = getBufferLen();
            if (byteArray != null)
            {
                Array.Clear(byteArray, 0, byteArray.Length);
                Array.Clear(doubleArray, 0, doubleArray.Length);
            }
            byteArray = new byte[bufferLen];

            unsafe
            {
                byte* bPointer = (byte*)bufferP;
                for (int i = 0; i < bufferLen; i++)
                {
                    byteArray[i] = bPointer[i];
                }
            }

            short[] sdata = new short[(int)Math.Ceiling((double)byteArray.Length / 2)];
            Buffer.BlockCopy(byteArray, 0, sdata, 0, byteArray.Length);
            doubleArray = sdata.Select(x => (double)x).ToArray();

            displayWave();
        }

        private void dft_click(object sender, EventArgs e)
        {
            if (chart1.ChartAreas[0].CursorX.SelectionEnd == chart1.ChartAreas[0].CursorX.SelectionStart ||  double.IsNaN(chart1.ChartAreas[0].CursorX.SelectionEnd))
            {
                MessageBox.Show("Select values first");
                return;
            }
            Form2 dftForm = new Form2();
            dftForm.displayDFT(DFT());
            
            dftForm.Show();
        }
    }
}
