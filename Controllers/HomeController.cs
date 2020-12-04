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


namespace BlogAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string webApiServer = "";
        public HomeController(IConfiguration configuration)
        {
            webApiServer = configuration["WebApiServer"];
        }

        public IActionResult Home()
        {

            var model = new MainPageData(webApiServer);

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
