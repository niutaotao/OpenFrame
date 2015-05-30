using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tao.Infrastructure.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        bool IsDisposed();

        bool HasNoneDisposeTransaction();

        void OpenTransaction();

        int SaveChanges();

        IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        void Attach<TEntity>(TEntity item) where TEntity : class;
    }
}
