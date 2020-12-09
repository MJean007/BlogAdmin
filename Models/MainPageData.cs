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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Newtonsoft.Json;

namespace BlogAdmin.Controllers
{
    public class MainPageData
    {
        private Models.BlogDBContext _context = new Models.BlogDBContext();
        public List<Category> ListOfCategories { get; set; }
        public List<post> ListOfPosts { get; set; }

        private string webApiServer = "";

        private readonly HttpClient client = null;
        private IConfiguration _config = null;


        private MainPageData()
        { 
        }

        public  MainPageData(HttpClient client, IConfiguration configuration)
        {
            _config = configuration;
            webApiServer = configuration["WebApiServer"];
            this.client = client;
            //ListOfCategories = getCategories();
            //ListOfPosts = getPosts();
        }

        private async Task<MainPageData> InitializeAsync()
        {
            ListOfCategories = await getCategoriesFromWebApi();
            ListOfPosts = getPosts();
            return this;
        }

        public static  Task<MainPageData> CreateAsync()
        {
            var result = new MainPageData();
            return result.InitializeAsync();
        }


        private List<Category> getCategories()
        {
            //Task val = getCategoriesFromWebApi();
            //Task.Run(() => Console.WriteLine("ok doke"));
            List<Category> liste = new List<Category>();

            //Task.Run((List<Category>) =>
            //{ 
            //    liste = await getCategoriesFromWebApi()


            //});

            liste = _context.Category.ToList();
            liste = liste.OrderBy(l => l.title).ToList();

            return liste;
        }

        private async Task<List<Category>> getCategoriesFromWebApi()
        {
            List<Category> liste = new List<Category>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(webApiServer + "category");
                string stringData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                liste = JsonSerializer.Deserialize<List<Category>>(stringData, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur: " + ex.Message);
            }

            //return liste;
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
