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

        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern int getSampleRate();

        [DllImport("DLL.dll", CallingConvention = CallingConvention.Cdecl,  CharSet = CharSet.Auto)]
        public static extern void setHeader(int _nSamplesPerSec, int _nAvgBytesPerSec, int _nBlockAlign, int _wBitsPerSample);

        //Record DLL
        [DllImport("recordDLL.dll", CharSet = CharSet.Auto)]
        public static extern int Rstart();

        [DllImport("recordDLL.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr RgetBuffer();

        [DllImport("recordDLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern void RsetSaveBuffer(byte* ptr, long dLen, int sps, int bps, int nBlock, int wbps);

        [DllImport("recordDLL.dll", CharSet = CharSet.Auto)]
        public static extern uint RgetBufferLen();

        [DllImport("recordDLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern void RclipBuffer(byte* newbuffer, long dLen);

        [DllImport("recordDLL.dll", CharSet = CharSet.Auto)]
        public static extern void Rplay();

        [DllImport("recordDLL.dll", CharSet = CharSet.Auto)]
        public static extern void Rrecord();
        [DllImport("recordDLL.dll", CharSet = CharSet.Auto)]
        public static extern void Rend();
        [DllImport("recordDLL.dll", CharSet = CharSet.Auto)]
        public static extern void Rpause();

        [DllImport("recordDLL.dll", CharSet = CharSet.Auto)]
        public static extern int RgetSampleRate();

        [DllImport("DLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern void RsetHeader(int _nSamplesPerSec, int _nAvgBytesPerSec, int _nBlockAlign, int _wBitsPerSample);

        WaveHeader waveHeader = new WaveHeader();
        private readonly Stack<ZoomFrame> _zoomFrames = new Stack<ZoomFrame>();
        byte[] byteArrayImport;
        double[] doubleArrayImport;
        long userSelectionStart, userSelectionEnd;
        Boolean copied = false;
        byte[] byteArrayRecorded;
        double[] doubleArrayRecorded;
        double[] doubleArrayCopy;

        public void readHeader(string filename)
        {
            BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open));
            waveHeader.reset();
            if (byteArrayImport != null)
            {
                Array.Clear(byteArrayImport, 0, byteArrayImport.Length);
                Array.Clear(doubleArrayImport, 0, doubleArrayImport.Length);
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
            byteArrayImport = reader.ReadBytes(waveHeader.subchunk2Size);

            short[] shortArray = new short[waveHeader.subchunk2Size / waveHeader.blockAligh];

            for (int i = 0; i < shortArray.Length; i++)
            {
                shortArray[i] = BitConverter.ToInt16(byteArrayImport, i * waveHeader.blockAligh);
            }
            doubleArrayImport = shortArray.Select(x => (double)x).ToArray();
            fixed (byte* byteP = byteArrayImport)
            {
                setSaveBuffer(byteP, byteArrayImport.Length, waveHeader.sampleRate, waveHeader.byteRate, waveHeader.blockAligh, waveHeader.bps);
            }
        }

        public void displayWave()
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < doubleArrayImport.Length; i++)
            {
                chart1.Series[0].Points.Add(doubleArrayImport[i]);
            }
        }

        public void displayRecorded()
        {
            chartRecorded.Series[0].Points.Clear();
            for (int i = 0; i < doubleArrayRecorded.Length; i++)
            {
                chartRecorded.Series[0].Points.Add(doubleArrayRecorded[i]);
            }
        }

        public Complex[] DFT()
        {
            long start =  userSelectionStart = (long)chartRecorded.ChartAreas[0].CursorX.SelectionStart;
            long end = userSelectionEnd = (long)chartRecorded.ChartAreas[0].CursorX.SelectionEnd;
            if (end > doubleArrayRecorded.Length)
            {
                end = doubleArrayRecorded.Length - 1;
            }

            int dftLen = (int)(end - start);
            Complex[] cArray = new Complex[dftLen];
            
            for (int f = 0; f < dftLen; f++)
            {
                cArray[f].im = 0;
                cArray[f].re = 0;
                for (int t = 0; t < dftLen; t++)
                {
                    cArray[f].re += byteArrayRecorded[t] * Math.Cos(2 * Math.PI * t * f / dftLen);
                    cArray[f].im -= byteArrayRecorded[t] * Math.Sin(2 * Math.PI * t * f / dftLen);
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

            ChartArea ca2 = chartRecorded.ChartAreas[0];  // quick reference

            ca2.CursorX.IsUserEnabled = true;
            ca2.CursorX.IsUserSelectionEnabled = true;
            ca2.CursorX.AutoScroll = true;
            ca2.CursorX.AutoScroll = false;
            ca2.AxisX.ScaleView.Zoomable = false;
            ca2.AxisX.ScrollBar.Enabled = true;
            ca2.AxisX.Minimum = 0;
            ca2.AxisX.Maximum = Double.NaN;
            this.chartRecorded.MouseWheel += chart1_MouseWheel;
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
            endBtn.Enabled = false;
            playBtn.Enabled = false;
            pauseBtn.Enabled = false;
            unsafe
            {
                start();
                Rstart();
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
            setHeader(waveHeader.sampleRate, waveHeader.byteRate, waveHeader.blockAligh, waveHeader.bps);
            //waveViewer1.WaveStream = new NAudio.Wave.WaveFileReader(open.FileName);
            displayWave();
            playBtn.Enabled = true;
            pauseBtn.Enabled = true;
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
            if (recordedRadio.Checked && importedRadio.Checked)
            {
                unsafe
                {
                    pause();
                    Rpause();
                }
            }
            else if (importedRadio.Checked)
            {
                unsafe
                {
                    pause();
                }
            }
            else if (recordedRadio.Checked)
            {
                unsafe
                {
                    Rpause();
                }
            }
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
            userSelectionStart = (long)chartRecorded.ChartAreas[0].CursorX.SelectionStart;
            userSelectionEnd= (long)chartRecorded.ChartAreas[0].CursorX.SelectionEnd;
            if (userSelectionEnd > doubleArrayRecorded.Length)
            {
                userSelectionEnd = doubleArrayRecorded.Length - 1;
            }
            int counter = 0;
            doubleArrayCopy = new double[userSelectionEnd - userSelectionStart];
            for (long i = userSelectionStart; i < userSelectionEnd; i++)
            {
                doubleArrayCopy[counter++] = doubleArrayRecorded[i];
            }
            Trace.WriteLine(doubleArrayCopy.Length);

        }

        private void Paste_Click(object sender, EventArgs e)
        {
            long pasteDest = (long)chartRecorded.ChartAreas[0].CursorX.SelectionStart;
            double[] temp = new double[doubleArrayRecorded.Length + doubleArrayCopy.Length];
            long tempCounter = 0;
            long copyCounter = 0;
            for (int i = 0; i < doubleArrayRecorded.Length; i++)
            {
                if(i == pasteDest)
                {
                    while (copyCounter < doubleArrayCopy.Length)
                    {
                        temp[tempCounter++] = doubleArrayCopy[copyCounter++];
                    }
                }
                temp[tempCounter++] = doubleArrayRecorded[i];
            }
            doubleArrayRecorded = temp;
            byteArrayRecorded = GetBytesAlt(doubleArrayRecorded);
            //waveHeader.subchunk2Size = byteArrayImport.Length;

            fixed (byte* byteP = byteArrayRecorded)
            {
                RclipBuffer(byteP, byteArrayRecorded.Length);
            }

            displayRecorded();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            ChartArea ca = chartRecorded.ChartAreas[0];
            long start = (long)ca.CursorX.SelectionStart;
            long end = (long)ca.CursorX.SelectionEnd;
            if (end > doubleArrayRecorded.Length)
            {
                end = doubleArrayRecorded.Length - 1;
            }
            double[] temp = new double[doubleArrayRecorded.Length - (end - start)];
            int tempCounter = 0;
            for (int i = 0; i < doubleArrayRecorded.Length; i++)
            {
                while (i >= start && i < end) { i++; }
                temp[tempCounter++] = doubleArrayRecorded[i];
            }
            doubleArrayRecorded = temp;
            byteArrayRecorded = GetBytesAlt(doubleArrayRecorded);
            //waveHeader.subchunk2Size = byteArrayImport.Length;

            fixed (byte* byteP = byteArrayRecorded)
            {
                RclipBuffer(byteP, byteArrayRecorded.Length);
            }

            displayRecorded();
            ca.CursorX.SetSelectionPosition(0, 0);
        }
        private void play_click(object sender, EventArgs e)
        {
            if (recordedRadio.Checked && importedRadio.Checked)
            {
                unsafe
                {
                    play();
                    Rplay();
                }
            } else if (importedRadio.Checked)
            {
                unsafe
                {
                    play();
                }
            } else if (recordedRadio.Checked)
            {
                unsafe
                {
                    Rplay();
                }
            }
            
        }

        private void recordBtn_Click(object sender, EventArgs e)
        {
            endBtn.Enabled = true;
            recordBtn.Enabled = false;
            Rrecord();
        }

        private void endBtn_Click(object sender, EventArgs e)
        {
            playBtn.Enabled = true;
            pauseBtn.Enabled = true;
            recordBtn.Enabled = true;
            Rend();
            loadAudioRecorded();
        }


        public void loadAudioRecorded()
        {
            IntPtr bufferP = RgetBuffer();
            uint bufferLen = RgetBufferLen();
            if (byteArrayRecorded != null)
            {
                Array.Clear(byteArrayRecorded, 0, byteArrayRecorded.Length);
                Array.Clear(doubleArrayRecorded, 0, doubleArrayRecorded.Length);
            }
            byteArrayRecorded = new byte[bufferLen];

            unsafe
            {
                byte* bPointer = (byte*)bufferP;
                for (int i = 0; i < bufferLen; i++)
                {
                    byteArrayRecorded[i] = bPointer[i];
                }
            }

            short[] sdata = new short[(int)Math.Ceiling((double)byteArrayRecorded.Length / 2)];
            Buffer.BlockCopy(byteArrayRecorded, 0, sdata, 0, byteArrayRecorded.Length);
            doubleArrayRecorded = sdata.Select(x => (double)x).ToArray();

            displayRecorded();
        }

        private void recorded_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void dft_click(object sender, EventArgs e)
        {
            if (chartRecorded.ChartAreas[0].CursorX.SelectionEnd == chartRecorded.ChartAreas[0].CursorX.SelectionStart ||  double.IsNaN(chartRecorded.ChartAreas[0].CursorX.SelectionEnd))
            {
                MessageBox.Show("Select values first");
                return;
            }
            Complex[] dft = DFT();
            Form2 dftForm = new Form2(dft, dft.Length, RgetSampleRate());

            dftForm.displayDFT();
            
            dftForm.Show();
        }
    }
}
