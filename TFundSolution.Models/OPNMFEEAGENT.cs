namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.OPNMFEEAGENT")]
    public partial class OPNMFEEAGENT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string FUND { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string AGENT { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime DATE_NAV { get; set; }

        public decimal? FEE_AMT { get; set; }

        public decimal? FEE_EXT { get; set; }

        public decimal? NET_AMT { get; set; }

        public decimal? TOT_UNI { get; set; }

        public decimal? AGENT_UNI { get; set; }

        public decimal? AGENT_FEE { get; set; }

        [StringLength(10)]
        public string UPD_BY { get; set; }

        public DateTime? UPD_DATE { get; set; }

        public decimal? BF_AGENTUNIT { get; set; }

        public decimal? PER_UNIT { get; set; }

        public decimal? PER_FEE { get; set; }

        public decimal? AGENT_UNI_NEW { get; set; }

        public decimal? AGENT_FEE_NEW { get; set; }
    }
}
