using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitOfWorkTest
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private Dictionary<BaseEntity, IUnitOfWorkRepository> addedEntities;
        private Dictionary<BaseEntity, IUnitOfWorkRepository> changededEntities;
        private Dictionary<BaseEntity, IUnitOfWorkRepository> removedEntities;

        #endregion

        #region Constructor

        public UnitOfWork()
        {
            addedEntities = new Dictionary<BaseEntity, IUnitOfWorkRepository>();
            changededEntities = new Dictionary<BaseEntity, IUnitOfWorkRepository>();
            removedEntities = new Dictionary<BaseEntity, IUnitOfWorkRepository>();
        }

        #endregion

        #region Implement IUnitOfWork

        public void RegisterAdded(BaseEntity entityBase, IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.addedEntities.Add(entityBase, unitOfWorkRepository);
        }

        public void RegisterChangeded(BaseEntity entityBase, IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.changededEntities.Add(entityBase, unitOfWorkRepository);
        }

        public void RegisterRemoved(BaseEntity entityBase, IUnitOfWorkRepository unitOfWorkRepository)
        {
            this.removedEntities.Add(entityBase, unitOfWorkRepository);
        }

        public void Commit()
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                foreach (var entity in addedEntities.Keys)
                {
                    addedEntities[entity].PersistNewItem(entity);
                }

                foreach (var entity in changededEntities.Keys)
                {
                    changededEntities[entity].PersistUpdatedItem(entity);
                }

                foreach (var entity in removedEntities.Keys)
                {
                    removedEntities[entity].PersistDeletedItem(entity);
                }

                transactionScope.Complete();
            }
        }

        #endregion
    }
}
