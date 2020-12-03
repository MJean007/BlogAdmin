using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class post
    {
        public int postID { get; set; }
        public string title { get; set; }

        public DateTime publicationDate { get; set; }
        public string content { get; set; }
        public int categoryID { get; set; }
    }
}
