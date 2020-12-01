﻿using System;
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



        //public void Add(Models.Category model)
        public async Task<IActionResult> Add(Models.Category model)
        {
            //    try
            //    {
            //        BlogDBContext context = new BlogDBContext();
            //        Category cat = new Category();
            //        cat.title = model.title;
            //        if (context != null)
            //        {
            //            context.Category.Add(cat);
            //            context.SaveChanges();
            //        }



            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(string.Format("Error: {0} ", ex.Message));
            //    }
            //}

            int id = 0;
            try
            {
                Task<int> task = Task.Run(() =>
                {
                    BlogDBContext _context = new BlogDBContext();
                    Category cat = null;
                    if (_context != null)
                    {
                        // check to make sure this category does not already exist
                        cat = _context.Category.Where(t => t.title == model.title).FirstOrDefault();
                        // if the category does not exist, create it.
                        if (cat == null)
                        {
                            cat = new Category();
                            cat.title = model.title;
                            _context.Category.Add(cat);
                            _context.SaveChanges();
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

            return View("~/Views/Home/Home.cshtml");
        }
    }
}