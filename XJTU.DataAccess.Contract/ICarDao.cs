using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XJTU.Model;

namespace XJTU.DataAccess.Contract
{
    public interface ICarDao : IBaseDao<Car>
    {
        /// <summary>
        /// 获取车记录
        /// </summary>
        /// <param name="carid">车ID</param>
        /// <returns></returns>
        int GetCarRecordCount(string carid);
    }
}
