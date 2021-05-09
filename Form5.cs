using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Browsy
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public void GetTextBox(string Favorite)
        {
            textBox1.Text = Favorite;
        }

        //gets the selected favourtie in the form4 and displays its text in the textbox for editing, then saves it
        private void button1_Click(object sender, EventArgs e)
        {
            Form4 SaveFavorite = new Form4();
            SaveFavorite.EditFavoite(textBox1.Text);
            this.Hide();
            SaveFavorite.Show();
        }
    }
}
