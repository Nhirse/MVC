using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services.DTO
{
    public class DataAnalysis
        {
            public int RowCount { get; set; }
            public int ColumnCount { get; set; }
            public List<ColumnStats> Columns {get; set;} = new();
        }
}