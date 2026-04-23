#pragma warning disable
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace CourseWork.Solvers
{
    public class GaussInverter : IMatrixInverter
    {
        public double[,] Invert(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            // Створюємо розширену матрицю
            double[,] aug = new double[n, 2 * n];

            // Заповнюємо матрицю: зліва - початкова (A), справа - одинична (E)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    aug[i, j] = matrix[i, j];
                }
                aug[i, i + n] = 1.0;
            }

            // Прямий хід: утворення одиниць на діагоналі та нулів під нею
            for (int k = 0; k < n; k++)
            {
                double pivot = aug[k, k];

                // Нормалізація рядка (ділимо на головний елемент)
                for (int j = 0; j < 2 * n; j++)
                {
                    aug[k, j] = aug[k, j] / pivot;
                }

                // Обнулення елементів під діагоналлю
                for (int i = k + 1; i < n; i++)
                {
                    double factor = aug[i, k];

                    for (int j = 0; j < 2 * n; j++)
                    {
                        aug[i, j] = aug[i, j] - factor * aug[k, j];
                    }
                }
            }

            // Зворотний хід: утворення нулів над діагоналлю
            for (int k = n - 1; k >= 0; k--)
            {
                for (int i = 0; i < k; i++)
                {
                    double factor = aug[i, k];

                    for (int j = 0; j < 2 * n; j++)
                    {
                        aug[i, j] = aug[i, j] - factor * aug[k, j];
                    }
                }
            }

            // Витягуємо результат (праву частину розширеної матриці)
            double[,] result = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = aug[i, j + n];
                }
            }

            return result;
        }
    }
}