using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkTest
{
    public interface IAccountRepository
    {
        void Save(BankAccount account);
        void Add(BankAccount account);
        void Remove(BankAccount account);
    }
}
