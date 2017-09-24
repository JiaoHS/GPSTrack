using System;
using System.Collections;
using System.Collections.Generic;
using XJTU.DataAccess.Contract;
using XJTU.Model;
using XJTU.Service.Contract;

namespace XJTU.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected IBaseDao<T> Instance;

        public int Add(T model)
        {
            return Instance.Add(model);
        }

        public bool Delete(int id)
        {
            return Instance.Delete(id);
        }

        public bool Update(T model)
        {
            return Instance.Update(model);
        }

        public bool Update(string stateName, Hashtable ht = null)
        {
            return Instance.Update(stateName, ht);
        }

        public T Get(int id)
        {
            return Instance.Get(id);
        }

        public T Get(string id)
        {
            return Instance.Get(id);
        }

        public IList<T> GetList(System.Collections.Hashtable ht = null)
        {
            return Instance.GetList(ht);
        }


        public T Get(System.Collections.Hashtable ht = null)
        {
            return Instance.Get(ht);
        }

        public int Count(string stateName, Hashtable ht = null)
        {
            return Instance.Count(stateName, ht);
        }


        public IList<T> GetList(string stateName, Hashtable ht = null)
        {
            return Instance.GetList(stateName, ht);
        }
    }
}
