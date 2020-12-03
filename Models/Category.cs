using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Models
{
    public class Category
    {
        public string title { get; set; }
        public int categoryID { get; set; }
        public Category()
        {

        }


        public Category(int _catID, string _title)
        {
            categoryID = _catID;
            title = _title;
        }


    }
}
