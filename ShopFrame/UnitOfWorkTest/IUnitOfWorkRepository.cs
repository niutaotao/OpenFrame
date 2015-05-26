using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkTest
{
    public interface IUnitOfWorkRepository
    {
        void PersistNewItem(BaseEntity baseEntity);
        void PersistUpdatedItem(BaseEntity baseEntity);
        void PersistDeletedItem(BaseEntity baseEntity);
    }
}
