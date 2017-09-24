using System.Collections;
using System.Collections.Generic;
using XJTU.Model;

namespace XJTU.DataAccess.Contract
{
    public interface IBaseDao<T> where T : BaseEntity
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">要添加的实体</param>
        /// <returns></returns>
        int Add(T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">要删除实体的Id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">要修改的实体</param>
        /// <returns></returns>
        bool Update(T model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        bool Update(string stateName, Hashtable ht = null);

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="ht">查询条件</param>
        /// <returns></returns>
        IList<T> GetList(string stateName, Hashtable ht = null);

        /// <summary>
        /// 通过Id获取单个实体
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns></returns>
        T Get(int id);

        T Get(string id);

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="ht">查询条件</param>
        /// <returns></returns>
        IList<T> GetList(Hashtable ht = null);



        /// <summary>
        /// 根据条件获取单个实体
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns></returns>
        T Get(Hashtable ht = null);



        /// <summary>
        /// 根据条件获取相关实体
        /// </summary>
        /// <param name="id">stateName</param>
        /// <param name="ht">查询条件</param>
        /// <returns></returns>
        int Count(string stateName, Hashtable ht = null);
    }
}
