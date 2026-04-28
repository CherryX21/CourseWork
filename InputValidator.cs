#pragma warning disable
using System;
using System.Globalization;

namespace CourseWork
{
    public static class InputValidator
    {
        public static double ValidateCell(object cellValue, int row, int col)
        {
            // Перевірка на null та порожнечу за допомогою throw expression (C# 7.0+)
            string input = cellValue?.ToString()?.Trim()
                ?? throw new Exception($"Порожнє значення у клітинці [{row + 1}, {col + 1}].");

            if (string.IsNullOrEmpty(input))
                throw new Exception($"Порожнє значення у клітинці [{row + 1}, {col + 1}].");

            // Уніфікація десяткового розділювача
            string normalizedInput = input.Replace(',', '.');

            // Парсинг числового значення незалежно від регіональних налаштувань ОС
            if (!double.TryParse(normalizedInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedValue))
                throw new Exception($"Некоректний формат числа у клітинці [{row + 1}, {col + 1}].");

            // Валідація математичних обмежень через switch expression (C# 8.0+)
            return parsedValue switch
            {
                double v when Math.Abs(v) > 1_000_000 =>
                    throw new Exception($"Значення {v} у клітинці [{row + 1}, {col + 1}] перевищує верхній ліміт."),

                // Використовуємо double.Epsilon замість v != 0 для правильної роботи з плаваючою крапкою
                double v when Math.Abs(v) > double.Epsilon && Math.Abs(v) < 0.0001 =>
                    throw new Exception($"Значення {v} у клітинці [{row + 1}, {col + 1}] менше за допустимий нуль (0.0001)."),

                _ => parsedValue
            };
        }
    }
}