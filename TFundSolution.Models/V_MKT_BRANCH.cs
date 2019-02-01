namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.V_MKT_BRANCH")]
    public partial class V_MKT_BRANCH
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string MKT_AGEN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string MKT_CODE { get; set; }

        [StringLength(70)]
        public string MKT_NAME { get; set; }

        [StringLength(6)]
        public string AGENT_BRAN { get; set; }

        [StringLength(250)]
        public string AGENT_NAME { get; set; }

        [StringLength(4)]
        public string MKT_DEPT { get; set; }

        [StringLength(10)]
        public string EMP_ID { get; set; }

        [StringLength(5)]
        public string HUB_ID { get; set; }

        [StringLength(50)]
        public string HUB_DESC { get; set; }

        [StringLength(5)]
        public string DEPTCT_ID { get; set; }

        [StringLength(50)]
        public string DEPTCT_DESC { get; set; }

        [StringLength(50)]
        public string TEL { get; set; }

        [StringLength(10)]
        public string FAX { get; set; }
    }
}
