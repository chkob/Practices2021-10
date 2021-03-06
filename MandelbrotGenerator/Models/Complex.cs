using System;

namespace MandelbrotGenerator.Models
{
    public class Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public Complex() { }

        public Complex(double r, double im)
        {
            Real = r;
            Imaginary = im;
        }

        public static Complex operator +(Complex b, Complex c)
        {
            return new Complex
            {
                Real = b.Real + c.Real,
                Imaginary = b.Imaginary + c.Imaginary
            };
        }

        public Complex Conjugate()
        {
            return new Complex
            {
                Real = Real,
                Imaginary = -1*Imaginary
            };
        }

        public double Abs()
        {
            return Math.Abs(Math.Sqrt(((Real*Real)+(Imaginary*Imaginary))));
        }

        public static Complex operator -(Complex b, Complex c)
        {
            return new Complex
            {
                Real = b.Real - c.Real,
                Imaginary = b.Imaginary - c.Imaginary
            };
        }

        public static Complex operator *(Complex b, Complex c)
        {
            return new Complex
            {
                Real = b.Real*c.Real - b.Imaginary*c.Imaginary,
                Imaginary = b.Real*c.Imaginary + b.Imaginary*c.Real
            };
        }

        public static Complex operator /(Complex b, Complex c)
        {
            var recipocal = b.Real * b.Real + b.Imaginary * b.Imaginary;
            return new Complex
            {
                Real = ((b.Real * c.Real) + (b.Imaginary * c.Imaginary)) / recipocal,
                Imaginary = ((b.Real * c.Imaginary) - (b.Imaginary * c.Real)) / recipocal
            };
        }
    }
}
