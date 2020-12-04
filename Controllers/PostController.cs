using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogAdmin.Models;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace BlogAdmin.Controllers
{

    public class PostController : Controller
    {
        private BlogDBContext context = new BlogDBContext();

        private string webApiServer = "";

        public PostController(IConfiguration configuration)
        {
            webApiServer = configuration["WebApiServer"];
        }

        public IActionResult Index()
        {
            //post model = new post();
            List<Category> liste = new List<Category>();
            liste.Add(new Category(0, "Select"));
            liste.AddRange(context.Category.OrderBy(n => n.title).ToList());
            ViewBag.ListOfCategories = liste;
            return View("AddPost");
        }


        public IActionResult Edit(post model)
        {

            ViewBag.ListOfCategories = context.Category.ToList().OrderBy(l => l.title);
            ViewBag.PubDate = string.Format("{0}-{1:00}-{2:00}", model.publicationDate.Year, model.publicationDate.Month, model.publicationDate.Day);
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
            DateTime pubDate = DateTime.MinValue;
            int id = 0;
            int catID = 0;
            DateTime.TryParse(Request.Form["PubliDate"], out pubDate);
            int.TryParse(Request.Form["listCategories"], out catID);
 
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
                            _post.publicationDate = pubDate;
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

            //return RedirectToAction("Home", "Home");
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SavePost(post model)
        {
            int id = 0;
            int catID = 0;
            DateTime pubDate = DateTime.MinValue;
            int.TryParse(Request.Form["listCategories"], out catID);
            DateTime.TryParse(Request.Form["PubliDate"], out pubDate);
            try
            {
                Task<int> task = Task.Run(() =>
                {
                    BlogDBContext _context = new BlogDBContext();
                    post _post = null;
                    if (_context != null)
                    {
                        // check to make sure this Post does not already exist
                        _post = _context.post.Where(t => t.postID == model.postID).FirstOrDefault();
                        // if the Post doe exist, modify it.
                        if (_post != null)
                        {
                            _post.title = model.title;
                            _post.categoryID = catID;
                            _post.content = model.content;
                            _post.publicationDate = pubDate;
                            _context.SaveChanges();
                        }

                    }
                    // return the id of the Post that was created
                    return _context.post.Where(t => t.title == model.title).Select(c => c.postID).FirstOrDefault();
                }
                );
                // get the id the new Post from  the task
                id = await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error: {0}", ex.Message));
            }

            return RedirectToAction("Home", "Home");
        }
    }
}
