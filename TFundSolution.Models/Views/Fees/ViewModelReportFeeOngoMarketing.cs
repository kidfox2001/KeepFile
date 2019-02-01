using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models.Views
{
    public class ViewModelReportFeeOngoMarketing
    {

        public string mkt { get; set; }

        public string mkt_name { get; set; }

        public string fund { get; set; }

        public decimal total_fee_amt { get; set; }

        public decimal mkt_uni { get; set; }

        public decimal mkt_fee_amt { get; set; }

        public decimal mkt_fee_amt_net { get; set; }

        public string br_code { get; set; }

        public string agent_bran { get; set; }

       //public string agent_name { get; set; }

        public string agent_name
        {
            get
            {
               return this.BranchDescription.Where(q => q.MKT_CODE == this.mkt & q.MKT_AGEN == br_code.Substring(0, 3)).Single().AGENT_NAME;
            }
        }

        public List<V_MKT_BRANCH> BranchDescription = new List<V_MKT_BRANCH>();
        //public virtual List<V_MKT_BRANCH> BranchDescription
        //{
        //    get { return _BranchDescription; }
        //    set { _BranchDescription = value; }
        //}

    }
}
