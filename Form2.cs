using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Browsy
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           
          


        } 
        // saves the history items passes from the  Form1 class
        List<String> UpdatedHistory = new List<string>();

        private void Form2_Load(object sender, EventArgs e)
        {
            //Displays  History items in the list 
            ShowHistory();

        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       public void AddHistory(string Url)
        {
            
            // adding the history list. This method is called in Form 1 to pass the history items from that class list to this class
            UpdatedHistory.Add(Url);

        }

        // displays history in the checked list
        public void ShowHistory()
        {
            // adds item from updated history lit  to checked item list for the user to see
            foreach (string History in UpdatedHistory)
            {  
                checkedListBox1.Items.Add(History);            
            }
            //saves the list
            SaveHistory(UpdatedHistory);
        }

      
        // deleting the items ftom the list
        private void button2_Click(object sender, EventArgs e)
        {
            if(checkedListBox1.Items.Count >0)
            {
                deleteItem();
            
            }
            
        }

        // Displaying the html for the selected url in the history list.
        public void HisotryHtml()
        {
            Form1 geturl = new Form1();
            geturl.GetHtml(checkedListBox1.SelectedItem.ToString());
            geturl.Show();
   
        }

        // saving the lists to the file
        public void SaveHistory(List<String> History)
        {

            File.WriteAllText("History.dat", new JavaScriptSerializer().Serialize(History));

        }

     // deletuing checked history from the list
        private void deleteItem()
        {
            foreach (var item in checkedListBox1.CheckedItems.OfType<string>().ToList())
            {
                //removes selected history from check list
                checkedListBox1.Items.Remove(item);
                // removes the selected history from the list
                UpdatedHistory.Remove(item);
                //saves the new deleted version of the list to the file
                SaveHistory(UpdatedHistory);  
            }
        }      
         

        private void button3_Click(object sender, EventArgs e)
        {
          
            HisotryHtml();
        }
    }
}
