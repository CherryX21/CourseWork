#pragma warning disable
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork.Solvers
{
    // Інтерфейс для класів, що реалізують інверсію квадратних матриць.
    // Дозволяє використовувати різні алгоритми (Гаусс, блочний метод тощо)
    // через єдиний контракт без прив'язки до конкретної реалізації.
    public interface IMatrixInverter
    {
        // Обчислює обернену матрицю для переданої квадратної матриці.
        // Параметр matrix: вхідна квадратна матриця розміром n×n.
        // Повертає: обернену матрицю розміром n×n.
        // Викидає Exception якщо матриця вироджена або не відповідає вимогам алгоритму.
        double[,] Invert(double[,] matrix);
    }
}