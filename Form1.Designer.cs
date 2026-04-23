namespace CourseWork
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            nudSize = new NumericUpDown();
            dgvInput = new DataGridView();
            dgvOutput = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)nudSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOutput).BeginInit();
            SuspendLayout();
            // 
            // nudSize
            // 
            nudSize.Location = new Point(324, 97);
            nudSize.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            nudSize.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            nudSize.Name = "nudSize";
            nudSize.Size = new Size(150, 27);
            nudSize.TabIndex = 0;
            nudSize.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // dgvInput
            // 
            dgvInput.AllowUserToAddRows = false;
            dgvInput.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInput.Location = new Point(12, 45);
            dgvInput.Name = "dgvInput";
            dgvInput.RowHeadersWidth = 51;
            dgvInput.Size = new Size(300, 188);
            dgvInput.TabIndex = 1;
            dgvInput.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dgvOutput
            // 
            dgvOutput.AllowUserToAddRows = false;
            dgvOutput.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOutput.Location = new Point(488, 45);
            dgvOutput.Name = "dgvOutput";
            dgvOutput.RowHeadersWidth = 51;
            dgvOutput.Size = new Size(300, 188);
            dgvOutput.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(110, 272);
            button1.Name = "button1";
            button1.Size = new Size(136, 50);
            button1.TabIndex = 3;
            button1.Text = "Згенерувати";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(252, 272);
            button2.Name = "button2";
            button2.Size = new Size(136, 50);
            button2.TabIndex = 4;
            button2.Text = "Метод Гауса";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(398, 272);
            button3.Name = "button3";
            button3.Size = new Size(136, 50);
            button3.TabIndex = 5;
            button3.Text = "Метод розбиття на клітки";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(540, 272);
            button4.Name = "button4";
            button4.Size = new Size(136, 50);
            button4.TabIndex = 6;
            button4.Text = "Зберегти в файл";
            button4.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 373);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(776, 55);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(75, 22);
            label1.Name = "label1";
            label1.Size = new Size(171, 20);
            label1.TabIndex = 8;
            label1.Text = "Початкова матриця (A)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(569, 22);
            label2.Name = "label2";
            label2.Size = new Size(183, 20);
            label2.TabIndex = 9;
            label2.Text = "Обернена матриця (A⁻¹ )";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(313, 74);
            label3.Name = "label3";
            label3.Size = new Size(173, 20);
            label3.TabIndex = 10;
            label3.Text = "Розмірність матриці (n)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(340, 350);
            label4.Name = "label4";
            label4.Size = new Size(146, 20);
            label4.TabIndex = 11;
            label4.Text = "Журнал результатів";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dgvOutput);
            Controls.Add(dgvInput);
            Controls.Add(nudSize);
            Name = "Form1";
            Text = "Обернення матриць - Мінаєв А.І.";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)nudSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOutput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown nudSize;
        private DataGridView dgvInput;
        private DataGridView dgvOutput;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private RichTextBox richTextBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
