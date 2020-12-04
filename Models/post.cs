using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class post
    {
        public int postID { get; set; }
        public string title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}")]
        public DateTime publicationDate { get; set; }
        public string content { get; set; }
        public int categoryID { get; set; }
    }
}
