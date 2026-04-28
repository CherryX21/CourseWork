#pragma warning disable
using System;

namespace CourseWork.Solvers
{
    // Реалізація інверсії матриці методом розбиття на клітки (блочна інверсія).
    // Матриця A ділиться на 4 підматриці рівного розміру, після чого
    // обернена матриця обчислюється через доповнення Шура.
    // Обмеження: розмір матриці має бути парним (2, 4, 6, 8).
    public class BlockInverter : IMatrixInverter
    {
        // Обчислює обернену матрицю методом розбиття на клітки.
        // Параметр matrix: квадратна матриця парного розміру.
        // Повертає: обернену матрицю того самого розміру.
        // Викидає Exception якщо розмір непарний або підматриця вироджена.
        public double[,] Invert(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            if (n % 2 != 0)
                throw new Exception("Метод розбиття на клітки вимагає парного розміру матриці (2, 4, 6, 8).");

            // m — розмір кожного з чотирьох блоків
            int m = n / 2;

            // Оголошення чотирьох підматриць для розбиття A:
            // A = | P  Q |
            //     | R  S |
            double[,] P = new double[m, m];
            double[,] Q = new double[m, m];
            double[,] R = new double[m, m];
            double[,] S = new double[m, m];

            // Заповнення підматриць із початкової матриці.
            // P — верхній лівий квадрант  (рядки 0..m-1, стовпці 0..m-1)
            // Q — верхній правий квадрант (рядки 0..m-1, стовпці m..n-1)
            // R — нижній лівий квадрант   (рядки m..n-1, стовпці 0..m-1)
            // S — нижній правий квадрант  (рядки m..n-1, стовпці m..n-1)
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

            // Для інверсії підматриць використовується метод Гауса
            GaussInverter gauss = new GaussInverter();

            // Крок 1: обернення верхнього лівого блоку P
            double[,] Pinv = gauss.Invert(P);

            // Крок 2: обчислення доповнення Шура T = S - R·P⁻¹·Q.
            // Доповнення Шура виражає "вплив блоку P на решту матриці".
            // Якщо P невироджена, то T теж невироджена.
            double[,] RPinv = Multiply(R, Pinv);
            double[,] RPinvQ = Multiply(RPinv, Q);
            double[,] T = Subtract(S, RPinvQ);

            // Крок 3: обернення доповнення Шура
            double[,] Tinv = gauss.Invert(T);

            // Крок 4: обчислення чотирьох блоків оберненої матриці.
            // PinvQTinv використовується і в C11, і в C12 — рахується один раз.
            double[,] PinvQ = Multiply(Pinv, Q);
            double[,] PinvQTinv = Multiply(PinvQ, Tinv);
            double[,] PinvQTinvRPinv = Multiply(PinvQTinv, RPinv);

            // C11 = P⁻¹ + P⁻¹·Q·T⁻¹·R·P⁻¹  (верхній лівий блок результату)
            double[,] C11 = Add(Pinv, PinvQTinvRPinv);

            // C12 = -P⁻¹·Q·T⁻¹  (верхній правий блок результату)
            double[,] C12 = Negate(PinvQTinv);

            // C21 = -T⁻¹·R·P⁻¹  (нижній лівий блок результату)
            double[,] C21 = Negate(Multiply(Tinv, RPinv));

            // C22 = T⁻¹  (нижній правий блок результату)
            double[,] C22 = Tinv;

            // Збирання фінальної матриці з чотирьох блоків результату
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

        // Множення двох матриць A (rows×inner) і B (inner×cols).
        // Повертає матрицю розміром rows×cols. Складність O(n³).
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

        // Поелементне додавання двох матриць однакового розміру.
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

        // Поелементне віднімання матриці B від матриці A.
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

        // Множення всіх елементів матриці на -1 (зміна знаку).
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