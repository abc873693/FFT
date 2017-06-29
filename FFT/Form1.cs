using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FFT
{
    public partial class Form1 : Form
    {
        Stopwatch sw = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        /* Performs a Bit Reversal Algorithm on a postive integer 
         * for given number of bits
         * e.g. 011 with 3 bits is reversed to 110 */
        public static int BitReverse(int n, int bits)
        {
            int reversedN = n;
            int count = bits - 1;

            n >>= 1;
            while (n > 0)
            {
                reversedN = (reversedN << 1) | (n & 1);
                count--;
                n >>= 1;
            }

            return ((reversedN << count) & ((1 << bits) - 1));
        }

        public static Complex W(int nk, int N)
        {
            return new Complex(Math.Cos((2.0 * nk / N) * Math.PI), -Math.Sin((2.0 * nk / N) * Math.PI));
        }

        public static void DIT_FFT(Complex[] x)
        {
            int bits = (int)Math.Log(x.Length, 2);
            for (int j = 1; j < x.Length / 2; j++)
            {
                int swapPos = BitReverse(j, bits);
                var temp = x[j];
                x[j] = x[swapPos];
                x[swapPos] = temp;
            }

            for (int N = 2; N <= x.Length; N <<= 1)
            {
                for (int i = 0; i < x.Length; i += N)
                {
                    for (int k = 0; k < N / 2; k++)
                    {
                        int evenIndex = i + k;
                        int oddIndex = i + k + (N / 2);
                        var even = x[evenIndex];
                        var odd = x[oddIndex];

                        Complex exp = W(k,N) * odd;

                        x[evenIndex] = even + exp;
                        x[oddIndex] = even - exp;
                    }
                }
            }
        }

        public Complex DFT(Complex[] x,int k, int N)
        {
            Complex sum = new Complex();
            for (int n = 0; n < N; n++)
            {
                sum = sum + ((x[n]) * new Complex(Math.Cos((2.0 * n * k / N) * Math.PI), -Math.Sin((2.0 * n * k / N) * Math.PI)));
            }
            return sum;
        }

        public void Get_DFT_Data(int N)
        {
            Complex[] x = new Complex[N];
            Complex[] X = new Complex[N];
            for (int i = 0; i < N; i++)
            {
                x[i] = new Complex(i + 1);
            }
            sw.Reset();
            sw.Start();
            for (int i = 0; i < N; i++)
            {
                X[i] = DFT(x,i, N);
            }
            sw.Stop();
            data_out.AppendText("N = " +N + "\n Delay = " + sw.Elapsed.TotalMilliseconds.ToString() + "ms\n");
        }

        public void Get_DIT_DFT_Data(int N) {
            Complex[] x = new Complex[N];
            for (int i = 0; i < N; i++)
            {
                x[i] = new Complex(i + 1);
            }
            sw.Reset();
            sw.Start();
            DIT_FFT(x);
            sw.Stop();
            data_out.AppendText("N = "+ N + "\n Delay = " + sw.Elapsed.TotalMilliseconds.ToString() + "ms\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            data_out.AppendText("DIT-DFT\n");
            Get_DIT_DFT_Data(512);
            Get_DIT_DFT_Data(1024);

            data_out.AppendText("\nDFT\n");
            Get_DFT_Data(512);
            Get_DFT_Data(1024);
        }
    }
}
