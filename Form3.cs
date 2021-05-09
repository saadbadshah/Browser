using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Browsy
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        //homepage list
        List<String> HomePage = new List<string>();
       

        private void Form3_Load(object sender, EventArgs e)
        {
            //load homepage if its set
            LoadHomepage();
        }
        public void LoadHomepage()
        {
            // checks if the homepage.dat file exists 
            if (File.Exists("HomePage.dat"))

            {  // adds the retrived Homepage data from file into the retrivedHomePage list
                List<String> RetriievedHomePage = GetHomePage();
                if (RetriievedHomePage == null)
                {
                    //Do nothing
                }
                else
                {    // Displays the item in the textbox
                    foreach (string Homepage in RetriievedHomePage)
                    {
                        
                        textBox1.Text = Homepage;
                    }
                }
            }

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // sets the homepage
        private void button1_Click(object sender, EventArgs e)
        {

            Boolean HomSet = true;

            if (HomSet == true && textBox1.TextLength>0)
            {
                //adds homepage to the Homepage list
                HomePage.Add(textBox1.Text);
                //saves the hompage list to the file
                File.WriteAllText("HomePage.dat", new JavaScriptSerializer().Serialize(HomePage));
                this.Close();
            }
           
        }
        // writes  empty string to the file if user deletes the homepage
        private void deleteItem()
        {
            File.WriteAllText("HomePage.dat"," ");

        }
        // removes homepage
        private void button2_Click(object sender, EventArgs e)
        {
            //resets the textbox to empty
            textBox1.ResetText();
            deleteItem();
            this.Close();
        }

        private static List<String> GetHomePage()
        {

            return new JavaScriptSerializer().Deserialize<List<String>>(File.ReadAllText("HomePage.dat"));
        }
    }

  
}
