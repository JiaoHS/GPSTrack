using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XJTU.Model;
using XJTU.Service.Contract;

namespace XJTU.Service.Contract
{
    public interface ICarService : IBaseService<Car>
    {
        int GetCarRecordCount(string carid);
    }
}
