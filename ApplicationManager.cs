using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TFundSolution.Services
{

    public interface IApplicationManager
    {

        string GetUserName();
        void SetUserName(string username);
        string GetPassword();
        void SetPassword(string password);
        bool GetIsAuthen();
        void SetIsAuthen(bool result);

    }

    public static class SystemFactory
    {
        private static IApplicationManager appMn;
        public static IApplicationManager getSystemContext()
        {
            return appMn;
        }

        public static void setSystemContext(IApplicationManager appManager)
        {
            appMn = appManager;
        }

    }

    public class WebApplicationManager : IApplicationManager
    {

        public SystemRepo sysRepo
        {
            get
            {
                var context = System.Web.HttpContext.Current;
                if ((SystemRepo)context.Session["System"] == null)
                {
                    context.Session["System"] = new SystemRepo();
                }

                return (SystemRepo)context.Session["System"];

            }
            set
            {
                var context = System.Web.HttpContext.Current;
                context.Session["System"] = value;
            }
        }

        public string GetUserName()
        {
            return sysRepo.Username;
        }

        public void SetUserName(string username)
        {
            sysRepo.Username = username;
        }

        public string GetPassword()
        {
            return sysRepo.Password;
        }

        public void SetPassword(string password)
        {
            sysRepo.Password = password;
        }

        public bool GetIsAuthen()
        {
            return sysRepo.IsAuthen;
        }

        public void SetIsAuthen(bool result)
        {
            sysRepo.IsAuthen = result;
        }
    }

    public class ApplicationManager : IApplicationManager
    {

        public SystemRepo sysRepo
        {
            get
            {
                return AppRepo.SysRepo;
            }
        }

        public string GetUserName()
        {
            return sysRepo.Username;
        }

        public void SetUserName(string username)
        {
            sysRepo.Username = username;
        }

        public string GetPassword()
        {
            return sysRepo.Password;
        }

        public void SetPassword(string password)
        {
            sysRepo.Password = password;
        }

        public bool GetIsAuthen()
        {
            return sysRepo.IsAuthen;
        }

        public void SetIsAuthen(bool result)
        {
            sysRepo.IsAuthen = result;
        }
    }

    public class SystemRepo
    {

        public SystemRepo()
        {
            this.IsAuthen = true;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAuthen { get; set; }

    }

    public static class AppRepo
    {
        public static SystemRepo SysRepo;

        static AppRepo()
        {
            SysRepo = new SystemRepo();
            SysRepo.Username = "System";
        }

    }

    //public static class ApplicationManager
    //{

    //    private static string userName = "SYSTEM"; // todo
    //    public static string UserName
    //    {
    //        get
    //        {
    //            return userName;
    //        }
    //        set
    //        {
    //            userName = value;
    //        }
    //    }

    //}


}
