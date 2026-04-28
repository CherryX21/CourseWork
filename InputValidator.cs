using System;

namespace CourseWork
{
    public static class InputValidator
    {
        // Цей метод приймає те, що ввели в клітинку, перевіряє і повертає чистий double
        public static double ValidateCell(object cellValue, int row, int col)
        {
            // 1. Перевірка на порожнечу
            if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                throw new Exception($"Порожнє значення у клітинці [{row + 1}, {col + 1}].");

            // 2. Перевірка на букви/символи
            if (!double.TryParse(cellValue.ToString(), out double val))
                throw new Exception($"Некоректний формат числа (букви/символи) у клітинці [{row + 1}, {col + 1}].");

            // 3. Перевірка на гігантські числа
            if (Math.Abs(val) > 1000000)
                throw new Exception($"Число {val} у клітинці [{row + 1}, {col + 1}] занадто велике!");

            // 4. Перевірка на мікро-числа
            if (val != 0 && Math.Abs(val) < 0.0001)
                throw new Exception($"Число {val} у клітинці [{row + 1}, {col + 1}] занадто мале!");

            return val;
        }
    }
}