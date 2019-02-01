using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models.Views
{

    public  class ViewModelReportFeeOngoMarketingDaily
    {

        public string MKT_NAME { get; set; }

        public string FUND { get; set; }

        public string AGENT { get; set; }

        public string MKT { get; set; }

        public string DATE_NAV { get; set; }

        public decimal FEE_AMT { get; set; }

        public decimal TOT_UNI { get; set; }

        public decimal MKT_UNI { get; set; }

        public decimal MKT_FEE { get; set; }

        public decimal BF_MKTUNIT { get; set; }

        public decimal UNIT_NEW { get; set; }

        public decimal FEE_NEW { get; set; }

        public decimal UNIT_OLD { get; set; }

        public decimal FEE_OLD { get; set; }

        public decimal NAV  { get; set; }

        public decimal ON_MKTFEE { get; set; }

    }

}
