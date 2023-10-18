namespace WinSGBD
{
    public partial class Form1 : Form
    {
        private Analyseur Analyseur;
        public Form1()
        {
            InitializeComponent();
            Analyseur = new Analyseur(richTextBoxError, richTextBoxQuery, dataGridView1, treeView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Analyseur.ExecuteQuery(richTextBoxQuery.Text);
        }

    }
}