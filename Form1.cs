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

        private void btnGauss_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Узнаем текущий размер матрицы
                int n = (int)nudSize.Value;
                double[,] inputMatrix = new double[n, n];

                // 2. Считываем данные из левой таблицы (dgvInput) в массив
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        // Берем значение из ячейки и конвертируем его в число (double)
                        var cellValue = dgvInput.Rows[i].Cells[j].Value;
                        inputMatrix[i, j] = Convert.ToDouble(cellValue);
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
    }
}
