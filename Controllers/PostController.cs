using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogAdmin.Models;
using System.Net.Http;
using System.Configuration;
using BlogAdmin.Models;

namespace BlogAdmin.Controllers
{

    public class PostController : Controller
    {
        private BlogDBContext context = new BlogDBContext();
        public IActionResult Index()
        {
            ViewBag.ListOfCategories = context.Category.ToList().OrderBy(l => l.title);
            return View("AddPost");
        }


        public IActionResult Edit(Category model)
        {
            
            ViewBag.ListOfCategories = context.Category.ToList().OrderBy(l => l.title);
            return View("EditPost", model);
        }

    }
}
