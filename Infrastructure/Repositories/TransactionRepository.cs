

using Domain.Core.Interfaces;
using Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

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
            
            if (transaction?.AccountFrom?.AccountNumber < 1 && transaction?.AccountTo?.AccountNumber < 1) return null;
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
            bool nullAccountFrom = IsNull(transaction.AccountFrom);

            if (!nullAccountFrom) nullAccountFrom = transaction.AccountFrom.AccountNumber < 1;            
            
            if (transaction.AccountFrom?.AccountNumber > 0 && transaction.AccountTo?.AccountNumber > 0)
            {
                //Chamar transferencia
            }
          
            else if (nullAccountFrom && transaction.AccountTo?.AccountNumber > 0)
            {
                Deposit(transaction);
            }

            else if (transaction.AccountFrom?.AccountNumber > 0 && transaction.AccountTo?.AccountNumber == 0)
            {
                //Chamar saque
            }
        }

        private void Deposit(Domain.Entities.Transaction transaction)
        {

            var account = GetAccount.IfActiveById(transaction.AccountTo.AccountNumber, _context);
            if (IsNull(account)) return;

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
                Console.WriteLine(e.Message);
            }
        }

        private bool IsNull(Object obj)
        {
            return obj == null;
        }
    }
}
