using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace Browsy
{
    [Serializable]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        //https://www.google.co.uk/

        string Page;
        Boolean searchWasClicked = false;
        List<String> History = new List<string>();
        
        

        List<String> Favorites = new List<string>();
        //  Form2 Currenthistory = new Form2();
        // Form2 history = new Form2();

        public async void GetHtml(string Url)
        {



            //Fix the exception when user enters a url without htto or .com stuff

            //Make http client
            HttpClient Connect = new HttpClient();


            try
            {
                //Http reponse containing all the data from webserver waiting for the connection to establish
                HttpResponseMessage Html = await Connect.GetAsync(Url);

                //Get status code and displlay in the textbox
                textBox2.ReadOnly = true;
                textBox4.ReadOnly = true;
                HttpStatusCode code = Html.StatusCode;
                textBox2.Text = Html.StatusCode.ToString();
                textBox4.Text = " "+(int)code;
                //Http content variable gets all the data from Html response variable 
                HttpContent Data = Html.Content;
               
                    textBox3.Text = GetHtmlTitle(Url);
                
               
                //Read all data as a string and also use awaits to get all the data before moving forward
                Page = await Data.ReadAsStringAsync();

            }
            catch (HttpRequestException)
            {
                MessageBox.Show("The url entered does not exist ");
            }

            catch (InvalidOperationException)
            {
                // MessageBox.Show("Code 404 page not found");
                // richTextBox1.Text = "Code 404 page not found";
            }
            //Display html in the textbox
            richTextBox1.Text = Page;

            Connect.Dispose();
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // loading the homepage if its set

            LoadHomepage();

            //Deserialising history from file

            LoadHistory();
           
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        public void LoadHomepage()
        {

            // checks if the homepage file exists 

            if (File.Exists("HomePage.dat"))
            {
                List<String> RetriievedHomePage = GetHomePage();
                if (RetriievedHomePage == null)
                {
                    //Do nothing
                }
                else
                {
                    //For the homepage URL in the RetriievedHomePage list it calls GetHtml function which loads the Html for the url
                    foreach (string Homepage in RetriievedHomePage)
                    {
                        string homepage = Homepage;
                        GetHtml(homepage);
                    }
                }
            }

        }
        public void LoadHistory()
        {
            // checks if the History file exists 
            if (File.Exists("History.dat"))
            {
                // adds the retrived history data from file into the retrived history list
                List<String> RetriievedHistory = GetHistory();
                if (RetriievedHistory == null)
                {
                    //Do nothing 
                }
                else  
                    // Adds all the elements in Retrieved history from the previous sessions to the current sessions list called History
                    foreach (string PreviousHistory in RetriievedHistory)
                    {
                        History.Add(PreviousHistory);
                    }
            }
           
        }

        // serialising the History list to save it in the History.dat file
        public void SaveHistory(List<String> History)
        {
         
            File.WriteAllText("History.dat", new JavaScriptSerializer().Serialize(History));

        }

        //Deserialising the History.dat file 
        private static List<String> GetHistory()
        {

            //return File.ReadAllText("History.dat"));
            return new JavaScriptSerializer().Deserialize<List<String>>(File.ReadAllText("History.dat"));
        }

        //Getting all the data from the homepage file
        private static List<String> GetHomePage()
        {
            return new JavaScriptSerializer().Deserialize<List<String>>(File.ReadAllText("HomePage.dat"));
        }


        // getting the title for every html page that is loaded
        private static string GetHtmlTitle(string url)
        { 
            var title = " ";
           
            try
            {
               
                var webGet = new HtmlWeb();

                var document = webGet.Load(url);
          
               // getting the text of the title tag in the html page
                title = document?.DocumentNode?.SelectSingleNode("html/head/title")?.InnerText;
                
               
                

            }catch(NullReferenceException)
            {
                
            }
            return title;
        }

        // Code for the Go button on the form. 
        private void button1_Click(object sender, EventArgs e)
        {
            searchWasClicked = true;
            //url stored in the string variable
            string Url = textBox1.Text;
           
            //Adding url to the History List
            History.Add(Url);
            //Saving the current sessions History
            SaveHistory(History);
            // getting the html for the typed url
            GetHtml(Url);
           
           
        }

        private void SetHomePage()
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowHistory();

        }
        
           
        // method to display history form
        private void ShowHistory()
        {
            // history form
            Form2 history = new Form2();


              foreach (string url in History)
                {
                // adding every item in thhe History list to the other Clas form2's List
                    history.AddHistory(url);
                    
                }       
            history.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Method for reloading the page
            // only reloads if the url was searched before
            if (searchWasClicked == true)
            {
                string url = textBox1.Text;
                GetHtml(url);
            } 
            
        }

        private void setHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 SetHomePage = new Form3();
            SetHomePage.Show();
            
        }

        // adding favourites to the list
        private void button4_Click(object sender, EventArgs e)
        {
            string favorite = textBox1.Text;

            if (Favorites.Contains(favorite))
            {
                //do nothing
            }else
            {
                if(favorite.Length>0)
                {
                   // Adding New favorite to the favourite to the favourite list in this class
                    Favorites.Add(favorite);
                   // Console.WriteLine("This is favorite "+ Favorites);
                    Form4 Favorite = new Form4();
                    foreach (string fav in Favorites)
                    {
                        // passing all the favourites saved in this class's favourites list to the favorite class's list
                        Favorite.AddFavoite(fav);

                    }
                  
                }
            
           
            }
          
        }
        // showing favorite list
        private void button5_Click(object sender, EventArgs e)
        {
            Form4 Favorite = new Form4();
            Favorite.Show();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
