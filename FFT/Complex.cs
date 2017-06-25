using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFT
{
    public class Complex
    {
        public double real;
        public double imaginary;

        public Complex(double real = 0, double imaginary = 0)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary);
        }

        public static Complex operator +(double c1, Complex c2)
        {
            return new Complex(c1 + c2.real, c2.imaginary);
        }

        public static Complex operator -(double c1, Complex c2)
        {
            return new Complex(c1 - c2.real, -c2.imaginary);
        }

        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.real - c2.real, c1.imaginary - c2.imaginary);
        }

        public static Complex operator -(Complex c2)
        {
            return new Complex(-c2.real, -c2.imaginary);
        }

        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(c1.real * c2.real - c1.imaginary * c2.imaginary, c1.imaginary * c2.real + c1.real * c2.imaginary);
        }

        public override string ToString()
        {
            return (System.String.Format("{0:0.0} {1} {2:0.0}j", real, imaginary >= 0 ? "+" : "-", Math.Abs(imaginary)));
        }
    }
}
