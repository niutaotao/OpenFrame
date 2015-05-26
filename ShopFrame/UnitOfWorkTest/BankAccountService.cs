using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkTest
{
    public class BankAccountService
    {
        #region Field

        private IAccountRepository _accountRepository;
        private IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public BankAccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            this._accountRepository = accountRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Method

        public void TransferMoney(BankAccount from, BankAccount to, decimal balance)
        {
            if (from.Balance >= balance)
            {
                from.Balance = from.Balance - balance;
                to.Balance = to.Balance + balance;

                _accountRepository.Save(from);
                _accountRepository.Save(to);
                _unitOfWork.Commit();
            }
        }

        #endregion
    }
}
