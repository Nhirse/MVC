using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Runtime.CompilerServices;


namespace MVC.Services
{
    public class ExtractFile
    {
        public ExtractedFile Extract(string filepath)
        {
            List<string> lines = File.ReadAllLines(filepath).ToList(); //list of rows
            List<List<string>> table= new List<List<string>>();
            
            table=lines
                .Select(x=>x.Split(',').ToList())
                .ToList();

            int numRowss=lines.Count();
            int numColss=table[0].Count();

            List<string> header= table[0];

            return new ExtractedFile
            {
                filename=Path.GetFileName(filepath),
                numCols=numColss,
                numRows=numRowss,
                headers=header,
                Table=table

            };
            
        }
            
   }

    public class ExtractedFile
    {
        public string filename {get; set;}
        public int numCols {get; set;}
        public int numRows {get; set;}
        public List<string> headers {get; set;}
        public List<List<string>> Table {get; set;}
    }

}