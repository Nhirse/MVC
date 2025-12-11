using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class AccountController: Controller
    {
        
        public IActionResult Login()
        {
            //utilize authentication service
            return View(User);
            

            //redirect to Dashboard.home()
        }

    }
}