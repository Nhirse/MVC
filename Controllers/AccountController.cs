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
        private readonly AuthenticateLogin _auth;

        public AccountController(AuthenticateLogin auth)
        {
            _auth = auth;
        }
        [HttpPost]    
        public IActionResult Login(string Username, string Password)
        {
            var user = _auth.ValidateUser(username, password)

            if (user == null)
            {
            //utilize authentication service
            return View(User);
            }
            Http.Context.Session.SetInt32("userID", user.UserId);

            return RedirectToAction ("Home", "Dashboard")

            //redirect to Dashboard.home()
        }

    }
}
