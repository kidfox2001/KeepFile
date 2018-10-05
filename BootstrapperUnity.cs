using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Unity.Injection;
using TestModel;
using TestService;
using TestService;

namespace Testmvc
{
    public class BootstrapperUnity
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here  
            //This is the important line to edit  

            container.RegisterType<IUnitOfWork, UnitOfWorkTest>();
            container.RegisterType<TestManager>();
            container.RegisterType<ILog, LogFile>();

            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}