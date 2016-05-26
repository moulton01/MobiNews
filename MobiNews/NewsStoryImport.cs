using System;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient; 
namespace MobiNews
{
    public partial class NewsStoryImport : Form
    { 
        public NewsStoryImport()
        {
            InitializeComponent();
        }
        private void GetNewsStories_Click(object sender, EventArgs e)
        {
            RunImport(); 
        }
        private void RunImport()
        {
            List<NewsStory> newStories = new List<NewsStory>(); 
            try
            {
                newStories.AddRange(NewsTodayImport());
                newStories.AddRange(TechMediaNewsImport());
                InsertStories(newStories);
                MessageBox.Show("Import Finished with no errors."); 
            }
            catch(Exception e)
            {
                throw new Exception(String.Format("The following error occured when importing new stories: {0} {1}", Environment.NewLine, e.Message));
            }
        }
        private List<NewsStory> NewsTodayImport()
        {
            List<NewsStory> stories = new List<NewsStory>();
            //XmlDocument doc = new XmlDocument();
            //doc.Load("http:////www.example.com//xml//");
            //XmlElement root = doc.DocumentElement;
            //XmlNodeList newStories = root.SelectNodes("/publishing/stories/story/");
            //foreach (XmlNode story in newStories)
            //{
            //    NewsStory storyToAdd = new NewsStory
            //    {
            //        SupplierStoryId = Convert.ToInt32(story["id"].InnerText),
            //        Title = story["topTitle"].InnerText,
            //        StoryText = story["body"].InnerText,
            //        ImagePath = story["imageloc"].InnerText
            //    };
            //    stories.Add(storyToAdd);
            //}
            return stories; 
        }
        private List<NewsStory> TechMediaNewsImport()
        {
            List<NewsStory> stories = new List<NewsStory>(); 
            var xmlFiles = Directory.GetFiles("c:\\news\\", "*.xml");
            foreach (var file in xmlFiles)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlNodeList newStories = doc.SelectNodes("/NewsStory");
                foreach (XmlNode story in newStories)
                {
                    NewsStory storyToAdd = new NewsStory
                    {
                        SupplierStoryId = Convert.ToInt32(story["id"].InnerText),
                        Title = story["topTitle"].InnerText,
                        StoryText = story["body"].InnerText, 
                        ImagePath = story["imageloc"].InnerText
                    };
                    stories.Add(storyToAdd); 
                }
            }
            return stories;  
        }
        private void InsertStories(List<NewsStory> _newStories)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=BENS-PC\SQLEXPRESS;
                Initial Catalog=MobiNews;
                User id=Default;
                Password=testing1234"))
            {
                conn.Open();
                foreach(var story in _newStories)
                {
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO NEWSSTORIES (NEWSSTORYID, TITLE, NEWSSTORY, IMAGE, SUPPLIERSTORYID) VALUES (@0, @1, @2, @3, @4)", conn);
                    insertCommand.Parameters.AddWithValue("@0", GetNextId());
                    insertCommand.Parameters.AddWithValue("@1", story.Title);
                    insertCommand.Parameters.AddWithValue("@2", story.StoryText);
                    insertCommand.Parameters.AddWithValue("@3", story.ImagePath);
                    insertCommand.Parameters.AddWithValue("@4", story.SupplierStoryId);
                    insertCommand.ExecuteNonQuery(); 
                }
            }
        }
        private int GetNextId()
        {
            int Id = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=BENS-PC\SQLEXPRESS;
                Initial Catalog=MobiNews;
                User id=Default;
                Password=testing1234"))
            {
                conn.Open();
                SqlCommand nextIdCommand = new SqlCommand("Select MAX(NEWSSTORYID) as NextID From NEWSSTORIES", conn);
                var dbId= nextIdCommand.ExecuteScalar();
                Id = (dbId is DBNull) ? 1 : Convert.ToInt32(dbId); 
            }
            return Id; 
        }
    }
}
