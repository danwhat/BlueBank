using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Infrastructure.Shared;

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

            return transaction;
        }

        public List<Domain.Entities.Transaction> GetByDoc(string Doc)
        {
            try
            {
                DateTime limitDate = DateTime.Now.AddDays(-30);

                var dbAccount = GetAccount.IfActiveByOwnerDoc(Doc, _context);
                var dbAccountTransaction = GetAccountTransactions(limitDate, DateTime.Now, dbAccount);

                return dbAccountTransaction
                    .Select(dbTransaction => BuildInstance.TransactionEntity(dbTransaction)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Domain.Entities.Transaction> GetByAcc(int accountNumber)
        {
            try
            {
                DateTime limitDate = DateTime.Now.AddDays(-30);

                var dbAccount = GetAccount.IfActiveById(accountNumber, _context);
                var dbAccountTransaction = GetAccountTransactions(limitDate, DateTime.Now, dbAccount);

                return dbAccountTransaction
                    .Select(dbTransaction => BuildInstance.TransactionEntity(dbTransaction)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Domain.Entities.Transaction> GetByPeriod(DateTime initial, DateTime final, string Doc)
        {
            try
            {
                Validate.TransactionDate(initial, final);

                var dbAccount = GetAccount.IfActiveByOwnerDoc(Doc, _context);
                var dbAccountTransaction = GetAccountTransactions(initial, final, dbAccount);

                return dbAccountTransaction
                    .Select(dbTransaction => BuildInstance.TransactionEntity(dbTransaction)).ToList();
            }
            catch (ServerException e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(e.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Domain.Entities.Transaction> GetByPeriod(DateTime initial, DateTime final, int accountNumber)
        {
            try
            {
                Validate.TransactionDate(initial, final);

                var dbAccount = GetAccount.IfActiveById(accountNumber, _context);
                var dbAccountTransaction = GetAccountTransactions(initial, final, dbAccount);

                return dbAccountTransaction
                    .Select(dbTransaction => BuildInstance.TransactionEntity(dbTransaction)).ToList();
            }
            catch (ServerException e)
            {
                Debug.WriteLine(e.Message);
                throw new ServerException(e.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Helpers

        private void HandleTransaction(Domain.Entities.Transaction transaction)
        {
            if (transaction.Value <= 0) return;

            bool validAccountFrom = Validate.Account(transaction.AccountFrom);
            bool validAccountTo = Validate.Account(transaction.AccountTo);

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

            var accountTo = GetAccount.IfActiveById(transaction.AccountTo.AccountNumber, _context);
            if (accountTo == null) throw new ServerException(Error.AccountToNotFound);

            var accountFromLogs = (ICollection<TransactionLog>)_context
                .TransactionLog
                .Where(log => log.AccountId == accountFrom.Id)
                .ToList();

            decimal accountFromBalance = (GetBalance.Current(accountFromLogs));
            if (accountFromBalance < transaction.Value) throw new ServerException(Error.InsufficientFunds);

            var accountToLogs = (ICollection<TransactionLog>)_context
                .TransactionLog
                .Where(log => log.AccountId == accountTo.Id)
                .ToList();

            decimal accountToBalance = (GetBalance.Current(accountToLogs));

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
            if (Validate.IsNull(transaction.AccountTo)) throw new ServerException(Error.AccountInvalidId);

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

        private List<Transaction> GetAccountTransactions(DateTime initial, DateTime final, Account dbAccount)
        {
            return _context.Transactions
            .Where(transaction =>
                        (transaction.AccountFrom.Id == dbAccount.Id || transaction.AccountTo.Id == dbAccount.Id)
                        && (transaction.CreatedAt >= initial && transaction.CreatedAt <= final))
                    .OrderBy(transaction => transaction.CreatedAt).ToList();
        }

        #endregion Helpers
    }
}