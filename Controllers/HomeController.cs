using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogAdmin.Models;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace BlogAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string webApiServer = "";
        private readonly HttpClient client = null;
        private IConfiguration _config = null;
        public HomeController(HttpClient client, IConfiguration configuration)
        {
            webApiServer = configuration["WebApiServer"];
            this.client = client;
            _config = configuration;
        }

        public async Task<IActionResult> Home()
        {
            var _model = new MainPageData(client, _config);
            var model = await MainPageData.CreateAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
