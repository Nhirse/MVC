using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using MVC.Services;
using MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ExtractFile _extract; //get a readonly ExtractFile
        private readonly AppDbContext _context;
        public DashboardController(ExtractFile extract, AppDbContext context) //pass a random ExtractFile automatically to the controller? why?  
        {
            _extract=extract;
            _context=context;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult DisplayFiles()
        {
            int? userId=HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var allFiles= _context.Storages
                        .Where(x=> x.UserId==userId.Value)
                        .Include(x=>x.User)
                        .ToList();

            return View(allFiles);
            //utilize display service
            //display service should pass list of all files
            //redirect to DisplayFiles page

        }

         public IActionResult ExtractNewFiles(string filepath)
        {
            //temporary file path before front end is up
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "TestFiles",
                "student_exam_scores.csv"
            );

            var result=_extract.Extract(path);
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var record=new Storage
            {
                
                UserId=userId.Value, 
                FileName=result.FileName,
                numRows=result.numRows,
                numCol=result.numCols,
                checksum=//do checksum logic,
                dateTime=DateTime.Now.ToString()
                

            };
            //add to Storages dataset in AppDbContext
            _context.Storages.Add(record);
            _context.SaveChanges();

            return View();
            //post will return a view maybe saying, file uploaded and showing history?
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}