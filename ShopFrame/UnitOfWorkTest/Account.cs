using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkTest
{
    public class BankAccount : BaseEntity
    {
        #region Field

        public int Id { get; set; }

        public decimal Balance { get; set; }

        #endregion

        #region operator +

        public static BankAccount operator +(BankAccount accountLeft, BankAccount accountRight)
        {
            BankAccount account = new BankAccount();

            account.Balance = accountLeft.Balance + accountRight.Balance;

            return account;
        }

        #endregion
    }
}
