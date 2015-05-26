using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tao.Infrastructure.Core.Repository
{
    public interface IBaseRepository<TEntity> 
        where TEntity : BaseEntity
    {
        /// <summary>
        /// 通过Repository增加对象
        /// </summary>
        /// <param name="entity">业务对象</param>
        void Add(BaseEntity entity);

        /// <summary>
        /// 通过Repository删除对象
        /// </summary>
        /// <param name="entity">业务对象</param>
        void Remove(BaseEntity entity);

        /// <summary>
        /// 通过Repository修改对象
        /// </summary>
        /// <param name="entity">业务对象</param>
        void Update(BaseEntity entity);

        /// <summary>
        /// 通过id查找对象
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        BaseEntity Get(Guid id);
    }
}
