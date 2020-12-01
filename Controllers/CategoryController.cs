using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogAdmin.Models;
using System.Net.Http;
using System.Configuration;

namespace BlogAdmin.Controllers
{
    public class CategoryController : Controller
    {
        private string webApiServer = ""; // configuration.GetSection("WebApiServer");
        public IActionResult Index()
        {
            return View("AddCategory");
        }



        public void Add(Models.Category model)
        {
            try
            {
                BlogDBContext context = new BlogDBContext();
                Category cat = new Category();
                cat.title = model.title;
                if (context != null)
                {
                    context.Category.Add(cat);
                    context.SaveChanges();
                }

             

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error: {0} ", ex.Message));
            }
        }

    }
}