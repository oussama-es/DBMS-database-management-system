using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsSGBD
{
    public partial class Form1 : Form
    {
        private Analyseur Analyseur;
        public Form1()
        {
            InitializeComponent();
            Analyseur = new Analyseur(richTextBoxError, richTextBoxQuery, dataGridView1, treeView1);
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            Analyseur.ExecuteQuery(richTextBoxQuery.Text);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void richTextBoxQuery_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBoxError_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
