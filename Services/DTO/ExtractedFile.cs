using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services.DTO
{
    public class ExtractedFile
        {
            public string FileName {get; set;}
            public int numCols {get; set;}
            public int numRows {get; set;}
            public List<string> headers {get; set;}
            public string checksum {get; set;}
            public List<List<string>> Table {get; set;}
            
        }
}