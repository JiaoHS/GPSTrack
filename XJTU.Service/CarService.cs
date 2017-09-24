using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XJTU.DataAccess.Contract;
using XJTU.Model;
using XJTU.Service;
using XJTU.Service.Contract;

namespace XJTU.Service
{
    public class CarService : BaseService<Car>, ICarService
    {
        public CarService(ICarDao dao)
        {
            this.Instance = dao;
        }
        /// <summary>
        /// 获取车记录
        /// </summary>
        /// <param name="carid"></param>
        /// <returns></returns>
        public int GetCarRecordCount(string carid)
        {
            return ((ICarDao)Instance).GetCarRecordCount(carid);
        }
    }
}
