namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.LOG_COLUMN")]
    public partial class LOG_COLUMN
    {

        public LOG_COLUMN()
        {
            this.COLUMN_ID = Guid.NewGuid().ToString();
        }

        [Key]
        [StringLength(36)]
        public string COLUMN_ID { get; set; }

        [StringLength(36)]
        [Column(Order = 0)]
        public string ROW_ID { get; set; }

        [Column(Order = 1)]
        [StringLength(50)]
        public string COLUMN_NAME { get; set; }

        public bool? IS_KEY { get; set; }

        public string OLD_VALUE { get; set; }

        public string NEW_VALUE { get; set; }

        public LOG_ROW LOG_TABLE { get; set; }

    }
}
