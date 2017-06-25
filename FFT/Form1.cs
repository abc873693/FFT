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
        int N = 512;
        int count = 0;
        Complex[] x, OUT;

        public Form1()
        {
            InitializeComponent();
        }

        public Complex W(int nk, int N)
        {
            //return new Complex(Math.Cos(-2.0 * Math.PI * nk / N), Math.Sin(-2.0 * Math.PI * nk / N));
            return new Complex(Math.Cos((2.0 * nk / N) * Math.PI), -Math.Sin((2.0 * nk / N) * Math.PI));
        }

        public Complex X(int r, int N)
        {
            Complex G = new Complex();
            Complex H = new Complex();
            for (int n = 0; n <= (N / 2 - 1); n++)
            {
                G = G + ((x[2 * n]) * W(r * n, N / 2));
                count++;
            }
            for (int n = 0; n <= (N / 2 - 1); n++)
            {
                H = H + ((x[2 * n + 1]) * W(r * n, N / 2));
                count++;
            }
            H = H * W(r, N);

            return G + H;
        }

        public Complex DFT(int k, int N)
        {
            Complex sum = new Complex();
            for (int n = 0; n < N; n++)
            {
                sum = sum + ((x[n]) * new Complex(Math.Cos((2.0 * n * k / N) * Math.PI), -Math.Sin((2.0 * n * k / N) * Math.PI)));
                count++;
            }

            return sum;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            data_out.AppendText("DIT-DFT\n");
            N = 512;
            x = new Complex[N + 1];
            OUT = new Complex[N + 1];
            for (int i = 0; i < N; i++)
            {
                x[i] = new Complex(i + 1);
            }
            sw.Reset();
            sw.Start();
            for (int i = 0; i < N; i++)
            {
                OUT[i] = X(i, N);
            }
            sw.Stop();
            data_out.AppendText(N + " = " + sw.Elapsed.TotalMilliseconds.ToString() + "ms\n");
            data_out.AppendText("count = " + count + "\n");
            count = 0;
            //for (int i = 0; i < N; i++)
            //{
            //    data_out.AppendText("X[ " + i + "] = " + OUT[i].ToString() + "\n");
            //}

            N = 1024;
            x = new Complex[N + 1];
            OUT = new Complex[N + 1];
            for (int i = 0; i < N; i++)
            {
                x[i] = new Complex(i + 1);
            }
            sw.Reset();
            sw.Start();
            for (int i = 0; i < N; i++)
            {
                OUT[i] = X(i, N);
            }
            sw.Stop();
            data_out.AppendText(N + " = " + sw.Elapsed.TotalMilliseconds.ToString() + "ms\n");
            data_out.AppendText("count = " + count + "\n");
            count = 0;
            data_out.AppendText("DFT\n");
            N = 512;
            x = new Complex[N + 1];
            OUT = new Complex[N + 1];
            for (int i = 0; i < N; i++)
            {
                x[i] = new Complex(i + 1);
            }
            sw.Reset();
            sw.Start();
            for (int i = 0; i < N; i++)
            {
                OUT[i] = DFT(i, N);
            }
            sw.Stop();
            data_out.AppendText(N + " = " + sw.Elapsed.TotalMilliseconds.ToString() + "ms\n");
            data_out.AppendText("count = " + count + "\n");
            count = 0;
            N = 1024;
            x = new Complex[N + 1];
            OUT = new Complex[N + 1];
            for (int i = 0; i < N; i++)
            {
                x[i] = new Complex(i + 1);
            }
            sw.Reset();
            sw.Start();
            for (int i = 0; i < N; i++)
            {
                OUT[i] = DFT(i, N);
            }
            sw.Stop();
            data_out.AppendText(N + " = " + sw.Elapsed.TotalMilliseconds.ToString() + "ms\n");
            data_out.AppendText("count = " + count + "\n");
            count = 0;
            //for (int i = 0; i < N; i++)
            //{
            //    data_out.AppendText("X[ " + i + "] = " + X(i, N).ToString() + "\n");
            //}
            //for (int i = 0; i < N; i++)
            //{
            //    data_out.AppendText("W " + i + " " + N + " = " + W(i, N).ToString() + "\n");
            //}

        }
    }
}
