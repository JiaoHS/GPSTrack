using System;
using System.Collections.Generic;
using System.Linq;
using IBatisNet.DataAccess;
using IBatisNet.DataAccess.DaoSessionHandlers;
using IBatisNet.DataAccess.Interfaces;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;

namespace XJTU.DataAccess.BaseDao
{
    public abstract class SqlMapDao : IDao
    {
        protected IDaoManager DaoSale = null;

        protected IDaoManager DaoRoot = null;

        protected object SynMaster = new object();


        /// <summary>主库连接
        /// 主库连接
        /// </summary>
        /// <returns></returns>
        private IDaoManager MasterDaoManager
        {
            get
            {
                if (DaoSale == null)
                {
                    lock (SynMaster)
                    {
                        if (DaoSale == null)
                        {
                            DaoSale = (DaoManager)DaoManager.GetInstance(MarsterConnectionName);
                        }
                    }
                }
                return DaoSale;
            }
        }

        /// <summary>
        /// Master DAO Session
        /// </summary>
        /// <returns></returns>
        protected SqlMapDaoSession DaoSession
        {
            get
            {
                var dao = MasterDaoManager;
                if(!dao.IsDaoSessionStarted()) dao.OpenConnection();
                SqlMapDaoSession sqlMapDaoSession = (SqlMapDaoSession)dao.LocalDaoSession;
                return sqlMapDaoSession;
            }
        }


        /// <summary>
        /// 链接字符串名称
        /// </summary>
        protected abstract string MarsterConnectionName { get; }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        public virtual IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject)
        {
            using (SqlMapDaoSession session = DaoSession)
            {
                var sqlMapper = session.SqlMap;
                IMappedStatement statement = sqlMapper.GetMappedStatement(statementName);
                if (!sqlMapper.IsSessionStarted)
                {
                    sqlMapper.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, parameterObject, sqlMapper.LocalSession);
                string result = scope.PreparedStatement.PreparedSql;
                return session.SqlMap.QueryForList<T>(statementName, parameterObject);
            }
        }

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        public virtual T ExecuteQueryForObject<T>(string statementName, object parameterObject)
        {
            using (SqlMapDaoSession session = DaoSession)
            {
                return session.SqlMap.QueryForObject<T>(statementName, parameterObject);
            }
        }

        /// <summary>修改后查询对象(chenshi)
        /// 修改后查询对象(chenshi)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        public virtual T UpdateAndExecuteQueryForObject<T>(string statementName, object parameterObject)
        {
            using (SqlMapDaoSession session = DaoSession)
            {
                return session.SqlMap.QueryForObject<T>(statementName, parameterObject);
            }
        }

        /// <summary>
        /// 执行修改
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        public virtual int ExecuteUpdate(string statementName, object parameterObject)
        {
            using (SqlMapDaoSession session = DaoSession)
            {
                return session.SqlMap.Update(statementName, parameterObject);
            }
        }

        /// <summary>
        /// 执行插入
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        public virtual int ExecuteInsert(string statementName, object parameterObject)
        {
            using (SqlMapDaoSession session = DaoSession)
            {
                return (int)session.SqlMap.Insert(statementName, parameterObject);
            }
        }

        public virtual bool ExecuteDelete(string statementName, object parameterObject)
        {
            using (SqlMapDaoSession session = DaoSession)
            {
                return session.SqlMap.Delete(statementName, parameterObject)>0;
            }
        }

        //public virtual PageData<TR> ExecutePager<T, TR>(string pagerStatement, string countStatement, T parameterObject) where T : IPager
        //{
        //    using (var session = DaoSession)
        //    {
        //        var pager = new PageData<TR>();
        //        pager.Items = session.SqlMap.QueryForList<TR>(pagerStatement, parameterObject).ToList();

        //        var count = session.SqlMap.QueryForObject<int>(countStatement, parameterObject);
        //        pager.PageInfo = new PageInfo();
        //        pager.PageInfo.CurrentPage = parameterObject.PageIndex;
        //        pager.PageInfo.PageSize = parameterObject.PageSize;
        //        pager.PageInfo.ToalPage = count % parameterObject.PageSize == 0 ? count / parameterObject.PageSize : count / parameterObject.PageSize + 1;
        //        return pager;
        //    }
        //}

        /// <summary>
        /// 得到运行时ibatis.net动态生成的SQL
        /// </summary>
        /// <param name="sqlMapper"></param>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public virtual string GetRuntimeSql(ISqlMapper sqlMapper, string statementName, object paramObject)
        {
            string result;
            try
            {
                IMappedStatement statement = sqlMapper.GetMappedStatement(statementName);
                if (!sqlMapper.IsSessionStarted)
                {
                    sqlMapper.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, sqlMapper.LocalSession);
                result = scope.PreparedStatement.PreparedSql;
            }
            catch (Exception ex)
            {
                result = "获取SQL语句出现异常:" + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 得到运行时ibatis.net 动态生成的SQL
        /// </summary>
        /// <param name="sqlMapper"></param>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public virtual string GetRuntimeSql(string statementName, object paramObject)
        {
            string result;
            try
            {
                using (SqlMapDaoSession session = DaoSession)
                {
                    var sqlMapper = session.SqlMap;
                    IMappedStatement statement = sqlMapper.GetMappedStatement(statementName);
                    if (!sqlMapper.IsSessionStarted)
                    {
                        sqlMapper.OpenConnection();
                    }
                    RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, sqlMapper.LocalSession);
                    result = scope.PreparedStatement.PreparedSql;
                }

            }
            catch (Exception ex)
            {
                result = "获取SQL语句出现异常:" + ex.Message;
            }
            return result;
        }
    }
}
