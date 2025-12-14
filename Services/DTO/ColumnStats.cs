using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services.DTO
{
    public class ColumnStats
        {
            public string ColName {get; set;} = "";
            public ColumnType Type {get; set;}

            public int Count { get; set; }
            public int MissingCount { get; set; }

            //Numeric stats
            public double? Mean {get; set;}
            public double? Median { get; set; }
            public double? Min { get; set; }
            public double? Max { get; set; }

            //categorical stats

            public string? Mode {get; set;}
            public int? UniqueCount {get; set;}


        }
}