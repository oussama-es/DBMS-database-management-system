
namespace WindowsFormsSGBD
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("DATABASES", 4, 4);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelTreeView = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelDataGridView = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelTextBox = new System.Windows.Forms.Panel();
            this.buttonRun = new System.Windows.Forms.Button();
            this.richTextBoxQuery = new System.Windows.Forms.RichTextBox();
            this.richTextBoxError = new System.Windows.Forms.RichTextBox();
            this.panelTreeView.SuspendLayout();
            this.panelDataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelTextBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTreeView
            // 
            this.panelTreeView.Controls.Add(this.treeView1);
            this.panelTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTreeView.Location = new System.Drawing.Point(0, 0);
            this.panelTreeView.Name = "panelTreeView";
            this.panelTreeView.Size = new System.Drawing.Size(200, 511);
            this.panelTreeView.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 4;
            treeNode1.Name = "Parent";
            treeNode1.SelectedImageIndex = 4;
            treeNode1.Text = "DATABASES";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(200, 511);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "circle.png");
            this.imageList1.Images.SetKeyName(1, "database.png");
            this.imageList1.Images.SetKeyName(2, "PrimaryKey.png");
            this.imageList1.Images.SetKeyName(3, "table.png");
            this.imageList1.Images.SetKeyName(4, "barsHorizontal.png");
            // 
            // panelDataGridView
            // 
            this.panelDataGridView.Controls.Add(this.dataGridView1);
            this.panelDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDataGridView.Location = new System.Drawing.Point(200, 331);
            this.panelDataGridView.Name = "panelDataGridView";
            this.panelDataGridView.Size = new System.Drawing.Size(734, 180);
            this.panelDataGridView.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(734, 180);
            this.dataGridView1.TabIndex = 0;
            // 
            // panelTextBox
            // 
            this.panelTextBox.Controls.Add(this.buttonRun);
            this.panelTextBox.Controls.Add(this.richTextBoxQuery);
            this.panelTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTextBox.Location = new System.Drawing.Point(200, 0);
            this.panelTextBox.Name = "panelTextBox";
            this.panelTextBox.Size = new System.Drawing.Size(734, 180);
            this.panelTextBox.TabIndex = 2;
            // 
            // buttonRun
            // 
            this.buttonRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonRun.Location = new System.Drawing.Point(0, 157);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(734, 23);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // richTextBoxQuery
            // 
            this.richTextBoxQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBoxQuery.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxQuery.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxQuery.Name = "richTextBoxQuery";
            this.richTextBoxQuery.Size = new System.Drawing.Size(734, 160);
            this.richTextBoxQuery.TabIndex = 0;
            this.richTextBoxQuery.Text = "";
            this.richTextBoxQuery.TextChanged += new System.EventHandler(this.richTextBoxQuery_TextChanged);
            // 
            // richTextBoxError
            // 
            this.richTextBoxError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxError.Location = new System.Drawing.Point(200, 180);
            this.richTextBoxError.Name = "richTextBoxError";
            this.richTextBoxError.ReadOnly = true;
            this.richTextBoxError.Size = new System.Drawing.Size(734, 151);
            this.richTextBoxError.TabIndex = 3;
            this.richTextBoxError.Text = "";
            this.richTextBoxError.TextChanged += new System.EventHandler(this.richTextBoxError_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 511);
            this.Controls.Add(this.richTextBoxError);
            this.Controls.Add(this.panelTextBox);
            this.Controls.Add(this.panelDataGridView);
            this.Controls.Add(this.panelTreeView);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SGBD";
            this.panelTreeView.ResumeLayout(false);
            this.panelDataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelTextBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTreeView;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panelDataGridView;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelTextBox;
        private System.Windows.Forms.RichTextBox richTextBoxQuery;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.RichTextBox richTextBoxError;
        private System.Windows.Forms.ImageList imageList1;
    }
}

