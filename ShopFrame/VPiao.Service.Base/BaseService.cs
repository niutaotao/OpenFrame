using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Infrastructure.Core.UnitOfWork;
using Tao.Repository.User;

namespace Tao.Service.Base
{
    /// <summary>
    /// 服务的抽象基类，提供工作者单元，控制service层需要实现该基类服务
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        /// <summary>
        /// 构造
        /// </summary>
        protected BaseService()
        {

        }

        /// <summary>
        /// 工作单元
        /// </summary>
        private IUnitOfWork UnitOfWork
        {
            get
            {
                return UnitOfWorkFactory.GetUnitOfWork();
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        protected void SaveChanges()
        {
            this.UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 打开工作单元事务处理
        /// </summary>
        /// <returns></returns>
        protected IUnitOfWork OpenUnitOfWork()
        {
            return this.UnitOfWork;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        protected IUnitOfWork OpenTransaction()
        {
            if (this.UnitOfWork == null)
                this.OpenUnitOfWork();
            this.UnitOfWork.OpenTransaction();
            return this.UnitOfWork;
        }

        /// <summary>
        /// dispose
        /// </summary>
        protected void Dispose()
        {
            if (this.UnitOfWork != null)
                this.UnitOfWork.Dispose();
        }

        /// <summary>
        /// 提交工作单元
        /// </summary>
        protected void Commit()
        {
            this.UnitOfWork.Commit();
        }
    }
}