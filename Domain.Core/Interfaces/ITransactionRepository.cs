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
        public List<Transaction> GetByDoc(string Doc);
        public List<Transaction> GetByAcc(int accountNumber);
        public List<Transaction> GetByPeriod(DateTime initial, DateTime final, string Doc);
        public List<Transaction> GetByPeriod(DateTime initial, DateTime final, int accountNumber);
    }
}
