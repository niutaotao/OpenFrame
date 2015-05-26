using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkTest
{
    public class AccountRepository : IAccountRepository, IUnitOfWorkRepository
    {
        #region Field

        private const string _connectionString = @"Data Source=T57649\MSSQLSERVER2012;Initial Catalog=DB_Customer;Integrated Security=True";

        private IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public AccountRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Implement interface IAccountRepository,IUnitOfWorkRepository

        public void Save(BankAccount account)
        {
            _unitOfWork.RegisterChangeded(account, this);
        }

        public void Add(BankAccount account)
        {
            _unitOfWork.RegisterAdded(account, this);
        }

        public void Remove(BankAccount account)
        {
            _unitOfWork.RegisterRemoved(account, this);
        }

        public void PersistNewItem(BaseEntity entityBase)
        {
            BankAccount account = (BankAccount)entityBase;

            string insertAccountSql = string.Format("insert into DT_Account(balance,Id) values({0},{1})", account.Balance, account.Id);

            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(insertAccountSql, sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void PersistUpdatedItem(BaseEntity entityBase)
        {
            BankAccount account = (BankAccount)entityBase;

            string updateAccountSql = string.Format("update DT_Account set balance={0} where Id={1}", account.Balance, account.Id);

            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(updateAccountSql, sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void PersistDeletedItem(BaseEntity entityBase)
        {
            BankAccount account = (BankAccount)entityBase;

            string deleteAccountSql = string.Format("delete from DT_Account where Id={0}", account.Id);

            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(deleteAccountSql, sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        #endregion

        #region Method

        public BankAccount GetAccount(BankAccount account)
        {
            account.Balance = 100;
            return account;
        }

        #endregion
    }
}
