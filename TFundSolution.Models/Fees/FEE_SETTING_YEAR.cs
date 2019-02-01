using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models
{
    [Table("CIS.FEE_SETTING_YEAR")]
    public class FEE_SETTING_YEAR
    {
        public FEE_SETTING_YEAR()
        {
            this.FSY_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;
      
        }

        /// <summary>
        /// PK
        /// </summary>
        [Key]
        [StringLength(36)]
        public string FSY_ID { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [StringLength(36)]
        public string FES_ID { get; set; }
        public virtual FEE_SETTING_TBANK SettingOwner { get; set; }

        [StringLength(2)]
        public string YEAR_ORDER { get; set; }

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

        [NotMapped]
        public bool IsYearZero
        {
            get
            {
                if (this.YEAR_ORDER == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.YEAR_ORDER = "0";
                }
            }
        }

        [NotMapped]
        public string DisplayYear
        {
            get
            {
                if (this.YEAR_ORDER == "0")
                {
                    return "ปียกมา";
                }
                else
                {
                    return this.YEAR_ORDER;
                }
            }
        }

        /// <summary>
        /// เอาไว้ใช้เรียงตอน caculate เพราะอยากให้ปีตั้งต้น อยู่มากสุด
        /// </summary>
        [NotMapped]
        public string YearForResult
        {
            get
            {
                if (this.YEAR_ORDER == "0")
                {
                    return "B";
                }
                else
                {
                    return this.YEAR_ORDER;
                }
            }
        }

        #region Relation

        private ICollection<FEE_SETTING_ONLEVEL> _OnLevelSettings = new HashSet<FEE_SETTING_ONLEVEL>();
        public virtual ICollection<FEE_SETTING_ONLEVEL> SettingOnLevels  
        {
            get { return _OnLevelSettings; }
            set { _OnLevelSettings = value; }
        }

        #endregion


        #region Method

        public FEE_SETTING_ONLEVEL NewLevelSetting()
        {
            FEE_SETTING_ONLEVEL NewData = new FEE_SETTING_ONLEVEL();
            NewData.FSY_ID = this.FSY_ID;
            NewData.DataStatus = EnumDataStatus.NewData;
            NewData.LEVEL = SettingOnLevels.Count(q => q.DataStatus != EnumDataStatus.DeleteData) + 1;

            this.SettingOnLevels.Add(NewData);

            return NewData;
        }

        #endregion


    }

    [Table("CIS.FEE_SETTING_YEAR_REAL")]
    public class FEE_SETTING_YEAR_REAL
    {
        public FEE_SETTING_YEAR_REAL()
        {
            this.FSY_ID = Guid.NewGuid().ToString();
            this.DataStatus = EnumDataStatus.NotChange;

        }

        [Key]
        [StringLength(36)]
        public string FSY_ID { get; set; }

        [StringLength(36)]
        public string FES_ID { get; set; }
        public virtual FEE_SETTING_TBANK SettingOwner { get; set; }

        [StringLength(2)]
        public string YEAR_ORDER { get; set; }

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

        [NotMapped]
        public bool IsYearZero
        {
            get
            {
                if (this.YEAR_ORDER == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [NotMapped]
        public string DisplayYear
        {
            get
            {
                if (this.YEAR_ORDER == "0")
                {
                    return "ปียกมา";
                }
                else
                {
                    return this.YEAR_ORDER;
                }
            }
        }

        /// <summary>
        /// เอาไว้ใช้เรียงตอน caculate เพราะอยากให้ปีตั้งต้น อยู่มากสุด
        /// </summary>
        [NotMapped]
        public string OrderYearForResult
        {
            get
            {
                if (this.YEAR_ORDER == "0")
                {
                    return "B";
                }
                else
                {
                    return this.YEAR_ORDER;
                }
            }
        }

        #region Relation

        private ICollection<FEE_SETTING_ONLEVEL> _OnLevelSettings = new HashSet<FEE_SETTING_ONLEVEL>();
        public virtual ICollection<FEE_SETTING_ONLEVEL> SettingOnLevels
        {
            get { return _OnLevelSettings; }
            set { _OnLevelSettings = value; }
        }

        #endregion


        #region Method

        public FEE_SETTING_ONLEVEL NewLevelSetting()
        {
            FEE_SETTING_ONLEVEL NewData = new FEE_SETTING_ONLEVEL();
            NewData.FSY_ID = this.FSY_ID;
            NewData.DataStatus = EnumDataStatus.NewData;
            NewData.LEVEL = SettingOnLevels.Count(q => q.DataStatus != EnumDataStatus.DeleteData) + 1;

            this.SettingOnLevels.Add(NewData);

            return NewData;
        }

        #endregion


    }
}
