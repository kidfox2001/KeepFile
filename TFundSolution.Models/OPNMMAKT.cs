namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CIS.OPNMMAKT")]
    public partial class Marketing
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string MKT_AGEN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string MKT_BRANCH { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string MKT_CODE { get; set; }

        [StringLength(70)]
        public string MKT_NAME { get; set; }

        [StringLength(6)]
        public string BRANCH_CODE { get; set; }


        #region Relation

        private ICollection<FeeMarketing> _FeeDailys = new HashSet<FeeMarketing>();
        public virtual ICollection<FeeMarketing> FeeDailys
        {
            get { return _FeeDailys; }
            set { _FeeDailys = value; }
        }

        #endregion


        #region Method

        public decimal SumFeeOngoByDate(DateTime startDate, DateTime endDate)
        {
            var feeDailys = this.FeeDailys.Where(m => m.OnDateAgentFee.FEE_DATE >= startDate &&
                                                m.OnDateAgentFee.FEE_DATE <= endDate).ToList();

            return feeDailys.Sum(q => q.TOTAL_FEE_ONGO ?? 0);
        }

        #endregion

        //[ForeignKey("MarketingOwner")]
        //[NotMapped]
        //public string BR_CODE
        //{
        //    get
        //    {
        //        return this.MKT_AGEN + MKT_BRANCH;
        //    }
        //}
        //public Agent AgentOwner { get; set; }

        //[StringLength(10)]
        //public string ID_PASS { get; set; }

        //[StringLength(1)]
        //public string GRP_TYPE { get; set; }

        //[StringLength(4)]
        //public string MKT_DEPT { get; set; }

        //[StringLength(1)]
        //public string PRIVATE_FLAG { get; set; }

        //[StringLength(1)]
        //public string GRP_TYPE_PRIVATE { get; set; }

        //[StringLength(2)]
        //public string RESIGN { get; set; }

        //public decimal? TARGET { get; set; }

        //[StringLength(10)]
        //public string IP_LEVEL { get; set; }

        //public DateTime? IP_REGISTER { get; set; }

        //[StringLength(6)]
        //public string IP_CODE { get; set; }

        //[StringLength(1)]
        //public string PVD_FLAG { get; set; }

        //[StringLength(1)]
        //public string GRP_TYPE_PVD { get; set; }

        //[StringLength(1)]
        //public string RESIGN_FLG { get; set; }

        //public DateTime? RESIGN_DATE { get; set; }



        //[StringLength(15)]
        //public string UPD_BY { get; set; }

        //public DateTime? UPD_DATE { get; set; }

        //public bool? USER_COUNT_PASSERR { get; set; }

        //public DateTime? USER_LAST_PASSCHG { get; set; }
    }
}
