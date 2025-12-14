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
using MVC.Services.DTO;

namespace MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ExtractFile _extract; //get a readonly ExtractFile
        private readonly AppDbContext _context;
        private readonly DatasetAnalysis _dataset;
        public DashboardController(ExtractFile extract, AppDbContext context, DatasetAnalysis dataset) //pass a random ExtractFile automatically to the controller? why?  
        {
            _extract=extract;
            _context=context;
            _dataset=dataset;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult DisplayFiles()
        {
            int userId=1;
            // int? userId=HttpContext.Session.GetInt32("UserId"); temporarily giving a userID so it runs
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var allFiles= _context.Storages
                        .Where(x=> x.UserId==userId) //remember to add .Value
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

            
            int userId=1;
            //int? userId = HttpContext.Session.GetInt32("UserId"); //get the UserId for the session, posted after login

            if (userId == null)
            {
                return RedirectToAction("DisplayFiles", "Dashboard");
            }
            var result=_extract.Extract(path); //using extract service to call Extract method in the instance of extractFile service
            var analysis=_dataset.Analyze(result);
            foreach (var col in analysis.Columns)
            {
                Console.WriteLine(
                    $"{col.ColName} | {col.Type} | Count={col.Count}"
                );
            }
            var record=new Storage
            {
                
                UserId=userId, 
                FileName=result.FileName,
                numRows=result.numRows,
                numCol=result.numCols,
                checksum=result.checksum,
                dateTime=DateTime.Now.ToString()
                

            };

            //check if checksum already exists
            bool alreadyExists = _context.Storages.Any(s =>
                s.UserId == userId && //Add .Value later
                s.checksum == result.checksum
            );
            if (alreadyExists)
            {
                return Content("File already uploaded");
            }

            //add to Storages dataset in AppDbContext
            _context.Storages.Add(record);
            _context.SaveChanges();

            return RedirectToAction("DisplayFiles", "Dashboard");
            //return View();
            //post will return a view maybe saying, file uploaded and showing history?
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}