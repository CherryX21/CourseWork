#pragma warning disable
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseWork.Solvers;

namespace CourseWork.Tests
{
    public static class MatrixAssert
    {
        public const double Tolerance = 1e-9;

        public static void IsInverse(double[,] A, double[,] Ainv)
        {
            int n = A.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < n; k++)
                        sum += A[i, k] * Ainv[k, j];

                    double expected = (i == j) ? 1.0 : 0.0;
                    Assert.AreEqual(expected, sum, Tolerance);
                }
            }
        }
    }

    [TestClass]
    public class GaussTests
    {
        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_2x2_Analytic()
        {
            var solver = new GaussInverter();
            double[,] A = { { 4, 7 }, { 2, 6 } };
            double[,] result = solver.Invert(A);

            Assert.AreEqual(0.6, result[0, 0], MatrixAssert.Tolerance);
            Assert.AreEqual(-0.7, result[0, 1], MatrixAssert.Tolerance);
            Assert.AreEqual(-0.2, result[1, 0], MatrixAssert.Tolerance);
            Assert.AreEqual(0.4, result[1, 1], MatrixAssert.Tolerance);
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_Identity()
        {
            var solver = new GaussInverter();
            double[,] A = { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_3x3_Standard()
        {
            var solver = new GaussInverter();
            double[,] A = { { 1, 2, 3 }, { 0, 1, 4 }, { 5, 6, 0 } };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_4x4_WithNegatives()
        {
            var solver = new GaussInverter();
            double[,] A = {
                {  5, -2,  0,  1 },
                { -1,  4,  1, -1 },
                {  0,  1,  3, -1 },
                {  2, -1, -1,  6 }
            };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_8x8_MaxStandardSize()
        {
            var solver = new GaussInverter();
            double[,] A = {
                {  4,  3, -1,  2,  1,  0,  5, -2 },
                {  1,  7,  2, -3,  0,  4, -1,  3 },
                { -2,  1,  6,  0,  3, -2,  2,  1 },
                {  3, -4,  0,  8,  2,  1, -3,  0 },
                {  0,  2, -3,  1,  5,  3,  0, -1 },
                {  1,  0,  4, -2,  3,  7,  1,  2 },
                { -3,  1,  2,  0, -1,  2,  6,  3 },
                {  2,  3,  0,  1,  4, -1,  2,  8 }
            };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_10x10_Large()
        {
            var solver = new GaussInverter();
            double[,] A = new double[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    A[i, j] = (i == j) ? 5.0 : 1.0;
                }
            }
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_Singular_Throws()
        {
            var solver = new GaussInverter();
            double[,] A = { { 1, 2 }, { 2, 4 } };
            try { solver.Invert(A); Assert.Fail(); } catch (Exception) { }
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_AllZeroes_Throws()
        {
            var solver = new GaussInverter();
            double[,] A = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            try { solver.Invert(A); Assert.Fail(); } catch (Exception) { }
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_ZeroDiagonal_Pivots()
        {
            var solver = new GaussInverter();
            double[,] A = { { 0, 1, 2 }, { 1, 0, 3 }, { 4, 5, 0 } };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Gauss Method")]
        public void Matrix_Fractionals()
        {
            var solver = new GaussInverter();
            double[,] A = { { 1.5, 2.5 }, { 0.5, 1.5 } };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }
    }

    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        [TestCategory("Block Method")]
        public void Matrix_2x2_Standard()
        {
            var solver = new BlockInverter();
            double[,] A = { { 4, 7 }, { 2, 6 } };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Block Method")]
        public void Matrix_4x4_Standard()
        {
            var solver = new BlockInverter();
            double[,] A = {
                {  5, -2,  0,  1 },
                { -1,  4,  1, -1 },
                {  0,  1,  3, -1 },
                {  2, -1, -1,  6 }
            };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Block Method")]
        public void Matrix_6x6_Standard()
        {
            var solver = new BlockInverter();
            double[,] A = {
                {  5,  2, -1,  0,  3,  1 },
                {  1,  7,  2, -3,  0,  4 },
                { -2,  1,  6,  0,  3, -2 },
                {  3, -4,  0,  8,  2,  1 },
                {  0,  2, -3,  1,  5,  3 },
                {  1,  0,  4, -2,  3,  7 }
            };
            MatrixAssert.IsInverse(A, solver.Invert(A));
        }

        [TestMethod]
        [TestCategory("Block Method")]
        public void Matrix_OddSize_Throws()
        {
            var solver = new BlockInverter();
            double[,] A = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            try { solver.Invert(A); Assert.Fail(); } catch (Exception) { }
        }

        [TestMethod]
        [TestCategory("Block Method")]
        public void Matrix_Singular_Throws()
        {
            var solver = new BlockInverter();
            double[,] A = { { 1, 2 }, { 2, 4 } };
            try { solver.Invert(A); Assert.Fail(); } catch (Exception) { }
        }
    }

    [TestClass]
    public class ComparisonTests
    {
        [TestMethod]
        [TestCategory("Comparison")]
        public void Compare_4x4_Algorithms()
        {
            double[,] A = {
                {  5, -2,  0,  1 },
                { -1,  4,  1, -1 },
                {  0,  1,  3, -1 },
                {  2, -1, -1,  6 }
            };

            var gauss = new GaussInverter().Invert(A);
            var block = new BlockInverter().Invert(A);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Assert.AreEqual(gauss[i, j], block[i, j], MatrixAssert.Tolerance);
        }

        [TestMethod]
        [TestCategory("Comparison")]
        public void Compare_6x6_Algorithms()
        {
            double[,] A = {
                {  5,  2, -1,  0,  3,  1 },
                {  1,  7,  2, -3,  0,  4 },
                { -2,  1,  6,  0,  3, -2 },
                {  3, -4,  0,  8,  2,  1 },
                {  0,  2, -3,  1,  5,  3 },
                {  1,  0,  4, -2,  3,  7 }
            };

            var gauss = new GaussInverter().Invert(A);
            var block = new BlockInverter().Invert(A);

            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    Assert.AreEqual(gauss[i, j], block[i, j], MatrixAssert.Tolerance);
        }
    }

    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        [TestCategory("Validation")]
        public void Validator_ValidNumber_ReturnsParsedDouble()
        {
            double result = InputValidator.ValidateCell("42", 0, 0);
            Assert.AreEqual(42.0, result);
        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Validator_ExactZero_IsValid()
        {
            double result = InputValidator.ValidateCell("0", 0, 0);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Validator_Letters_ThrowsException()
        {
            try
            {
                InputValidator.ValidateCell("абракадабра", 0, 0);
                Assert.Fail("Очікувалась помилка на букви, але код пропустив їх!");
            }
            catch (Exception) { }
        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Validator_EmptyCell_ThrowsException()
        {
            try
            {
                InputValidator.ValidateCell("", 0, 0);
                Assert.Fail("Очікувалась помилка на порожню клітинку!");
            }
            catch (Exception) { }
        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Validator_HugeNumber_ThrowsException()
        {
            try
            {
                InputValidator.ValidateCell("5000000", 0, 0);
                Assert.Fail("Очікувалась помилка на гігантське число!");
            }
            catch (Exception) { }
        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Validator_TinyNumber_ThrowsException()
        {
            try
            {
                InputValidator.ValidateCell("0,000001", 0, 0);
                Assert.Fail("Очікувалась помилка на мікро-число!");
            }
            catch (Exception) { }
        }
    }
}