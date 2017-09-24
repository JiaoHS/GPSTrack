using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using IBatisNet.Common.Utilities;
using IBatisNet.DataAccess.Configuration;
using Microsoft.Practices.Unity;
using XJTU.Web.Mvc.Ioc;
using XJTU.Web;

namespace XJTU.Web.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer container;
        public static IUnityContainer Container
        {
            get { return container; }
        }

        private string daoConfig = "dao.config";
        private DomDaoManagerBuilder builder;

        private void OnConfigChange(object obj)
        {
            if (builder == null)
                builder = new DomDaoManagerBuilder();
            builder.Configure(daoConfig);
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MvcHandler.DisableMvcResponseHeader = true;

            container = new UnityContainer();
            IControllerFactory controllerFactory = new IocControllerFactory(container);
            RegistManager.RegistAll(container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);


            builder = new DomDaoManagerBuilder();
            builder.ConfigureAndWatch(daoConfig, new ConfigureHandler(OnConfigChange));
        }
    }
}
