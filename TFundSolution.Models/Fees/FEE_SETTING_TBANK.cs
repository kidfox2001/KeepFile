using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models
{

    public interface IFeeSettingTbank
    {

        /// <summary>
        /// เรียกข้อมูลการตั้งค่า onpay ที่อยู่ระหว่างวันที่ๆ ส่งเข้ามา
        /// </summary>
        /// <param name="dateFee"></param>
        /// <returns></returns>
        FEE_SETTING_ONPAY GetOngoOnPaySettingByCondition(DateTime dateFee);

        FEE_SETTING_YEAR GetOngoOnYearSettingByYear(string year);

         List<FEE_SETTING_UPFRONT_ONLEVEL> GetUpFrontOnLevel();

         List<FEE_SETTING_UPFRONT_TWEALTH> GetUpFrontTWealth();

         FEE_SETTING_UPFRONT_ONPAY GetUpFrontOnPaySettingByCondition(DateTime dateFee);

    }

    public class FEE_SETTING_TBANK : FEE_SETTING, IFeeSettingTbank
    {

       #region Relation

        private ICollection<FEE_SETTING_ONPAY> _SettingOngoOnpays = new HashSet<FEE_SETTING_ONPAY>();
        public virtual ICollection<FEE_SETTING_ONPAY> SettingOngoOnpays
        {
            get { return _SettingOngoOnpays; }
            set { _SettingOngoOnpays = value; }
        }

        private ICollection<FEE_SETTING_YEAR> _SettingOngoYears = new HashSet<FEE_SETTING_YEAR>();
        public virtual ICollection<FEE_SETTING_YEAR> SettingOngoYears
        {
            get { return _SettingOngoYears; }
            set { _SettingOngoYears = value; }
        }

        private ICollection<FEE_SETTING_UPFRONT_TWEALTH> _SettingUpFrontTWealth = new HashSet<FEE_SETTING_UPFRONT_TWEALTH>();
        public virtual ICollection<FEE_SETTING_UPFRONT_TWEALTH> SettingUpFrontTWealth
        {
            get { return _SettingUpFrontTWealth; }
            set { _SettingUpFrontTWealth = value; }
        }

        private ICollection<FEE_SETTING_UPFRONT_ONLEVEL> _SettingUpFrontLevels = new HashSet<FEE_SETTING_UPFRONT_ONLEVEL>();
        public virtual ICollection<FEE_SETTING_UPFRONT_ONLEVEL> SettingUpFrontLevels
        {
            get { return _SettingUpFrontLevels; }
            set { _SettingUpFrontLevels = value; }
        }


        private ICollection<FEE_SETTING_UPFRONT_ONPAY> _SettingUpFrontOnPays = new HashSet<FEE_SETTING_UPFRONT_ONPAY>();
        public virtual ICollection<FEE_SETTING_UPFRONT_ONPAY> SettingUpFrontOnPays
        {
            get { return _SettingUpFrontOnPays; }
            set { _SettingUpFrontOnPays = value; }
        }

        #endregion

        #region Method

        public FEE_SETTING_ONPAY NewOngoOnPaySetting()
        {
            FEE_SETTING_ONPAY NewData = new FEE_SETTING_ONPAY();
            NewData.FES_ID = this.FES_ID;
            NewData.DataStatus = EnumDataStatus.NewData;

            this.SettingOngoOnpays.Add(NewData);

            return NewData;
        }

        /// <summary>
        /// เมื่อมีการ new year จะมีการ add level อัตโนมัติ
        /// </summary>
        /// <returns></returns>
        public FEE_SETTING_YEAR NewOngoYearSetting()
        {
            FEE_SETTING_YEAR NewData = new FEE_SETTING_YEAR();
            NewData.FES_ID = this.FES_ID;
            NewData.DataStatus = EnumDataStatus.NewData;
            NewData.YEAR_ORDER = (SettingOngoYears.Where(q => !q.IsYearZero).ToList().Count + 1).ToString();
            NewData.NewLevelSetting();

            this.SettingOngoYears.Add(NewData);

            return NewData;
        }


        public FEE_SETTING_UPFRONT_ONLEVEL NewUpFrontLevel()
        {
            FEE_SETTING_UPFRONT_ONLEVEL NewData = new FEE_SETTING_UPFRONT_ONLEVEL();
            NewData.FES_ID = this.FES_ID;
            NewData.DataStatus = EnumDataStatus.NewData;
            NewData.SettingOwner = this;
       
            this.SettingUpFrontLevels.Add(NewData);

            return NewData;
        }

        public FEE_SETTING_UPFRONT_ONPAY NewUpFrontOnPay()
        {
            FEE_SETTING_UPFRONT_ONPAY NewData = new FEE_SETTING_UPFRONT_ONPAY();
            NewData.FES_ID = this.FES_ID;
            NewData.DataStatus = EnumDataStatus.NewData;
            NewData.SettingOwner = this;
       
            this.SettingUpFrontOnPays.Add(NewData);

            return NewData;
        }

        public FEE_SETTING_UPFRONT_TWEALTH NewUpFrontTWealth()
        {
            FEE_SETTING_UPFRONT_TWEALTH NewData = new FEE_SETTING_UPFRONT_TWEALTH();
            NewData.FES_ID = this.FES_ID;
            NewData.DataStatus = EnumDataStatus.NewData;
            NewData.SettingOwner = this;

            this.SettingUpFrontTWealth.Add(NewData);

            return NewData;
        }

        public FEE_SETTING_ONPAY GetOngoOnPaySettingByCondition(DateTime dateFee)
        {
            return this.SettingOngoOnpays.SingleOrDefault(m => m.START_DATE <= dateFee & dateFee <= m.DateEndCalculated);
        }


        public FEE_SETTING_YEAR GetOngoOnYearSettingByYear(string year)
        {
            return this.SettingOngoYears.SingleOrDefault(m => m.YearForResult.ToUpper() == year.ToUpper());
        }

        public List<FEE_SETTING_UPFRONT_ONLEVEL> GetUpFrontOnLevel()
        {
            return this.SettingUpFrontLevels.ToList();
        }

        public List<FEE_SETTING_UPFRONT_TWEALTH> GetUpFrontTWealth()
        {
            return this.SettingUpFrontTWealth.ToList();
        }

        public FEE_SETTING_UPFRONT_ONPAY GetUpFrontOnPaySettingByCondition(DateTime dateFee)
        {
            return this.SettingUpFrontOnPays.SingleOrDefault(m => m.START_DATE <= dateFee & dateFee <= m.DateEndCalculated);
        }


        #endregion

   }

   

}
