using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;
using MVC.Data;
using MVC.Services.DTO;


namespace MVC.Services
{
    public class DatasetAnalysis
    {
        public DataAnalysis Analyze(ExtractedFile extractedfile)
        {
            var result=new DataAnalysis
            {
                RowCount=extractedfile.numRows,
                ColumnCount=extractedfile.numCols
            };

            
            for (int i=0;i<result.ColumnCount;i++)
            {
                List<string> current_col= GetColumn(extractedfile.Table,i);
                bool isNumeric= IsNumericColumn(current_col);

                var column=new ColumnStats
                {
                    ColName=extractedfile.headers[i],
                    Type= isNumeric? ColumnType.Numeric:ColumnType.Categorical,
                    Count=current_col.Count,
                    MissingCount= extractedfile.numRows - current_col.Count
                };

                if(isNumeric)
                {
                    var NumericValues=ParseNumericColumn(current_col);
                    column.Mean=ComputeMean(NumericValues);
                    column.Median = ComputeMedian(NumericValues);
                    column.Min = NumericValues.Min();
                    column.Max = NumericValues.Max();

                }
                else
                {
                    column.Mode = ComputeMode(current_col);
                    column.UniqueCount = ComputeUniqueCount(current_col);
                }

                result.Columns.Add(column);
   
            }
            return result;
        }

        
        private List<string> GetColumn(List<List<string>> table, int columnIndex)
        {
            // skip header row (index 0)
            return table
                .Skip(1)
                .Select(row => row[columnIndex])
                .Where(cell => !string.IsNullOrWhiteSpace(cell))
                .ToList();
        }
        private bool IsNumericColumn(List<string> values)
        {
            foreach (var value in values)
            {
                if (!double.TryParse(value, out _))
                    return false;
            }
            return true;
        }
        private List<double> ParseNumericColumn(List<string> values)
        {
            return values
                .Select(v => double.Parse(v))
                .ToList();
        }
        private double ComputeMean(List<double> values)
        {
            return values.Sum() / values.Count;
        } 
        private double ComputeMedian(List<double> values)
        {
            var sorted = values.OrderBy(v => v).ToList();
            int count = sorted.Count;
            int mid = count / 2;

            if (count % 2 == 0)
            {
                // even
                return (sorted[mid - 1] + sorted[mid]) / 2.0;
            }
            else
            {
                // odd
                return sorted[mid];
            }
        }

        private string ComputeMode(List<string> values)
        {
            return values
                .GroupBy(v => v)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }

        private int ComputeUniqueCount(List<string> values)
        {
            return values.Distinct().Count();
        }

        

        
    }
}