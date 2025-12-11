using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MVC.Controllers
{
    //[Route("[controller]")]
    public class DashboardController : Controller
    {
       /* private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult DisplayFiles()
        {
            return View();
            //utilize display service
            //display service should pass list of all files
            //redirect to DisplayFiles page

        }

         public IActionResult ExtractNewFiles()
        {
            return View();
            //post will activate service that adds a file to the storage
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}