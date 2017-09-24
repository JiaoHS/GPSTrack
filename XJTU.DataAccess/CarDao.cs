using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XJTU.DataAccess.BaseDao;
using XJTU.DataAccess.Contract;
using XJTU.Model;

namespace XJTU.DataAccess
{
    public class CarDao : BaseDao<Car>, ICarDao
    {

        protected override void Init()
        {
            Dao = BeecellRootDao.Instance;
            TableName = "Car.";
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        /// <param name="carid"></param>
        /// <returns></returns>
        public int GetCarRecordCount(string carid)
        {
            var ht = new Hashtable()
            {
               {"sn",carid}
            };
            return Dao.ExecuteQueryForObject<int>("Car.GetCarRecordCount", ht);
        }
    }
}
