using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models.Views
{
    public class ViewModelReportFee
    {

        public ViewModelReportFee()
        {
            this.IsFilter = true;
            this.RowPerPage = 15;
            this.ChkAgentSelects = new List<string>();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsFilter { get; set; }
        public int RowPerPage { get; set; }
        public int CountPage { get; set; }
        public bool IsSelectIIAOnly 
        {
            get
            {
                var agentIIA = "IIA000001";
                var isSelectIIA =  this.ChkAgentSelects.Any(q => q ==  agentIIA);
                var isSelectAll =  this.ChkAgentSelects.Any(q => q ==  "000");

                if (isSelectIIA & this.ChkAgentSelects.Count == 1)
                {
                    return true;
                }

                if (isSelectIIA &  isSelectAll & this.ChkAgentSelects.Count == 2)
                {
                     return true;
                }

                return false;
            }
        }
        public string AgentSelectDetail 
        {
            get
            {
                if (this.ChkAgents.Count == 1 && this.ChkAgents.Any(q => q.ID == "000"))
                {
                    return "ทุกตัวแทน";
                }

                if (this.ChkAgents.Count > 0)
                {
                    List<String> allSelect = this.ChkAgents.Where(q => q.ID != "000" && q.IsChecked).Select(q => q.ID).ToList();
                    var txtJoin = string.Join(",", allSelect);
                    var maxLength = txtJoin.Length > 100 ? 100 : txtJoin.Length;
                    return txtJoin.Substring(0, maxLength) + (allSelect.Count > 10 ? "..." : "");
                }

                return "";
            }
        }
        

        public List<CheckBoxListItem> ChkAgents { get; set; }
        public List<string> ChkAgentSelects { get; set; }
        public List<string> Agents { get; set; }
        public IEnumerable<FEE_SETTING> ReportFeeOngoAgent { get; set; }
        public IEnumerable<FEE_SETTING> ReportFeeOngoAgentByPage { get; set; }
        public IEnumerable<ViewModelReportFeeOngoAgentDaily> ReportFeeOngoAgentDailyDetail { get; set; }
        public IEnumerable<ViewModelReportFeeOngoAgentDaily> ReportFeeOngoAgentDailyDetailByPage { get; set; }
        public IEnumerable<ViewModelReportFeeOngoMarketingDaily> ReportFeeOngoMarketingDaily { get; set; }
        public IEnumerable<ViewModelReportFeeOngoMarketingDaily> ReportFeeOngoMarketingDailyPage { get; set; }
        public IEnumerable<ViewModelReportFeeOngoMarketing> ReportFeeOngoMarketingSummary { get; set; }
        public IEnumerable<ViewModelReportFeeOngoMarketing> ReportFeeOngoMarketingSummaryPage { get; set; }

       
    }

}
