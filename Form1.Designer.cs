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
            buttonGenerate = new Button();
            buttonGauss = new Button();
            buttonBlock = new Button();
            buttonSave = new Button();
            rtbLog = new RichTextBox();
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
            nudSize.Location = new Point(284, 73);
            nudSize.Margin = new Padding(3, 2, 3, 2);
            nudSize.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudSize.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            nudSize.Name = "nudSize";
            nudSize.Size = new Size(131, 23);
            nudSize.TabIndex = 0;
            nudSize.Value = new decimal(new int[] { 2, 0, 0, 0 });
            nudSize.ValueChanged += nudSize_ValueChanged;
            // 
            // dgvInput
            // 
            dgvInput.AllowUserToAddRows = false;
            dgvInput.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInput.Location = new Point(10, 27);
            dgvInput.Margin = new Padding(3, 2, 3, 2);
            dgvInput.Name = "dgvInput";
            dgvInput.RowHeadersWidth = 51;
            dgvInput.Size = new Size(262, 141);
            dgvInput.TabIndex = 1;
            dgvInput.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dgvOutput
            // 
            dgvOutput.AllowUserToAddRows = false;
            dgvOutput.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOutput.Location = new Point(426, 27);
            dgvOutput.Margin = new Padding(3, 2, 3, 2);
            dgvOutput.Name = "dgvOutput";
            dgvOutput.RowHeadersWidth = 51;
            dgvOutput.Size = new Size(262, 141);
            dgvOutput.TabIndex = 2;
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(96, 173);
            buttonGenerate.Margin = new Padding(3, 2, 3, 2);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(119, 38);
            buttonGenerate.TabIndex = 3;
            buttonGenerate.Text = "Згенерувати";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // buttonGauss
            // 
            buttonGauss.Location = new Point(220, 173);
            buttonGauss.Margin = new Padding(3, 2, 3, 2);
            buttonGauss.Name = "buttonGauss";
            buttonGauss.Size = new Size(119, 38);
            buttonGauss.TabIndex = 4;
            buttonGauss.Text = "Метод Гауса";
            buttonGauss.UseVisualStyleBackColor = true;
            buttonGauss.Click += btnGauss_Click;
            // 
            // buttonBlock
            // 
            buttonBlock.Location = new Point(348, 173);
            buttonBlock.Margin = new Padding(3, 2, 3, 2);
            buttonBlock.Name = "buttonBlock";
            buttonBlock.Size = new Size(119, 38);
            buttonBlock.TabIndex = 5;
            buttonBlock.Text = "Метод розбиття на клітки";
            buttonBlock.UseVisualStyleBackColor = true;
            buttonBlock.Click += buttonBlock_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(472, 173);
            buttonSave.Margin = new Padding(3, 2, 3, 2);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(119, 38);
            buttonSave.TabIndex = 6;
            buttonSave.Text = "Зберегти в файл";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(10, 240);
            rtbLog.Margin = new Padding(3, 2, 3, 2);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(680, 94);
            rtbLog.TabIndex = 7;
            rtbLog.Text = "";
            rtbLog.TextChanged += richTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 8);
            label1.Name = "label1";
            label1.Size = new Size(135, 15);
            label1.TabIndex = 8;
            label1.Text = "Початкова матриця (A)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(489, 8);
            label2.Name = "label2";
            label2.Size = new Size(142, 15);
            label2.TabIndex = 9;
            label2.Text = "Обернена матриця (A⁻¹ )";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(274, 56);
            label3.Name = "label3";
            label3.Size = new Size(137, 15);
            label3.TabIndex = 10;
            label3.Text = "Розмірність матриці (n)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(298, 223);
            label4.Name = "label4";
            label4.Size = new Size(116, 15);
            label4.TabIndex = 11;
            label4.Text = "Журнал результатів";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(rtbLog);
            Controls.Add(buttonSave);
            Controls.Add(buttonBlock);
            Controls.Add(buttonGauss);
            Controls.Add(buttonGenerate);
            Controls.Add(dgvOutput);
            Controls.Add(dgvInput);
            Controls.Add(nudSize);
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
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
        private Button buttonGenerate;
        private Button buttonGauss;
        private Button buttonBlock;
        private Button buttonSave;
        private RichTextBox rtbLog;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
