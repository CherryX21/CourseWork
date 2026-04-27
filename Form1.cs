#pragma warning disable
using System;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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
            {
                for (int j = 0; j < n; j++)
                {
                    int randomNumber = rnd.Next(-10, 11);
                    dgvInput.Rows[i].Cells[j].Value = randomNumber;
                }
            }
        }

        private void buttonBlock_Click(object sender, EventArgs e)
        {
            //todo: Реалізувати алгоритм інверсії методом розбиття на клітки
        }

        private void btnGauss_Click(object sender, EventArgs e)
        {
            try
            {
                int n = (int)nudSize.Value;
                double[,] inputMatrix = new double[n, n];

                // Зчитування та парсинг даних з графічного інтерфейсу у двовимірний масив
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        var cellValue = dgvInput.Rows[i].Cells[j].Value;
                        double val = Convert.ToDouble(cellValue);

                        //! Валідація: верхня межа (запобігання переповненню)
                        if (Math.Abs(val) > 1000000)
                        {
                            throw new Exception($"Значення {val} у клітинці [{i + 1}, {j + 1}] перевищує ліміт.\nДіапазон вводу: від -1 000 000 до 1 000 000.");
                        }

                        //! Валідація: нижня межа / машинний нуль (захист від втрати значущості)
                        if (val != 0 && Math.Abs(val) < 0.0001)
                        {
                            throw new Exception($"Значення {val} у клітинці [{i + 1}, {j + 1}] занадто мале.\nМінімально допустиме значення (окрім нуля) становить 0.0001.");
                        }

                        //x Повторне присвоєння видалено для оптимізації
                        inputMatrix[i, j] = val;
                    }
                }

                // Виклик обчислювального ядра
                CourseWork.Solvers.GaussInverter solver = new CourseWork.Solvers.GaussInverter();
                double[,] resultMatrix = solver.Invert(inputMatrix);

                // Форматування та вивід результатів з точністю до 3 десяткових знаків
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        dgvOutput.Rows[i].Cells[j].Value = Math.Round(resultMatrix[i, j], 3);
                    }
                }

                rtbLog.Text = "Метод Гауса успішно виконано!\n";
            }
            catch (Exception ex)
            {
                //! Обробка та виведення помилок валідації або математичних винятків
                MessageBox.Show("Помилка обчислень: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //! Блокування експорту порожніх або неініціалізованих DataGridView
                if (dgvOutput.Rows.Count == 0 || dgvOutput.Rows[0].Cells[0].Value == null)
                {
                    MessageBox.Show("Немає даних для збереження! Спочатку обчисліть матрицю.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Налаштування стандартного діалогового вікна збереження файлу
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Текстовий файл (*.txt)|*.txt";
                sfd.FileName = "InverseMatrix.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string outputText = "Обернена матриця:\n\n";
                    int n = dgvOutput.ColumnCount;

                    // Серіалізація масиву в текст із використанням табуляції для вирівнювання колонок
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            outputText += dgvOutput.Rows[i].Cells[j].Value.ToString() + "\t";
                        }
                        outputText += "\n";
                    }

                    // Експорт сформованої строки на диск
                    System.IO.File.WriteAllText(sfd.FileName, outputText);

                    rtbLog.Text = $"Результат успішно збережено у файл:\n{sfd.FileName}\n";
                }
            }
            catch (Exception ex)
            {
                //! Перехоплення помилок доступу до файлової системи (наприклад, файл зайнятий іншим процесом)
                MessageBox.Show("Помилка при збереженні: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}