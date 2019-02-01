namespace TFundSolution.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIS.LOG_SYSTEM")]
    public partial class LOG_SYSTEM
    {

        public LOG_SYSTEM()
        {
            this.LOG_ID = Guid.NewGuid().ToString();
            this.ACTION_DATE = DateTime.Now; 
        }

        [Key]
        [StringLength(36)]
        public string LOG_ID { get; set; }

        [StringLength(50)]
        public string USERNAME { get; set; }

        public DateTime? ACTION_DATE { get; set; }

        public DateTime? SUCCESS_DATE { get; set; }

        public DateTime? ERR_DATE { get; set; }

        public EnumActionType? ACTION_TYPE { get; set; }

        public string ERR_DESC { get; set; }

        public string NOTE { get; set; }

        private ICollection<LOG_ROW> _Rows = new HashSet<LOG_ROW>();
        public virtual ICollection<LOG_ROW> Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }

        public LOG_ROW NewRow()
        {
            LOG_ROW newData = new LOG_ROW();
            newData.LOG_ID = this.LOG_ID;
            this.Rows.Add(newData);

            return newData;
        }

    }
}
