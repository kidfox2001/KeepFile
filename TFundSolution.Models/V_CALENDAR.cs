namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.V_CALENDAR")]
    public partial class V_CALENDAR
    {
        public decimal? DAY_WEEK { get; set; }

        public decimal? DAY_MONTH { get; set; }

        public decimal? DAY_YEAR { get; set; }

        public decimal? MONTH { get; set; }

        public decimal? WEEK_MONTH { get; set; }

        public decimal? WEEK_YEAR { get; set; }

        public decimal? YEAR { get; set; }

        [Key]
        public DateTime DATA_DATE { get; set; }

        [StringLength(1)]
        public string HOLIDAY_TYPE { get; set; }

        [StringLength(70)]
        public string DESCRIPTION_DATE { get; set; }
    }
}
