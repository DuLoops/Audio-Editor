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

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern int record();

        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr getBuffer();

        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern void setBuffer(IntPtr ptr);

        [DllImport("DLL.dll", CharSet = CharSet.Auto)]
        public static extern uint getBufferLen();

        WaveHeader waveHeader = new WaveHeader();
        byte[] byteArray;
        double[] doubleArray;

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


        }


        public void displayWave()
        {
            ChartArea CA = chart1.ChartAreas[0];  // quick reference

            CA.CursorX.IsUserSelectionEnabled = true;
            CA.AxisX.ScaleView.Zoomable = false;

            //CA.CursorX.AutoScroll = false;
            //CA.AxisX.Interval = 10;

            Console.WriteLine("whfs");

            for (int i = 0; i < doubleArray.Length; i++)
            {
                chart1.Series[0].Points.Add(doubleArray[i]);
            }
            CA.CursorX.Interval = 100;

            //selectionstat
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            record();

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
    }
}
