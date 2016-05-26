using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiNews
{
    public class NewsStory
    {
        internal string Title { get; set; }
        internal string StoryText { get; set; }
        internal int SupplierStoryId { get; set; }
        internal string ImagePath { get; set; }
    }
}
