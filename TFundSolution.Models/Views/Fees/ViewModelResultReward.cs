using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Models.Views
{
    public class ViewModelResultReward
    {
        public string FUND_ID { get; set; }

        public string AGENT_ID { get; set; }

        public string MKT { get; set; }

        public DateTime FEE_DATE { get; set; }

        public decimal? Y0_L1 { get; set; }

        public  decimal?  Y1_L1 { get; set; }

        public decimal? Y1_L2 { get; set; }

        public decimal? Y1_L3 { get; set; }

        public decimal? Y2_L1 { get; set; }

        public decimal? Y2_L2 { get; set; }

        public decimal? Y2_L3 { get; set; }

        public decimal? Y3_L1 { get; set; }

        public decimal? Y3_L2 { get; set; }

        public decimal? Y3_L3 { get; set; }
    }

    public class ViewResultRewardComparer : IEqualityComparer<ViewModelResultReward>
    {

        public bool Equals(ViewModelResultReward x, ViewModelResultReward y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.MKT.Equals(y.MKT) && x.FEE_DATE.Equals(y.FEE_DATE);
        }

        public int GetHashCode(ViewModelResultReward obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashMKT = obj.MKT.GetHashCode();

            //Get hash code for the Code field. 
            int hashFEE_DATE = obj.FEE_DATE.GetHashCode();

            //Calculate the hash code for the product. 
            return hashMKT ^ hashFEE_DATE;
        }
    }
}
