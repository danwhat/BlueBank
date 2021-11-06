

using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BlueBankContext _context;

        public TransactionRepository(BlueBankContext context)
    {
            _context = context;
        }

        public Domain.Entities.Transaction Create(Domain.Entities.Transaction transaction)
        {
            
            if (
                transaction?.AccountFrom?.AccountNumber < 1 
                && transaction?.AccountTo?.AccountNumber < 1
                ) return null;

            HandleTransaction(transaction);

            // from null e to null >>>
            //identificar a transação
            //validações
            //montar transação de infra
            //montar log de transação
            //salvar transação
            //salvar log de transação                  

            return transaction;
        }

        public List<Domain.Entities.Transaction> GetByAcc(int accountNumber)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Entities.Transaction> GetByDoc(string Doc)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Entities.Transaction> GetByPeriod(DateTime initial, DateTime final, string Doc)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Entities.Transaction> GetByPeriod(DateTime initial, DateTime final, int accountNumber)
        {
            throw new NotImplementedException();
        }

        private void HandleTransaction(Domain.Entities.Transaction transaction)
        {
            bool validAccountFrom = validAccount(transaction.AccountFrom);
            bool validAccountTo = validAccount(transaction.AccountTo);

            if (validAccountFrom && validAccountTo)
            {
                Transfer(transaction);
            }
            else if (validAccountFrom && validAccountTo == false)
            {
                Withdraw(transaction);
            }
            else if (validAccountFrom == false && validAccountTo)
            {
                Deposit(transaction);
            }

        }

        private void Transfer(Domain.Entities.Transaction transaction)
        {
            var accountFrom = GetAccount.IfActiveById(transaction.AccountFrom.AccountNumber, _context);
            if (accountFrom == null) throw new ServerException(Error.AccountFromNotFound);

            var accountTo = GetAccount.IfActiveById(transaction.AccountFrom.AccountNumber, _context);
            if (accountTo == null) throw new ServerException(Error.AccountToNotFound);

            var accountFromlogs = (ICollection<TransactionLog>)_context
                .TransactionLog
                .Where(log => log.AccountId == accountFrom.Id)
                .ToList();

            decimal accountFromBalance = (GetBalance.Current(accountFromlogs));
            if (accountFromBalance < transaction.Value) throw new ServerException(Error.InsufficientFunds);

            var accountTologs = (ICollection<TransactionLog>)_context
                .TransactionLog
                .Where(log => log.AccountId == accountFrom.Id)
                .ToList();

            decimal accountToBalance = (GetBalance.Current(accountTologs));

            var dbTransaction = new Transaction()
            {
                AccountFrom = accountFrom,
                AccountTo = accountTo,
                Value = transaction.Value,
            };


            var transactionLogFrom = new TransactionLog()
            {
                Value = transaction.Value,
                BalanceAfter = accountFromBalance - transaction.Value,
                Account = accountFrom,
                Transaction = dbTransaction,
            };

            var transactionLogTo = new TransactionLog()
            {
                Value = transaction.Value,
                BalanceAfter = accountToBalance + transaction.Value,
                Account = accountTo,
                Transaction = dbTransaction,
            };

            try
            {
                _context.Transactions.Add(dbTransaction);
                _context.TransactionLog.Add(transactionLogFrom);
                _context.TransactionLog.Add(transactionLogTo);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void Withdraw(Domain.Entities.Transaction transaction)
        {
            var account = GetAccount.IfActiveById(transaction.AccountFrom.AccountNumber, _context);
            if (account == null) throw new ServerException(Error.AccountNotFound);

            var logs = (ICollection<TransactionLog>)_context
                .TransactionLog
                .Where(log => log.AccountId == account.Id)
                .ToList();

            decimal balance = (GetBalance.Current(logs));
            if (balance < transaction.Value) throw new ServerException(Error.InsufficientFunds);
            
            var dbTransaction = new Transaction()
            {
                AccountFrom = account,
                Value = transaction.Value,
            };


            var transactionLog = new TransactionLog()
            {
                Value = transaction.Value,
                BalanceAfter = balance - transaction.Value,
                Account = account,
                Transaction = dbTransaction,
            };

            try
            {
                _context.Transactions.Add(dbTransaction);
                _context.TransactionLog.Add(transactionLog);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void Deposit(Domain.Entities.Transaction transaction)
        {
            if (IsNull(transaction.AccountTo)) throw new ServerException(Error.AccountInvalidId);

            var account = GetAccount.IfActiveById(transaction.AccountTo.AccountNumber, _context);
            if (account == null) throw new ServerException(Error.AccountNotFound);

            var dbTransaction = new Transaction()
            {
                AccountTo = account,
                Value = transaction.Value,
            };
            
            var logs = (ICollection<TransactionLog>)_context
                .TransactionLog
                .Where(log => log.AccountId == account.Id)
                .ToList();

            decimal balance = (GetBalance.Current(logs));

            var transactionLog = new TransactionLog()
            {
                Value = transaction.Value,
                BalanceAfter = balance + transaction.Value,
                Account = account,
                Transaction = dbTransaction,
            };

            try
            {
                _context.Transactions.Add(dbTransaction);
                _context.TransactionLog.Add(transactionLog);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private bool IsNull(Object obj)
        {
            return obj == null;
        }

        private bool validAccount(Domain.Entities.Account account)
        {
            var nullAccount = account?.AccountNumber < 1;
            return nullAccount;
        }
    }
}
