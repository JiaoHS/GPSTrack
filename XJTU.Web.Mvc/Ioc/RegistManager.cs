using Microsoft.Practices.Unity;
using XJTU.Web.Mvc.Ioc;

namespace XJTU.Web.Mvc.Ioc
{
    public class RegistManager
    {
        public static void RegistAll(IUnityContainer container)
        {
            new ControllerRegister(container).Regist();
            new ServiceRegister(container).Regist();
            new DaoRegister(container).Regist();
        }
    }
}
