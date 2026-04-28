#pragma warning disable
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Ініціалізація початкового розміру таблиць при запуску програми
            nudSize_ValueChanged(null, null);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            int n = (int)nudSize.Value;

            dgvInput.ColumnCount = n;
            dgvInput.RowCount = n;
            dgvOutput.ColumnCount = n;
            dgvOutput.RowCount = n;

            dgvInput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int n = (int)nudSize.Value;

            // Генерація псевдовипадкових цілих чисел для початкової матриці
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    dgvInput.Rows[i].Cells[j].Value = rnd.Next(-10, 11);
        }

        // Зчитує матрицю з dgvInput, повертає масив або кидає Exception з описом проблеми
        private double[,] ReadMatrix()
        {
            int n = (int)nudSize.Value;
            double[,] matrix = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var cellValue = dgvInput.Rows[i].Cells[j].Value;

                    if (cellValue == null || cellValue.ToString().Trim() == "")
                        throw new Exception($"Клітинка [{i + 1}, {j + 1}] порожня. Заповніть усі клітинки або використайте генерацію.");

                    if (!double.TryParse(cellValue.ToString(), out double val))
                        throw new Exception($"Клітинка [{i + 1}, {j + 1}] містить некоректне значення \"{cellValue}\". Допустимі лише числа.");

                    if (Math.Abs(val) > 1_000_000)
                        throw new Exception($"Значення {val} у клітинці [{i + 1}, {j + 1}] перевищує ліміт ±1 000 000.");

                    if (val != 0 && Math.Abs(val) < 0.0001)
                        throw new Exception($"Значення {val} у клітинці [{i + 1}, {j + 1}] занадто мале.\nМінімально допустиме ненульове значення: 0.0001.");

                    matrix[i, j] = val;
                }
            }

            return matrix;
        }

        // Виводить матрицю результату в dgvOutput
        private void WriteResult(double[,] result)
        {
            int n = result.GetLength(0);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    dgvOutput.Rows[i].Cells[j].Value = Math.Round(result[i, j], 3);
        }

        // Обчислює норму відхилення A·A⁻¹ від одиничної матриці (перевірка точності)
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

                    double expected = (i == j) ? 1.0 : 0.0;
                    maxError = Math.Max(maxError, Math.Abs(sum - expected));
                }
            }

            return maxError;
        }

        private void btnGauss_Click(object sender, EventArgs e)
        {
            try
            {
                double[,] inputMatrix = ReadMatrix();
                int n = inputMatrix.GetLength(0);

                Stopwatch sw = Stopwatch.StartNew();
                CourseWork.Solvers.GaussInverter solver = new CourseWork.Solvers.GaussInverter();
                double[,] result = solver.Invert(inputMatrix);
                sw.Stop();

                WriteResult(result);

                double error = ComputeError(inputMatrix, result);

                StringBuilder log = new StringBuilder();
                log.AppendLine("Метод Гауса — виконано успішно");
                log.AppendLine($"Розмір матриці: {n}×{n}");
                log.AppendLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F3} мс");
                log.AppendLine($"Похибка верифікації ||A·A⁻¹ - E||: {error:E3}");

                rtbLog.Text = log.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBlock_Click(object sender, EventArgs e)
        {
            try
            {
                int n = (int)nudSize.Value;

                // Метод розбиття на клітки вимагає парного розміру
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

                Stopwatch sw = Stopwatch.StartNew();
                CourseWork.Solvers.BlockInverter solver = new CourseWork.Solvers.BlockInverter();
                double[,] result = solver.Invert(inputMatrix);
                sw.Stop();

                WriteResult(result);

                double error = ComputeError(inputMatrix, result);

                StringBuilder log = new StringBuilder();
                log.AppendLine("Метод розбиття на клітки — виконано успішно");
                log.AppendLine($"Розмір матриці: {n}×{n}  |  Розмір блоку: {n / 2}×{n / 2}");
                log.AppendLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F3} мс");
                log.AppendLine($"Похибка верифікації ||A·A⁻¹ - E||: {error:E3}");

                rtbLog.Text = log.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
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

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                            sb.Append(dgvOutput.Rows[i].Cells[j].Value.ToString().PadLeft(12));

                        sb.AppendLine();
                    }

                    sb.AppendLine();
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