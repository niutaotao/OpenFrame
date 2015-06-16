using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tao.Infrastructure.Core.Domain;
using Tao.Infrastructure.Core.UnitOfWork;

namespace Tao.Infrastructure.Core.Repository
{

    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// 获取unit
        /// </summary>
        public IUnitOfWork unitOfWork
        {
            get { return UnitOfWorkFactory.GetUnitOfWork(); }
        }

        /// <summary>
        /// 获取dbset
        /// </summary>
        /// <returns></returns>
        private IDbSet<TEntity> GetSet()
        {
            return this.unitOfWork.CreateSet<TEntity>();
        }

        #region 实现IBaseRepository
        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("仓储Add，无效的实体参数");
            }
            this.GetSet().Add(entity);
        }

        ///// <summary>
        ///// 增加实体集
        ///// </summary>
        ///// <param name="entityList">实体集</param>
        //public void AddList(IEnumerable<TEntity> entityList)
        //{
        //    if (entityList == null || entityList.Count() <= 0)
        //    {
        //        throw new Exception("仓储AddList，无效的实体参数");
        //    }
        //    for (var i = 0; i < entityList.Count(); i++)
        //    {
        //        this.GetSet().Add(entityList.ElementAt(i));
        //    }
        //}

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("仓储Remove，无效的实体参数");
            }
            this.GetSet().Remove(entity);
        }

        ///// <summary>
        ///// 删除实体集
        ///// </summary>
        ///// <param name="entityList"></param>
        //public void RemoveList(IEnumerable<TEntity> entityList)
        //{
        //    if (entityList == null || entityList.Count() <= 0)
        //    {
        //        throw new Exception("仓储RemoveList，无效的实体参数");
        //    }
        //    for (var i = 0; i < entityList.Count(); i++)
        //    {
        //        this.GetSet().Remove(entityList.ElementAt(i));
        //    }
        //}

        /// <summary>
        /// 根据唯一标识获取数据
        /// </summary>
        /// <param name="keyId">唯一标识</param>
        /// <returns></returns>
        TEntity IBaseRepository<TEntity>.Get(Guid keyId)
        {
            var entity = this.GetSet().Find(keyId);
            if (entity == null || (this.unitOfWork as DbContext).Entry<BaseEntity>(entity).State == System.Data.Entity.EntityState.Deleted)
            {
                return default(TEntity);
            }
            else
            {
                return entity;
            }
        }

        /// <summary>
        /// 查询传入keyIdList的数据
        /// </summary>
        /// <param name="keyIdList">keyId集</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(IEnumerable<Guid> keyIdList)
        {
            if (keyIdList == null)
            {
                return new List<TEntity>(0);
            }
            return this.FindByCondition(m => keyIdList.Contains(m.KeyId));
        }
        #endregion

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        protected IEnumerable<TEntity> FindByCondition(Func<TEntity, bool> filter)
        {
            return this.GetSet().Where(filter);
        }

        protected IEnumerable<Object> SelectByCondition(Func<TEntity,Object> selectFilter,Func<Object,bool> whereFilter)
        {
            return this.GetSet().Select(selectFilter).Where(whereFilter);
        }
    }
}
