using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Tao.Infrastructure.Core.UnitOfWork
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        private bool isDisposed = false;
        private TransactionScope TransactionScope = null;

        /// <summary>
        /// 构造，指定链接字符串
        /// </summary>
        public UnitOfWork()
            : base("DbConnectionString")
        {

        }

        /// <summary>
        /// 重写savechange，记录日志
        /// </summary>
        /// <returns></returns>
        public new int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                throw new Exception("验证字段失败", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("保存异常", ex);
            }
        }

        public bool IsDisposed()
        {
            return this.isDisposed;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.TransactionScope != null)
            {
                this.TransactionScope.Dispose();
                this.TransactionScope = null;
            }
            base.Dispose(disposing);
            this.isDisposed = true;
        }

        public void Commit()
        {
            try
            {
                this.SaveChanges();
            }
            catch (Exception e)
            {
                if (this.TransactionScope != null)
                {
                    this.TransactionScope.Complete();
                    this.TransactionScope.Dispose();
                }
                this.Dispose();
                //throw new Exception("严重问题:提交事务TransactionScope出错");
                throw e;
            }
            finally
            {
                if (this.TransactionScope != null)
                {
                    this.TransactionScope.Complete();
                    this.Dispose();
                }
                else
                {
                    // 如果调用Commit但TransactionScope已经不存在，抛出异常。
                    //this.RollbackChanges();
                    //throw e;
                    //throw new Exception("严重问题:调用Commit时TransactionScope已经不存在.");
                }
            }
        }

        public void OpenTransaction()
        {
            if (this.TransactionScope == null) //第一次打开事务
            {
                TransactionOptions tso = new TransactionOptions();
                tso.IsolationLevel = IsolationLevel.ReadCommitted;
                tso.Timeout = new TimeSpan(0, 3, 0);
                this.TransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, tso);
            }
        }

        #region DbContext Overrides
        /// <summary>
        /// ModelCreating加载所有所有实现IOnModelCreating的配置
        /// 每个模块都有自己的映射配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            IEnumerable<IOnModelCreating> onModelCreatings = UnitOfWorkFactory.CreateAllOnModelCreatings();
            foreach (IOnModelCreating item in onModelCreatings)
            {
                item.OnModelCreating(modelBuilder);
            }
        }
        #endregion

        public bool HasNoneDisposeTransaction()
        {
            return this.TransactionScope != null;
        }

        public IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            base.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Unchanged;
        }
    }
}
