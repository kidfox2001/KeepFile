using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models.Views
{
    public class ViewModelReportFeeOngoIIAMarketing
    {

        public string Mkt_Name { get; set; }

        public string Mkt_Code { get; set; }

        public string Fund { get; set; }

        public decimal Total_Fee_Amt { get; set; }

        public decimal Mkt_Unit { get; set; }

        public decimal Mkt_Fee_Amt { get; set; }

    }

    public class ViewModelReportFeeOngoIIAMarketingSummary
    {

        public string Fund { get; set; }

        public decimal Mkt_Unit { get; set; }

        public decimal Mkt_Fee_Amt { get; set; }

    }
}
