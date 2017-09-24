using Microsoft.Practices.Unity;
using XJTU.Web.Mvc.Controllers;

namespace XJTU.Web.Mvc.Ioc
{
    public class ControllerRegister
    {
        private IUnityContainer container;

        public ControllerRegister(IUnityContainer container)
        {
            this.container = container;
        }

        public void Regist()
        {
            container.RegisterType<HomeController>();
        }
    }
}
