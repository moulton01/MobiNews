using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MobiNews
{
    public class NewsStory
    {
        internal string Title { get; set; }
        internal string StoryText { get; set; }
        internal int SupplierStoryId { get; set; }
        internal string ImagePath { get; set; }

        /// <summary>
        /// Method called before any story is inserted to the database, checks whether it already exists. 
        /// </summary>
        public bool StoryExists()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=BENS-PC\SQLEXPRESS;
                Initial Catalog=MobiNews;
                User id=Default;
                Password=testing1234"))
            {
                conn.Open();
                SqlCommand storyExistsCommand = new SqlCommand("Select COUNT(*) From NEWSSTORIES where NEWSSTORYID = @0", conn);
                storyExistsCommand.Parameters.AddWithValue("@0", this.SupplierStoryId); 
                if (Convert.ToInt32(storyExistsCommand.ExecuteScalar()) == 0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Gets the next NewsStoryID (Key to the NewsStories Table).
        /// </summary>
        /// <returns>The next increment on NewsStoryID</returns>
        public int GetNextId()
        {
            int nextId = 0; 
            using (SqlConnection conn = new SqlConnection(@"Data Source=BENS-PC\SQLEXPRESS;
                Initial Catalog=MobiNews;
                User id=Default;
                Password=testing1234"))
            {
                conn.Open();
                SqlCommand nextIdCommand = new SqlCommand("Select MAX(NEWSSTORYID) as NextID From NEWSSTORIES", conn);
                var dbId = nextIdCommand.ExecuteScalar();
                nextId = (dbId is DBNull) ? 1 : Convert.ToInt32(dbId) + 1;
            }
            return nextId; 
        }
        public void InsertStory()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=BENS-PC\SQLEXPRESS;
                Initial Catalog=MobiNews;
                User id=Default;
                Password=testing1234"))
            {
                if (!this.StoryExists())
                {
                    conn.Open();
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO NEWSSTORIES (NEWSSTORYID, TITLE, NEWSSTORY, IMAGE, SUPPLIERSTORYID) VALUES (@0, @1, @2, @3, @4)", conn);
                    insertCommand.Parameters.AddWithValue("@0", GetNextId());
                    insertCommand.Parameters.AddWithValue("@1", this.Title);
                    insertCommand.Parameters.AddWithValue("@2", this.StoryText);
                    insertCommand.Parameters.AddWithValue("@3", this.ImagePath);
                    insertCommand.Parameters.AddWithValue("@4", this.SupplierStoryId);
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }

}
