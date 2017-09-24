using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XJTU.DataAccess.BaseDao;

namespace XJTU.DataAccess.BaseDao
{
   public class BeecellRootDao : SqlMapDao
    {
       private static BeecellRootDao _rootBaseDao = new BeecellRootDao();
        protected override string MarsterConnectionName
        {
            get { return "Root"; }
        }

        /// <summary>返回实例
        /// 返回实例
        /// </summary>
        public static BeecellRootDao Instance
        {
            get
            {
                return _rootBaseDao;
            }
        }
    }
}
