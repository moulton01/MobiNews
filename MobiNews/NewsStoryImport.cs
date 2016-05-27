using System;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Collections.Generic;
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
            // Add any new news suppliers here
            newStories.AddRange(NewsTodayImport());
            newStories.AddRange(TechMediaNewsImport());
            // Insert all new stories found
            InsertStories(newStories);
            // Alert user import has finished successfully.
            MessageBox.Show("Import Finished."); 
        }
        /// <summary>
        /// Handles imports for NewsToday supplier
        /// </summary>
        /// <returns>List of new stories</returns>
        private List<NewsStory> NewsTodayImport()
        {
            List<NewsStory> stories = new List<NewsStory>();
            XmlDocument doc = new XmlDocument();
            try
            {
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
            }
            catch(Exception e)
            {
                MessageBox.Show(String.Format("The Imports for NewsToday did not complete successfully. The following error occurred:{0}{1}", Environment.NewLine, e.Message));
            }
            return stories; 
        }
        /// <summary>
        /// Handles imports for TechMedia supplier, should only come one at a time. 
        /// </summary>
        /// <returns>List of new stories</returns>
        private List<NewsStory> TechMediaNewsImport()
        {
            List<NewsStory> stories = new List<NewsStory>();
            try
            {
                var xmlFiles = Directory.GetFiles("c:\\news\\", "*.xml");
                foreach (var file in xmlFiles)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(file);
                    XmlNode story = doc.SelectSingleNode("/NewsStory");
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
            catch(Exception e)
            {
                MessageBox.Show(String.Format("The Imports for TechMediaNews did not complete successfully. The following error occurred:{0}{1}", Environment.NewLine, e.Message));
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
