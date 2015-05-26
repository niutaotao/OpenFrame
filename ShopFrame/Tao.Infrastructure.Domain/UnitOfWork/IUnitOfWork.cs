using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tao.Infrastructure.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void RegisterAdded(BaseEntity baseEntity);
        void RegisterChangeded(BaseEntity baseEntity);
        void RegisterRemoved(BaseEntity baseEntity);
        void Commit();
        bool IsDisposed();

        bool HasNoneDisposeTransaction();
    }
}
