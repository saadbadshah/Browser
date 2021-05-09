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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            
        }
        //list for favourites
        List<String> Favorites = new List<string>();
       
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void AddFavoite(string Url)
        {
            // if favorites already haas the favourite then it doesnt add it
            if (Favorites.Contains(Url))
            {
                //do nothing
            }else
            {
                //adds favorite to the favourites list
                Favorites.Add(Url);
                //saves the list to the file
                SaveFavorites(Favorites);
                Console.WriteLine("This is favorite " + Favorites);
            }
           
            //listBox1.Items.Add(Url);
        }

        //edits the selected favourite from the slist
        public void EditFavoite(string Url)
        {
            if (Favorites.Contains(Url)==false)
            {
                Favorites.Add(Url);
                // saves the updates favourites list to the file after the edit is done
                SaveFavorites(Favorites);
            }
        
        }


        public void ShowFavoite()
        {
            foreach (string fav in Favorites)
            {
             //displays favourites in the check list from the Favourites list
                checkedListBox1.Items.Add(fav);
              
            }
            
        }

        //serialising or saving favourites list
        public void SaveFavorites(List<String> Favorites)
        {

            File.WriteAllText("Favorites.dat", new JavaScriptSerializer().Serialize(Favorites));

        }
        // retrieving the saved favourites list
        private static List<String> GetFavorites()
        {

            //return File.ReadAllText("History.dat"));
            return new JavaScriptSerializer().Deserialize<List<String>>(File.ReadAllText("Favorites.dat"));
        }

        // gets previous sessions favourites and adds it to the current sessions list to display all the favourites to the user
        public void CombineFavorites()
        {
            
            if (File.Exists("Favorites.dat"))
            {
                List<String> RetriievedFavorites = GetFavorites();
                if (RetriievedFavorites == null)
                {
                    //Do nothing 
                }
                else
                    foreach (string PreviousFavorites in RetriievedFavorites)
                    {
                        Favorites.Add(PreviousFavorites);
                    }
            }
        }

        // edits the selected favorite by opening form5 or the editing form sends infomation of the selcted item to the next form
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(checkedListBox1.SelectedItem ==null)
                {
                    //do nothing
                }else
                {
                    Form5 Edit = new Form5();
                    Edit.GetTextBox(checkedListBox1.SelectedItem.ToString());
                    this.Hide();
                    Edit.Show();

                }
               
            } catch (System.NullReferenceException)
            {
               
            }
            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // combine previous and this sessions favourites
            CombineFavorites();
            //displays favorites
            ShowFavoite();
            //saves all together to the file
            SaveFavorites(Favorites);
         
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //deletes the selected favourite
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBox1.CheckedItems.OfType<string>().ToList())
            {
                checkedListBox1.Items.Remove(item);
                Favorites.Remove(item);
                SaveFavorites(Favorites);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Method for reloading the page for the selected favourite
            Form1 LoadFavoriteUrl = new Form1();
            string url = checkedListBox1.SelectedItem.ToString();

             LoadFavoriteUrl.GetHtml(url);
            this.Close();
            LoadFavoriteUrl.Show();
        }
    }
}
