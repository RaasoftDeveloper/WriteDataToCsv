using System;
using System.Windows.Forms;
using WriteDataToCsv;

namespace JohnsonControlHelper
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            formEdit formEdit = new formEdit();
            formEdit.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formGenerate formGenerate = new formGenerate();
            formGenerate.Show();
        }
    }
}
