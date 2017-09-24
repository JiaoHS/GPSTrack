using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using XJTU.DataAccess.Contract;
using XJTU.DataAccess;

namespace XJTU.Web.Mvc.Ioc
{
    public class DaoRegister
    {
        private IUnityContainer container;

        public DaoRegister(IUnityContainer container)
        {
            this.container = container;
        }

        public void Regist()
        {
            //container.RegisterType<ITestDao, TestDao>();
            container.RegisterType<ICarDao, CarDao>();
        }
    }
}
