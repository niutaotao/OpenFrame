using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkTest
{
    public interface IUnitOfWork
    {
        void RegisterAdded(BaseEntity baseEntity, IUnitOfWorkRepository unitOfWorkRepository);
        void RegisterChangeded(BaseEntity baseEntity, IUnitOfWorkRepository unitOfWorkRepository);
        void RegisterRemoved(BaseEntity baseEntity, IUnitOfWorkRepository unitOfWorkRepository);
        void Commit();
    }
}
