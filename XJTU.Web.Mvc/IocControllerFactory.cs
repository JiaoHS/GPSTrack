using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace XJTU.Web.Mvc
{
    public class IocControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _container;

        public IocControllerFactory(IUnityContainer serviceLocator)
        {
            _container = serviceLocator;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;
            return (IController)_container.Resolve(controllerType);
        }
    }
}
