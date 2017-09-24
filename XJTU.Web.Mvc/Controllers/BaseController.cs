using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using XJTU.Model;
using XJTU.Service.Contract;

namespace XJTU.Web.Mvc.Controllers
{
    public class BaseController<T> : Controller where T : BaseEntity
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(BaseController<T>));

        #region MyRegion
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    var merchant = filterContext.RouteData.Values["merchant"];
        //    string merchantName = string.Empty;
        //    if (merchant != null)
        //        merchantName = merchant.ToString();
        //    log.Error(merchantName + ":" + filterContext.Exception.Message + Environment.NewLine + filterContext.Exception.StackTrace);
        //    ////TODO log
        //}

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    #region 商户校验

        //    filterContext.HttpContext.Response.Write("这里可以校验商户,以下是之前的验证商户的方式");
        //    //var merchant = filterContext.RouteData.Values["merchant"] == null ? "" : filterContext.RouteData.Values["merchant"].ToString();

        //    //if (string.IsNullOrEmpty(merchant))
        //    //    throw new HttpException(404, "merchant参数错误");

        //    #endregion
        //} 
        #endregion

        /// <summary>
        /// 业务逻辑基类
        /// </summary>
        protected IBaseService<T> BaseService;

        #region 添加

        public virtual ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public virtual JsonResult Add(T model)
        {
            if (BaseService.Add(model) > 0)
            {
                return Json("");//todo 制定返回规范
            }
            return Json("");
        }
        #endregion

        #region 修改

        public virtual ViewResult Update(int id)
        {
            var model = BaseService.Get(id);

            if (model == null) throw new Exception(string.Format("无法根据Id:{0}找到更多信息", id));

            return View(model);
        }

        [HttpPost]
        public virtual JsonResult Update(T model)
        {
            if (BaseService.Update(model))
            {
                return Json("");//todo 制定返回规范
            }
            return Json("");
        }
        #endregion

        #region 删除
        public virtual JsonResult Delete(int id)
        {
            if (BaseService.Delete(id))
            {
                return Json("成功");
            }
            return Json("失败");
        }
        #endregion

        #region 获取列表
        //public virtual ViewResult Index(int id=0)
        //{
        //    IList<T> list = BaseService.GetList();
        //    return View(list);
        //}
        #endregion

        #region 查看详情
        //public virtual ViewResult Detail(string id)
        //{
        //    int temp = DesCode.DESDeCode(id).ToInt();
        //    var model = BaseService.Get(temp);

        //    if (model == null) throw new Exception(string.Format("无法根据Id:{0}找到实体信息", temp));

        //    return View(model);
        //}
        #endregion
    }
}
