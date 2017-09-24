using System.Collections;
using System.Collections.Generic;
using System;
using XJTU.Model;
using XJTU.DataAccess.BaseDao;
using XJTU.DataAccess.Contract;

namespace XJTU.DataAccess
{
    public abstract class BaseDao<T> : IBaseDao<T> where T : BaseEntity
    {
        protected SqlMapDao Dao;
        protected string TableName;

        protected BaseDao()
        {
            Init();
        }

        protected abstract void Init();

        public virtual int Add(T model)
        {
            return Dao.ExecuteInsert(TableName + "insert", model);
        }

        public virtual bool Delete(int id)
        {
            return Dao.ExecuteDelete(TableName + "delete", id);
        }

        public virtual bool Update(T model)
        {
            return Dao.ExecuteUpdate(TableName + "update", model) > 0;
        }

        public virtual T Get(int id)
        {
            return Dao.ExecuteQueryForObject<T>(TableName + "select", id);
        }

        public virtual T Get(string id)
        {
            return Dao.ExecuteQueryForObject<T>(TableName + "select", id);
        }

        public virtual IList<T> GetList(Hashtable ht = null)
        {
            return Dao.ExecuteQueryForList<T>(TableName + "list", ht);
        }



        public virtual T Get(Hashtable ht = null)
        {
            return Dao.ExecuteQueryForObject<T>(TableName + "singleselect", ht);
        }



        public virtual int Count(string stateName, Hashtable ht = null)
        {
            return Dao.ExecuteQueryForObject<int>(stateName, ht);
        }

        public bool Update(string stateName, Hashtable ht = null)
        {
            return Dao.ExecuteUpdate(stateName, ht) > 0;
        }

        public IList<T> GetList(string stateName, Hashtable ht = null)
        {
            return Dao.ExecuteQueryForList<T>(stateName, ht);
        }
        //todo 后续加上分页方法

        public virtual string GetRuntimeSql(string stateName, Hashtable ht = null)
        {
            return Dao.GetRuntimeSql(stateName, ht);
        }
    }
}
