using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Infrastructure.Core.Domain;

namespace Tao.Infrastructure.Core.Repository
{
    /// <summary>
    /// 业务对象的仓储的顶级接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> 
        where TEntity : BaseEntity
    {
        /// <summary>
        /// 增加一个业务对象
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// 增加一批业务对象
        /// </summary>
        /// <param name="entityList">业务对象集合</param>
        void AddList(IEnumerable<TEntity> entityList);

        /// <summary>
        /// 删除一个业务对象
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// 删除一批业务对象
        /// </summary>
        /// <param name="entityList">业务对象集合</param>
        void RemoveList(IEnumerable<TEntity> entityList);

        /// <summary>
        /// 获取一个业务对象
        /// </summary>
        /// <param name="keyId">唯一标识</param>
        /// <returns></returns>
        TEntity Get(Guid keyId);    
    
        /// <summary>
        /// 获取一批业务对象
        /// </summary>
        /// <param name="keyIdList">唯一标识集合</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(IEnumerable<Guid> keyIdList);
    }
}
