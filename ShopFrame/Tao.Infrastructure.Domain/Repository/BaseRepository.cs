using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tao.Infrastructure.Core.Repository
{

    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> 
        where TEntity : BaseEntity
    {
        DbContext context = new DbContext("testConnection");
        public void Add(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public BaseEntity Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
