#pragma warning disable
using System;

namespace CourseWork.Solvers
{
    public class BlockInverter : IMatrixInverter
    {
        public double[,] Invert(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            if (n % 2 != 0)
                throw new Exception("Метод розбиття на клітки вимагає парного розміру матриці.");

            int m = n / 2;

            // Розбиття матриці A на 4 блоки: A = | P  Q |
            //                                    | R  S |
            double[,] P = new double[m, m];
            double[,] Q = new double[m, m];
            double[,] R = new double[m, m];
            double[,] S = new double[m, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    P[i, j] = matrix[i, j];
                    Q[i, j] = matrix[i, j + m];
                    R[i, j] = matrix[i + m, j];
                    S[i, j] = matrix[i + m, j + m];
                }
            }

            GaussInverter gauss = new GaussInverter();

            // P⁻¹ через метод Гауса
            double[,] Pinv = gauss.Invert(P);

            // Доповнення Шура: T = S - R·P⁻¹·Q
            double[,] RPinv = Multiply(R, Pinv);
            double[,] RPinvQ = Multiply(RPinv, Q);
            double[,] T = Subtract(S, RPinvQ);

            // T⁻¹ через метод Гауса
            double[,] Tinv = gauss.Invert(T);

            // Формули блоків оберненої матриці:
            // C11 = P⁻¹ + P⁻¹·Q·T⁻¹·R·P⁻¹
            double[,] PinvQ = Multiply(Pinv, Q);
            double[,] PinvQTinv = Multiply(PinvQ, Tinv);
            double[,] PinvQTinvRPinv = Multiply(PinvQTinv, RPinv);
            double[,] C11 = Add(Pinv, PinvQTinvRPinv);

            // C12 = -P⁻¹·Q·T⁻¹
            double[,] C12 = Negate(PinvQTinv);

            // C21 = -T⁻¹·R·P⁻¹
            double[,] C21 = Negate(Multiply(Tinv, RPinv));

            // C22 = T⁻¹
            double[,] C22 = Tinv;

            // Збираємо результат з блоків
            double[,] result = new double[n, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = C11[i, j];
                    result[i, j + m] = C12[i, j];
                    result[i + m, j] = C21[i, j];
                    result[i + m, j + m] = C22[i, j];
                }
            }

            return result;
        }

        private double[,] Multiply(double[,] A, double[,] B)
        {
            int rows = A.GetLength(0);
            int cols = B.GetLength(1);
            int inner = A.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    for (int k = 0; k < inner; k++)
                        result[i, j] += A[i, k] * B[k, j];

            return result;
        }

        private double[,] Add(double[,] A, double[,] B)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = A[i, j] + B[i, j];

            return result;
        }

        private double[,] Subtract(double[,] A, double[,] B)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = A[i, j] - B[i, j];

            return result;
        }

        private double[,] Negate(double[,] A)
        {
            int rows = A.GetLength(0);
            int cols = A.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = -A[i, j];

            return result;
        }
    }
}