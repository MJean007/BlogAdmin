using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogAdmin.Models;
using System.Net.Http;
using System.Configuration;
using BlogAdmin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogAdmin.Controllers
{

    public class PostController : Controller
    {
        private BlogDBContext context = new BlogDBContext();
        public IActionResult Index()
        {
            //post model = new post();
            //var liste = new SelectList(context.Category.OrderBy(n => n.title), "categoryID", "title", model.categoryID);
            List<Category> liste = new List<Category>();
            liste.Add(new Category(0, "Select"));
            liste.AddRange(context.Category.OrderBy(n => n.title).ToList());
            ViewBag.ListOfCategories = liste;
            //ViewBag.ListOfCategories = context.Category.OrderBy(n => n.title).ToList();
            return View("AddPost");
        }


        public IActionResult Edit(Category model)
        {
            
            ViewBag.ListOfCategories = context.Category.ToList().OrderBy(l => l.title);
            return View("EditPost", model);
        }
        private bool ValidatePage()
        {
            bool ok = true;

            return ok;
        }
        [HttpPost]
        public async Task<IActionResult> Add(Models.post model)
        {
            if (!ValidatePage())
            {
                return null;
            }
            int id = 0;
            int catID = 0;

            int.TryParse(Request.Form["listCategories"], out catID);
            //string strCatID = Request["listCategories"];
            try
            {
                Task<int> task = Task.Run(() =>
                {
                    BlogDBContext _context = new BlogDBContext();
                    post _post = null;
                    if (_context != null)
                    {
                        // check to make sure this post does not already exist

                        _post = _context.post.Where(t => t.title == model.title).FirstOrDefault();
                        // if the category does not exist, create it.
                        if (_post == null)
                        {
                            _post = new post();
                            _post.title = model.title;
                            _post.categoryID = catID;
                            _post.publicationDate = DateTime.Now;
                            _post.content = model.content;
                            _context.post.Add(_post);
                            _context.SaveChanges();
                        }
                        else
                        {
                            // show error message.
                            Console.Write("");
                        }

                    }
                    // return the id of that category created
                    return _context.Category.Where(t => t.title == model.title).Select(c => c.categoryID).FirstOrDefault();
                }
                );
                // get the id the new category from  the task
                id = await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error: {0}", ex.Message));
            }

            //return View("~/Views/Home/Home.cshtml");
            return RedirectToAction("Home", "Home");
        }

    }
}
