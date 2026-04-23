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
    }
}
