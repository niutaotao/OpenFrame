using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitOfWorkTest;

namespace UnitTestUnitOfWork
{
    [TestClass]
    public class AccountTest
    {
        private IUnitOfWork unitOfWork;
        private IAccountRepository accountRepository;
        private BankAccountService accountService;

        public AccountTest()
        {
            unitOfWork = new UnitOfWork();
            accountRepository = new AccountRepository(unitOfWork);
            accountService = new BankAccountService(accountRepository, unitOfWork);
        }


        [TestMethod]
        public void Add()
        {
            var accountLeft = new BankAccount() { Balance = 200, Id = 1 };
            var accountRight = new BankAccount() { Balance = 10, Id = 2 };

            accountRepository.Add(accountLeft);
            accountRepository.Add(accountRight);

            unitOfWork.Commit();
        }

        [TestMethod]
        public void Save()
        {
            var accountLeft = new BankAccount() { Balance = 200, Id = 1 };
            var accountRight = new BankAccount() { Balance = 10, Id = 2 };


            accountService.TransferMoney(accountLeft, accountRight, 100);
        }

        [TestMethod]
        public void Remove()
        {
            var accountLeft = new BankAccount() { Balance = 200, Id = 1 };

            accountRepository.Remove(accountLeft);

            unitOfWork.Commit();
        }
    }
}
