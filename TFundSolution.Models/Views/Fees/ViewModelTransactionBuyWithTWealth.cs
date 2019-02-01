using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models.Views
{
    public class ViewModelTransactionBuyWithTWealth
    {

        public string FUND { get; set; }

        public string BR_CODE { get; set; }

        public string MKT_CODE { get; set; }

        public DateTime TRAN_DATE { get; set; }

        public decimal MKT_AMT { get; set; }

        public string CIS_NO { get; set; }

        public string ORDER_TYPE { get; set; }

        public string TWEALTH { get; set; }

        public string TWEALTH_PAY { get; set; }

    }
}
