using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form3 : Form
    {
        byte[] bArray;
        Complex[] cArray;
        public Form3(Complex[] input2, byte[] inputArray, int len)
        {
            InitializeComponent();
            bArray = inputArray;
            cArray = input2;
            //sampleRate = len;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }

        private void dftChart_Click(object sender, EventArgs e)
        {
        }

        // Display Windowing 
        public void displayWindowing()
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < bArray.Length; i++)
            {
                chart1.Series[0].Points.Add(bArray[i]);
            }
        }

        // Display Windowed DFT
        public void displayWDFT()
        {
            dftChart.Series[0].Points.Clear();
            for (int i = 0; i < cArray.Length; i++)
            {
                dftChart.Series[0].Points.Add(Math.Sqrt(Math.Pow(cArray[i].re, 2) + Math.Pow(cArray[i].im, 2)));
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
