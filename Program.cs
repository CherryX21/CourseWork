// Файл: Program.cs
// Призначення: Точка входу в програму.
// Забезпечує ініціалізацію графічної оболонки та запуск головного вікна застосунку.
namespace CourseWork
{
    internal static class Program
    {
        // Точка входу застосунку.
        // STAThread — обов'язковий атрибут для WinForms,
        // оскільки UI-компоненти вимагають однопотокової моделі COM (Single-Threaded Apartment).
        [STAThread]
        static void Main()
        {
            // Налаштування DPI та стилю відображення відповідно до конфігурації застосунку
            ApplicationConfiguration.Initialize();

            // Запуск головної форми — програма працює доки Form1 відкрита
            Application.Run(new Form1());
        }
    }
}