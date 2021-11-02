using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    interface ITransactionRepository
    {
        public Transaction Create(Transaction transaction);
        public List<Transaction> ReadCotainsAcc(Account acc);
        public List<Transaction> ReadCotainsAcc(int accountNumber);
        public List<Transaction> ReadByPeriod(DateTime initial, DateTime final, Account acc);
        public List<Transaction> ReadByPeriod(DateTime initial, DateTime final, int accountNumber);
    }
}
