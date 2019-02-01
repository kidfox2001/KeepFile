using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace TFundSolution.Models.Views
{
    public class ViewModelExcelFeeAgent
    {
        public string Agent_Code { get; set; }
        public string Agent_Name { get; set; }
        public string Fund { get; set; }
        public decimal Unit_Last_Date { get; set; }
        public decimal Agent_Fee { get; set; }
    }
}
