using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MobiNews
{
    public class NewsStory
    {
        internal string Title { get; set; }
        internal string StoryText { get; set; }
        internal int SupplierStoryId { get; set; }
        internal string ImagePath { get; set; }

        /// <summary>
        /// Method to insert the story into the database. First checks whether the story exists. 
        /// </summary>
        public void InsertStory()
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                SqlCommand insertCommand = new SqlCommand(@"INSERT INTO[dbo].[NEWSSTORIES]
                                                            ([NEWSSTORYID]
                                                            ,[TITLE]
                                                            ,[NEWSSTORY]
                                                            ,[IMAGE]
                                                            ,[SUPPLIERSTORYID])
                                                            SELECT
                                                            (SELECT coalesce(max(NewsStoryId) + 1, 1) from NEWSSTORIES),
                                                            @Title,
                                                            @StoryText,
                                                            @ImagePath,
                                                            @SupplierStoryId;", conn);
                insertCommand.Parameters.AddWithValue("@Title", this.Title);
                insertCommand.Parameters.AddWithValue("@StoryText", this.StoryText);
                insertCommand.Parameters.AddWithValue("@ImagePath", this.ImagePath);
                insertCommand.Parameters.AddWithValue("@SupplierStoryId", this.SupplierStoryId);
                insertCommand.ExecuteNonQuery();
            }
        }
        static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MobiNewsConnStr"].ConnectionString;
        }
    }
}
