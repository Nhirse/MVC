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
using MVC.Models.ViewModels;
using System.Text.Json;


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

        public IActionResult Home(DashboardViewModel viewmodel)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user= _context.Users.First(u=>u.UserId==userId.Value);
            var files= _context.Storages.Where(u=>u.UserId==userId.Value)
                        .ToList();
            var model=new DashboardViewModel
            {
                TotalFiles=files.Count,
                fullName=user.fullName,
                LastUploaded=files
                    .OrderByDescending(f=>f.dateTime)
                    .Select(f=>(DateTime?)DateTime.Parse(f.dateTime))
                    .FirstOrDefault()
            };

            return View(model);
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
        [HttpGet]
        public IActionResult ExtractNewFiles()
        {
            return View();
        }

        [HttpPost]
         public IActionResult ExtractNewFiles(IFormFile file)
        {

            int? userId = HttpContext.Session.GetInt32("UserId"); //get the UserId for the session, posted after login

            if (userId == null)
            {
                return RedirectToAction("Home", "Dashboard");
            }
            var result=_extract.Extract(file); //using extract service to call Extract method in the instance of extractFile service
            var analysis=_dataset.Analyze(result);

            string json = JsonSerializer.Serialize(analysis);

            foreach (var col in analysis.Columns)
            {
                Console.WriteLine(
                    $"{col.ColName} | {col.Type} | Count={col.Count}"
                );
            }
            var record=new Storage
            {
                
                UserId=userId.Value, 
                FileName=result.FileName,
                numRows=result.numRows,
                numCol=result.numCols,
                checksum=result.checksum,
                dateTime=DateTime.Now.ToString(),

                AnalysisJson=json
            };

            //check if checksum already exists
            bool alreadyExists = _context.Storages.Any(s =>
                s.UserId == userId.Value && //Add .Value later
                s.checksum == result.checksum
            );
            if (alreadyExists)
            {
                TempData["InfoMessage"] = "This file was already uploaded earlier.";
                return RedirectToAction("DisplayFiles", "Dashboard");
            }

            //add to Storages dataset in AppDbContext
            _context.Storages.Add(record);
            _context.SaveChanges();

            return RedirectToAction("DisplayFiles", "Dashboard");
            //return View();
            
        }
        public IActionResult FileAnalysis(int storageId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized();

            var file = _context.Storages
                .FirstOrDefault(s => s.FileId == storageId && s.UserId == userId.Value);

            if (file == null || string.IsNullOrEmpty(file.AnalysisJson))
                return NotFound();

            var analysis = JsonSerializer.Deserialize<DataAnalysis>(file.AnalysisJson);

            return PartialView("FileAnalysisModal", analysis);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}