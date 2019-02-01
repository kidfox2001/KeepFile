using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models.Views
{
    public class ViewModelReportFeeOngoAgentDaily
    {

        public string fund { get; set; }

        public string agent { get; set; }

        public string date_nav { get; set; }

        public decimal fee_amt { get; set; }

        public decimal fee_ext { get; set; }

        public decimal net_amt { get; set; }

        public decimal tot_uni { get; set; }

        public decimal agent_uni { get; set; }

        public decimal agent_fee { get; set; }

        public decimal bf_agentunit { get; set; }

        /// <summary>
        /// rate agent
        /// </summary>
        public decimal on_agentfee { get; set; }

        public decimal agent_uni_new { get; set; }

        public decimal agent_fee_new { get; set; }

        public decimal agent_uni_old { get; set; }

        public decimal agent_fee_old { get; set; }

        public decimal nav { get; set; }

        public decimal on_agentfee2 { get; set; }

        public decimal ongo_tier_rate { get; set; }

        public decimal ongo_new_rate { get; set; }

    }
}
