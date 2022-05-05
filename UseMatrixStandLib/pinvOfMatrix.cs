using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace pinvofmatrix
{
    class Program
    {
        static void pinvOfMatrix(double[,] a)
        {
            Matrix<double> A = DenseMatrix.OfArray(a);
            Console.WriteLine("原矩阵：");
            Console.WriteLine(A);
            Matrix<double> pinvA = A.PseudoInverse();
            Console.WriteLine("广义逆矩阵：");
            Console.WriteLine(pinvA);
        }
        static void Main(string[] args)
        {
            double[,] a1 = new double[5, 7] { { 1, 2, 3, 4, 5, 6, 7 }, { 8, 9, 10, 11, 12, 13, 14 }, { 15, 16, 17, 18, 19, 20, 21 }, { 22, 23, 24, 25, 26, 27, 28 }, { 29, 30, 31, 32, 33, 34, 35 } }; ;
            pinvOfMatrix(a1);
            double[,] a2 = new double[6, 4] { { 11, 12, 13, 14 }, { 15, 16, 17, 18 }, { 19, 20, 21, 22 }, { 23, 24, 25, 26 }, { 27, 28, 29, 30 }, { 31, 32, 33, 34 } };
            pinvOfMatrix(a2);
        }
    }
}
