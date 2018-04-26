using System.Web.Http;
using Dashing.Wellcom.Dam.Models;
using Unity;
using Unity.WebApi;
using Unity.Injection;
using System.Configuration;

namespace Dashing.Wellcom.Dam
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterType<HMACUtils>();
            container.RegisterType<WellcomProductsService>();
        }
    }
}