using Microsoft.Practices.Unity;
using XJTU.Service;
using XJTU.Service.Contract;

namespace XJTU.Web.Mvc.Ioc
{
    public class ServiceRegister
    {
        private IUnityContainer container;

        public ServiceRegister(IUnityContainer container)
        {
            this.container = container;
        }

        public void Regist()
        {
            //container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICarService, CarService>();
        }
    }
}
