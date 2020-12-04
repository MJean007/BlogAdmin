using BlogAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Drawing.Text;
//using Newtonsoft.Json;

namespace BlogAdmin.Controllers
{
    public class MainPageData
    {
        private Models.BlogDBContext _context = new Models.BlogDBContext();
        public List<Category> ListOfCategories { get; set; }
        public List<post> ListOfPosts { get; set; }

        private string webApiServer = "";


        public MainPageData(string _webApiServer)
        {
            webApiServer = _webApiServer;
            ListOfCategories = getCategories();
            ListOfPosts = getPosts();
        }

        private List<Category> getCategories()
        {
            //Task val = getCategoriesFromWebApi();

            List<Category> liste = new List<Category>();
            liste = _context.Category.ToList();
            liste = liste.OrderBy(l => l.title).ToList();

            return liste;
        }

        private async Task<IActionResult> getCategoriesFromWebApi()
        {
            List<Category> liste = new List<Category>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(liste), Encoding.UTF8, "application/json");
                    //List<Category> content = new StringContent(JsonConvert.SerializeObject(liste), Encoding.UTF8, "application/json");

                    //byte[] jsonUtf8Bytes;
                    //var options = new JsonSerializerOptions;
                    //{
                    //    WriteIndented = true
                    //};
                    //jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(liste, options);
                    //string content = JsonSerializer.Serialize(liste);
                    string endpoint = webApiServer + "Category";
                    
                    using (var Response = await client.GetAsync(endpoint))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            //object TempData = null;
                            var item2do = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                            //return RedirectToAction("Index");
                            liste = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(item2do));
                        }
                        else if (Response.StatusCode == System.Net.HttpStatusCode.Conflict)
                        {
                            //ModelState.Clear();
                            //ModelState.AddModelError("Username", "Username Already Exist");
                            //return View();
                        }
                        else
                        {
                            //return View();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur: " + ex.Message);
            }

            //return liste;
            return null;
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
