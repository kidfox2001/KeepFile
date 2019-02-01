namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SAS.SASMORDERTYPE")]
    public partial class SASMORDERTYPE
    {
        [Key]
        [StringLength(15)]
        public string ORDER_TYPE { get; set; }

        [StringLength(50)]
        public string DESCRIPTION { get; set; }

        [StringLength(8)]
        public string UPD_BY { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(1)]
        public string SUIT_FLG { get; set; }
    }
}
