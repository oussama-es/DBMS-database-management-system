namespace WinSGBD
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
            panel1 = new Panel();
            panel4 = new Panel();
            richTextBoxQuery = new RichTextBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            panel2 = new Panel();
            button4 = new Button();
            textBox2 = new TextBox();
            treeView1 = new TreeView();
            panel3 = new Panel();
            panel5 = new Panel();
            richTextBoxError = new RichTextBox();
            dataGridView1 = new DataGridView();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(920, 19);
            panel1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(richTextBoxQuery);
            panel4.Controls.Add(button3);
            panel4.Controls.Add(button2);
            panel4.Controls.Add(button1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 19);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(920, 74);
            panel4.TabIndex = 0;
            // 
            // richTextBoxQuery
            // 
            richTextBoxQuery.Location = new Point(55, 0);
            richTextBoxQuery.Name = "richTextBoxQuery";
            richTextBoxQuery.Size = new Size(744, 70);
            richTextBoxQuery.TabIndex = 4;
            richTextBoxQuery.Text = "";
            // 
            // button3
            // 
            button3.Location = new Point(20, 41);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(29, 29);
            button3.TabIndex = 3;
            button3.Text = "↻";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(20, 4);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(29, 29);
            button2.TabIndex = 2;
            button2.Text = "◀";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(812, 4);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(96, 66);
            button1.TabIndex = 1;
            button1.Text = "Execute";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(button4);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(treeView1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(692, 93);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(228, 385);
            panel2.TabIndex = 1;
            // 
            // button4
            // 
            button4.Location = new Point(206, 3);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(20, 22);
            button4.TabIndex = 2;
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(3, 4);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(201, 23);
            textBox2.TabIndex = 1;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(0, 31);
            treeView1.Margin = new Padding(3, 2, 3, 2);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(224, 356);
            treeView1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel5);
            panel3.Controls.Add(dataGridView1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 93);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(692, 385);
            panel3.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.Controls.Add(richTextBoxError);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(692, 138);
            panel5.TabIndex = 1;
            // 
            // richTextBoxError
            // 
            richTextBoxError.Dock = DockStyle.Fill;
            richTextBoxError.Location = new Point(0, 0);
            richTextBoxError.Name = "richTextBoxError";
            richTextBoxError.Size = new Size(692, 138);
            richTextBoxError.TabIndex = 0;
            richTextBoxError.Text = "";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(692, 385);
            dataGridView1.TabIndex = 0;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 478);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel4);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel4;
        private Button button3;
        private Button button2;
        private Button button1;
        private Panel panel2;
        private Button button4;
        private TextBox textBox2;
        private TreeView treeView1;
        private Panel panel3;
        private DataGridView dataGridView1;
        private Panel panel5;
        private RichTextBox richTextBoxError;
        private RichTextBox richTextBoxQuery;
    }
}