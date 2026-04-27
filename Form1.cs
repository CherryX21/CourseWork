#pragma warning disable
namespace CourseWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            nudSize_ValueChanged(null, null); //для коректного відображення матриці при першому запуску
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

        }

        private void btnGauss_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Узнаем текущий размер матрицы
                int n = (int)nudSize.Value;
                double[,] inputMatrix = new double[n, n];

                // 2. Зчитуємо дані з лівої таблиці (dgvInput) у масив
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        var cellValue = dgvInput.Rows[i].Cells[j].Value;

                        // Конвертуємо значення
                        double val = Convert.ToDouble(cellValue);

                        // ЗАХИСТ ВІД ДУРНЯ: перевіряємо верхні ліміти (більше мільйона)
                        if (Math.Abs(val) > 1000000)
                        {
                            throw new Exception($"Число {val} у клітинці [{i + 1}, {j + 1}] занадто велике!\nБудь ласка, вводьте числа в діапазоні від -1 000 000 до 1 000 000.");
                        }

                        // ЗАХИСТ ВІД ДУРНЯ: перевіряємо мікро-числа (менше 0.0001, АЛЕ дозволяємо чистий нуль)
                        if (val != 0 && Math.Abs(val) < 0.0001)
                        {
                            throw new Exception($"Число {val} у клітинці [{i + 1}, {j + 1}] занадто мале!\nМінімально допустиме значення (окрім нуля) становить 0.0001.");
                        }

                        inputMatrix[i, j] = val;

                        inputMatrix[i, j] = val;
                    }
                }

                // 3. МАГИЯ: Вызываем твой класс для решения!
                // (Обязательно добавь using CourseWork.Solvers; в самом верху файла Form1.cs, если студия ругается)
                CourseWork.Solvers.GaussInverter solver = new CourseWork.Solvers.GaussInverter();
                double[,] resultMatrix = solver.Invert(inputMatrix);

                // 4. Выводим результат в правую таблицу (dgvOutput)
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        // Округляем до 3 знаков после запятой для красоты, как в твоем ТЗ
                        dgvOutput.Rows[i].Cells[j].Value = Math.Round(resultMatrix[i, j], 3);
                    }
                }

                // Пишем об успехе в нижнее текстовое поле (RichTextBox)
                // Замени rtbLog на имя твоего RichTextBox, если ты назвал его иначе
                rtbLog.Text = "Метод Гаусса успішно виконано!\n";
            }
            catch (Exception ex)
            {
                // Если что-то пошло не так (ввели буквы, пустые клетки и т.д.)
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
                // 1. Проверяем, есть ли вообще что сохранять (чтобы не сохранить пустоту)
                if (dgvOutput.Rows.Count == 0 || dgvOutput.Rows[0].Cells[0].Value == null)
                {
                    MessageBox.Show("Немає даних для збереження! Спочатку обчисліть матрицю.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Вызываем стандартное виндовое окно сохранения файла
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Текстовий файл (*.txt)|*.txt"; // Фильтр форматов
                sfd.FileName = "InverseMatrix.txt"; // Имя по умолчанию

                // 3. Если пользователь нажал "Сохранить", собираем текст
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string outputText = "Обернена матриця:\n\n";
                    int n = dgvOutput.ColumnCount;

                    // Пробегаемся по правой таблице и склеиваем числа в строку
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            // \t - это символ табуляции, чтобы колонки в файле были ровными
                            outputText += dgvOutput.Rows[i].Cells[j].Value.ToString() + "\t";
                        }
                        outputText += "\n"; // Переход на новую строку после каждого ряда
                    }

                    // 4. Записываем весь собранный текст в выбранный файл
                    System.IO.File.WriteAllText(sfd.FileName, outputText);

                    // Пишем в нижний лог, что всё прошло успешно
                    rtbLog.Text = $"Результат успішно збережено у файл:\n{sfd.FileName}\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
