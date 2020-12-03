using BlogAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin.Controllers
{
    public class MainPageData
    {
        private Models.BlogDBContext _context = new Models.BlogDBContext();
        public List<Category> ListOfCategories {get; set;}
        public List<post> ListOfPosts { get; set; }

        public  MainPageData()
        {
            ListOfCategories = getCategories();
            ListOfPosts = new List<post>();
        }

        private List<Category> getCategories()
        {
            List<Category> liste = new List<Category>();
            liste = _context.Category.ToList();
            liste = liste.OrderBy(l => l.title).ToList();

            return liste;
        }

        private List<post> getPosts()
        {
            List<post> liste = new List<post>();
            liste = _context.post.ToList();
            liste = liste.OrderBy(l => l.title).ToList();
            return liste;
        }

    }
}
