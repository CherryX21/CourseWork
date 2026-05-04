#pragma warning disable
using CourseWork.Solvers;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace CourseWork
{
    // Головна форма застосунку.
    // Відповідає за введення матриці, запуск алгоритмів інверсії,
    // відображення результату та збереження у файл.
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Забороняємо ручне введення в таблицю результатів
            dgvOutput.ReadOnly = true;
            dgvOutput.AllowUserToAddRows = false;

            // Ініціалізація таблиць відповідно до початкового значення nudSize
            nudSize_ValueChanged(null, null);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        // Обробник зміни розміру матриці (NumericUpDown nudSize).
        // Перебудовує обидві таблиці під новий розмір n×n.
        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            int n = (int)nudSize.Value;

            dgvInput.ColumnCount = n;
            dgvInput.RowCount = n;

            dgvOutput.Columns.Clear();
            dgvOutput.ColumnCount = n;
            dgvOutput.RowCount = n;

            // Автоматичне вирівнювання ширини стовпців по доступному простору
            dgvInput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Обробник кнопки "Згенерувати".
        // Заповнює dgvInput псевдовипадковими цілими числами у діапазоні [-10, 10].
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int n = (int)nudSize.Value;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    dgvInput.Rows[i].Cells[j].Value = rnd.Next(-10, 11);
        }

        // Зчитує матрицю з dgvInput і повертає її як двовимірний масив.
        private double[,] ReadMatrix()
        {
            int n = (int)nudSize.Value;
            double[,] matrix = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var cellValue = dgvInput.Rows[i].Cells[j].Value;

                    //! Вся перевірка (букви, ліміти, порожнеча) делегована окремому класу-валідатору
                    matrix[i, j] = CourseWork.InputValidator.ValidateCell(cellValue, i, j);
                }
            }

            return matrix;
        }

        // Записує матрицю результату в dgvOutput.
        // Значення округлюються до 3 десяткових знаків для зручності відображення.
        private void WriteResult(double[,] result)
        {
            int n = result.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double roundedValue = Math.Round(result[i, j], 3);

                    if (roundedValue == 0.0 || roundedValue == -0.0)
                    {
                        roundedValue = 0.0;
                    }

                    dgvOutput.Rows[i].Cells[j].Value = roundedValue;
                }
            }
        }

        // Обчислює максимальну абсолютну похибку добутку A·A⁻¹ відносно одиничної матриці E.
        // Використовується для верифікації коректності результату.
        // Ідеальний результат: похибка = 0. На практиці допустима похибка < 1e-9.
        private double ComputeError(double[,] A, double[,] Ainv)
        {
            int n = A.GetLength(0);
            double maxError = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < n; k++)
                        sum += A[i, k] * Ainv[k, j];

                    // На діагоналі очікується 1, поза діагоналлю — 0
                    double expected = (i == j) ? 1.0 : 0.0;
                    maxError = Math.Max(maxError, Math.Abs(sum - expected));
                }
            }

            return maxError;
        }

        // Обробник кнопки "Метод Гауса".
        // Зчитує матрицю, запускає GaussInverter, виводить результат і лог.
        private void btnGauss_Click(object sender, EventArgs e)
        {
            try
            {
                double[,] inputMatrix = ReadMatrix();
                int n = inputMatrix.GetLength(0);

                // Вимірювання часу виконання алгоритму
                Profiler.OperationsCount = 0;
                Stopwatch sw = Stopwatch.StartNew();
                CourseWork.Solvers.GaussInverter solver = new CourseWork.Solvers.GaussInverter();
                double[,] result = solver.Invert(inputMatrix);
                sw.Stop();

                WriteResult(result);

                // Верифікація: перевірка що A·A⁻¹ близька до одиничної матриці
                double error = ComputeError(inputMatrix, result);

                StringBuilder log = new StringBuilder();
                log.AppendLine("Метод Гауса — виконано успішно");
                log.AppendLine($"Розмір матриці: {n}×{n}");
                log.AppendLine($"Кількість обчислених операцій: {Profiler.OperationsCount}");
                log.AppendLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F3} мс");
                log.AppendLine($"Похибка верифікації ||A·A⁻¹ - E||: {error:E3}");

                rtbLog.Text = log.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обробник кнопки "Метод розбиття на клітки".
        // Перед запуском перевіряє що розмір матриці парний — це обов'язкова умова методу.
        // Метод Гауса при цьому не обмежується і працює для будь-якого n.
        private void buttonBlock_Click(object sender, EventArgs e)
        {
            try
            {
                int n = (int)nudSize.Value;

                // Перевірка парності розміру — специфічна вимога блочного методу
                if (n % 2 != 0)
                {
                    MessageBox.Show(
                        $"Метод розбиття на клітки вимагає парного розміру матриці.\nПоточний розмір: {n}×{n}.\nВстановіть парне значення (2, 4, 6 або 8).",
                        "Непарний розмір",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                double[,] inputMatrix = ReadMatrix();

                // Вимірювання часу виконання алгоритму
                Profiler.OperationsCount = 0;
                Stopwatch sw = Stopwatch.StartNew();
                CourseWork.Solvers.BlockInverter solver = new CourseWork.Solvers.BlockInverter();
                double[,] result = solver.Invert(inputMatrix);
                sw.Stop();

                WriteResult(result);

                // Верифікація: перевірка що A·A⁻¹ близька до одиничної матриці
                double error = ComputeError(inputMatrix, result);

                StringBuilder log = new StringBuilder();
                log.AppendLine("Метод розбиття на клітки — виконано успішно");
                log.AppendLine($"Розмір матриці: {n}×{n}  |  Розмір блоку: {n / 2}×{n / 2}");
                log.AppendLine($"Кількість обчислених операцій: {Profiler.OperationsCount}");
                log.AppendLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F3} мс");
                log.AppendLine($"Похибка верифікації ||A·A⁻¹ - E||: {error:E3}");

                rtbLog.Text = log.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обробник кнопки "Зберегти в файл".
        // Зберігає вміст dgvOutput та журнал rtbLog у текстовий файл .txt.
        // Стовпці вирівнюються за допомогою PadLeft для читабельності.
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Перевірка що результат вже обчислений перед збереженням
                if (dgvOutput.Rows.Count == 0 || dgvOutput.Rows[0].Cells[0].Value == null)
                {
                    MessageBox.Show("Немає даних для збереження! Спочатку обчисліть матрицю.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Текстовий файл (*.txt)|*.txt";
                sfd.FileName = "InverseMatrix.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    int n = dgvOutput.ColumnCount;
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("Обернена матриця:");
                    sb.AppendLine();

                    // Серіалізація матриці у текст з вирівнюванням стовпців
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            // sb.Append(dgvOutput.Rows[i].Cells[j].Value.ToString().PadLeft(12));
                            string cellText = dgvOutput.Rows[i].Cells[j].Value?.ToString() ?? "";
                            sb.Append(cellText.PadLeft(12));
                        }

                        sb.AppendLine();
                    }

                    sb.AppendLine();

                    // Додавання журналу результатів до файлу
                    sb.AppendLine(rtbLog.Text);

                    System.IO.File.WriteAllText(sfd.FileName, sb.ToString());

                    rtbLog.AppendText($"Збережено: {sfd.FileName}\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}