using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Services
{
    class Fetch_data
    {
        protected static List<List<string>> columns= new List<List<string>>();
        protected string name_of_file;
        protected List<string> lines;
        protected static int num_columns = 0;
        public string FileName
        {
            get { return name_of_file; }
            set { name_of_file = value; }
        }

        public Fetch_data(string nameFile)
        {
            name_of_file = nameFile;
            lines = new List<string>();
            
        }

        public virtual void Extract_file()
        {
            string path = name_of_file;
            lines = File.ReadAllLines(path).ToList();

        }
        public void AddColumn(List<string> col_list)
        {
            columns.Add(col_list);
        }



    }
    class Create_column : Fetch_data
    {
        private List<string> data_list;
        private int chosen_col;

        public Create_column(string nameFile, int col) : base(nameFile)
        {
            num_columns += 1;
            name_of_file = nameFile;
            chosen_col = col;
            data_list = new List<string>();
        }
        public override void Extract_file()
        {
            base.Extract_file();
            data_list = lines[chosen_col].Split(',').ToList();
            AddColumn(data_list);

        }
        public void getColumn()
        {
            foreach (string i in data_list)
                Console.WriteLine(i);
        }

    }
    public class ExtractFile
    {
        public ExtractedFileResult Extract(string filename)
        {
            List<string> 
        }
        
    }
}