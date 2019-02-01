namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.LOG_ROW")]
    public partial class LOG_ROW
    {

        public LOG_ROW()
        {
            this.ROW_ID = Guid.NewGuid().ToString();
        }

        [StringLength(36)]
        [Column(Order = 0)]
        public string LOG_ID { get; set; }

        [Key]
        [StringLength(36)]
        [Column(Order = 1)]
        public string ROW_ID { get; set; }

        [StringLength(100)]
        public string TABLE_NAME { get; set; }

        /// <summary>
        /// สถานะการกระทำ table ตาม ef
        /// </summary>
        [StringLength(30)]
        public string STATUS { get; set; }

        public LOG_SYSTEM LOG_SYSTEM { get; set; }

        private ICollection<LOG_COLUMN> _Columns = new HashSet<LOG_COLUMN>();
        public virtual ICollection<LOG_COLUMN> Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }

        public LOG_COLUMN NewColumn()
        {
            LOG_COLUMN NewData = new LOG_COLUMN();
            NewData.ROW_ID = this.ROW_ID;
            this.Columns.Add(NewData);

            return NewData;
        
        }

    }
}
