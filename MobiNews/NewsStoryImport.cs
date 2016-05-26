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
                // Add any new news suppliers here
                newStories.AddRange(NewsTodayImport());
                newStories.AddRange(TechMediaNewsImport());
  
                InsertStories(newStories);
                MessageBox.Show("Import Finished with no errors."); 
            }
            catch(Exception e)
            {
                MessageBox.Show(String.Format("The following error occured when importing new stories: {0} {1}", Environment.NewLine, e.Message));
            }
        }
        /// <summary>
        /// Handles imports for NewsToday supplier
        /// </summary>
        /// <returns>List of new stories</returns>
        private List<NewsStory> NewsTodayImport()
        {
            List<NewsStory> stories = new List<NewsStory>();
            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.example.com/xml/");
            XmlElement root = doc.DocumentElement;
            XmlNodeList newStories = root.SelectNodes("/publishing/stories/story/");
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
            return stories; 
        }
        /// <summary>
        /// Handes imports for TechMedia supplier
        /// </summary>
        /// <returns>List of new stories</returns>
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
        /// <summary>
        /// Once all stories have been imported via XML, this method is called and inserts only new 
        /// record into the MobiNews Database.
        /// </summary>
        /// <param name="_newStories">List of new stories</param>
        private void InsertStories(List<NewsStory> _newStories)
        {
            foreach(var story in _newStories)
            {
                story.InsertStory(); 
            }
        }
    }
}
