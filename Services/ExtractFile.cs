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
using System.Security.Cryptography;
using MVC.Services.DTO;



namespace MVC.Services
{
    public class ExtractFile
    {
        public ExtractedFile Extract(IFormFile csvfile)
        {
            List<string> lines = new(); //list of rows
            List<List<string>> table= new List<List<string>>();

            using (var reader=new StreamReader(csvfile.OpenReadStream()))
            {
                string? line;
                while ((line=reader.ReadLine())!=null) //while there's a line to be read in the textfile
                {
                    lines.Add(line);
                }
            }
            // 1️⃣ Read raw file bytes for checksum
            byte[] fileBytes;
            using (var ms=new MemoryStream())
            {
                csvfile.CopyTo(ms);
                fileBytes=ms.ToArray();
            }
            

            // 2️⃣ Compute checksum from raw bytes
            string checksumm = ComputeChecksum(fileBytes);
            
            table=lines
                .Select(x=>x.Split(',').ToList())
                .ToList();

            int numRowss=lines.Count();
            int numColss=table[0].Count();

            List<string> header= table[0];

            return new ExtractedFile
            {
                FileName=csvfile.FileName,
                numCols=numColss,
                numRows=numRowss,
                checksum=checksumm,
                headers=header,
                Table=table

            };
        }
             
        private string ComputeChecksum(byte[] fileBytes)
        {
            using var sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(fileBytes);
            return Convert.ToHexString(hash); // uppercase hex string
        }
        
    }
    
    


}