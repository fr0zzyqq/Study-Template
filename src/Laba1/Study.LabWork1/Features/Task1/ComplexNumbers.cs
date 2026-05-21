#nullable disable
using System;

namespace Study.LabWork1.Features.Task1
{
    /// <summary>
    /// Класс комплексных чисел (Вариант 2)
    /// </summary>
    public class ComplexNumber
    {
        public double Real { get; }
        public double Imaginary { get; }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);
        public ComplexNumber Conjugate => new ComplexNumber(Real, -Imaginary);

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(
                a.Real * b.Real - a.Imaginary * b.Imaginary,
                a.Real * b.Imaginary + a.Imaginary * b.Real
            );

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
            if (denominator == 0)
                throw new DivideByZeroException("Деление на ноль");
            return new ComplexNumber(
                (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator,
                (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator
            );
        }

        public static double operator +(ComplexNumber a) => a.Magnitude;
        public static ComplexNumber operator -(ComplexNumber a) => a.Conjugate;

        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return Math.Abs(a.Real - b.Real) < 1e-10 && Math.Abs(a.Imaginary - b.Imaginary) < 1e-10;
        }

        public static bool operator !=(ComplexNumber a, ComplexNumber b) => !(a == b);

        public override bool Equals(object? obj) => obj is ComplexNumber other && this == other;
        public override int GetHashCode() => HashCode.Combine(Real, Imaginary);

        public override string ToString()
        {
            if (Math.Abs(Imaginary) < 1e-10) return $"{Real}";
            if (Math.Abs(Real) < 1e-10) return $"{Imaginary}i";
            string sign = Imaginary > 0 ? "+" : "-";
            return $"{Real} {sign} {Math.Abs(Imaginary)}i";
        }
    }
}