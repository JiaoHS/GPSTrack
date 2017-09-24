using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using XJTU.Model;
using XJTU.Service.Contract;

namespace XJTU.Web.Mvc.Controllers
{
    public class HomeController : BaseController<Car>
    {
        private ICarService _carService;
        public HomeController(ICarService carService)
        {
            _carService = carService;
        }


        public ActionResult HomeIndex()
        {
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HomeList(string sn_id, int pageIndex, int pageSize, DateTime startTime, DateTime endTime)
        {
            var ht = new Hashtable() { { "sn", sn_id }, { "startIndex", (pageIndex - 1) * pageSize }, { "pageSize", pageSize }, { "startTime", startTime }, { "endTime", endTime } };
            var list = _carService.GetList(ht);
            ht = new Hashtable() { { "sn", sn_id } };
            var count = _carService.GetCarRecordCount(sn_id);
            var json = new { Data = list, totalCount = count };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
