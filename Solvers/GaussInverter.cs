#pragma warning disable
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace CourseWork.Solvers
{
    // Реалізація інверсії матриці методом Гауса-Жордана.
    // Алгоритм будує розширену матрицю [A | E], де E — одинична,
    // і за допомогою елементарних перетворень рядків зводить її до вигляду [E | A⁻¹].
    // Використовується часткове вибирання головного елемента (partial pivoting)
    // для підвищення чисельної стійкості.
    public class GaussInverter : IMatrixInverter
    {
        // Обчислює обернену матрицю методом Гауса-Жордана.
        // Параметр matrix: квадратна матриця довільного розміру n×n.
        // Повертає: обернену матрицю розміром n×n.
        // Викидає Exception якщо матриця вироджена (det = 0).
        public double[,] Invert(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            // Розширена матриця [A | E] розміром n×2n.
            // Ліва частина — копія вхідної матриці A.
            // Права частина — одинична матриця E.
            double[,] aug = new double[n, 2 * n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    aug[i, j] = matrix[i, j];

                // Одиниця на діагоналі правої частини
                aug[i, i + n] = 1.0;
            }

            // Прямий хід: перетворення лівої частини на верхню трикутну матрицю
            // з одиницями на діагоналі (нормалізація рядків).
            for (int k = 0; k < n; k++)
            {
                // Часткове вибирання головного елемента: серед рядків k..n-1
                // знаходимо рядок з найбільшим за модулем елементом у стовпці k.
                // Це зменшує накопичення похибок округлення.
                int maxRow = k;
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Abs(aug[i, k]) > Math.Abs(aug[maxRow, k]))
                        maxRow = i;
                }

                // Якщо максимальний елемент близький до нуля — матриця вироджена
                if (Math.Abs(aug[maxRow, k]) < 1e-10)
                    throw new Exception("Матриця вироджена (детермінант = 0), оберненої не існує!");

                // Перестановка поточного рядка k з рядком maxRow
                if (maxRow != k)
                {
                    for (int j = 0; j < 2 * n; j++)
                    {
                        double temp = aug[k, j];
                        aug[k, j] = aug[maxRow, j];
                        aug[maxRow, j] = temp;
                    }
                }

                double pivot = aug[k, k];

                // Нормалізація рядка k: ділимо всі елементи на головний елемент,
                // щоб на діагоналі стояла одиниця
                for (int j = 0; j < 2 * n; j++)
                {
                    aug[k, j] /= pivot;
                    Profiler.OperationsCount++; //+1 операція (ділення)
                }

                // Обнулення всіх елементів нижче діагоналі у стовпці k
                for (int i = k + 1; i < n; i++)
                {
                    double factor = aug[i, k];

                    for (int j = 0; j < 2 * n; j++)
                    {
                        aug[i, j] -= factor * aug[k, j];
                        Profiler.OperationsCount += 2; //+2 операції (множення та віднімання)
                    }
                }
            }

            // Зворотний хід: обнулення всіх елементів вище діагоналі.
            // Після цього ліва частина стане одиничною матрицею E.
            for (int k = n - 1; k >= 0; k--)
            {
                for (int i = 0; i < k; i++)
                {
                    double factor = aug[i, k];

                    for (int j = 0; j < 2 * n; j++)
                    {
                        aug[i, j] -= factor * aug[k, j];
                        Profiler.OperationsCount += 2; //+2 операції (множення та віднімання)
                    }
                }
            }

            // Витягуємо результат — праву частину розширеної матриці,
            // яка після перетворень містить A⁻¹
            double[,] result = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    result[i, j] = aug[i, j + n];

            return result;
        }
    }
}