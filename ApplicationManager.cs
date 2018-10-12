using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Services
{

    public static class ApplicationManager 
    {
        private static string userName = "TEST"; // todo
        public static string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
    }

   
}
