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
using Tao.Infrastructure.Core.IOC;

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

        public void RegisterAdded(BaseEntity entityBase)
        {
            base.Entry<BaseEntity>(entityBase).State = System.Data.Entity.EntityState.Added;
        }

        public void RegisterChangeded(BaseEntity entityBase)
        {
            base.Entry<BaseEntity>(entityBase).State = System.Data.Entity.EntityState.Modified;
        }

        public void RegisterRemoved(BaseEntity entityBase)
        {
            base.Entry<BaseEntity>(entityBase).State = System.Data.Entity.EntityState.Deleted;
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            IEnumerable<IOnModelCreating> onModelCreatings = BuilderFactory.CreateAllOnModelCreatings();
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
    }
}
