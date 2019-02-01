using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models
{
    [Table("CIS.AGENT_BF_VALUE")]
    public class AgentBfValue
    {

        public AgentBfValue()
        {
            this.ABF_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
        }

        [Key]
        [StringLength(36)]
        public string ABF_ID { get; set; }

        [Index]
        [ForeignKey("FundOwner")]
        [StringLength(30)]
        [Column("FUND", Order = 0)]
        public string FUND_ID { get; set; }
        public virtual Fund FundOwner { get; set; }

        [Index]
        [ForeignKey("AgentOwner")]
        [StringLength(9)]
        [Column("AGENT", Order = 1)]
        public string AGENT_ID { get; set; }
        public virtual Agent AgentOwner { get; set; }

        public decimal BF_VALUE { get; set; }

        [StringLength(20)]
        public string UPDATE_BY { get; set; }

        private DateTime? _UPDATE_DATE;
        public DateTime? UPDATE_DATE
        {
            get
            {
                return _UPDATE_DATE ?? DateTime.Now;
            }
            set
            {
                _UPDATE_DATE = value;
            }
        }


        [NotMapped]
        public EnumDataStatus? DataStatus { get; set; }


    }
}
