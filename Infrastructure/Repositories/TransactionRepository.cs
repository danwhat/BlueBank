using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository

    {
        public Domain.Entities.Transaction Create(Domain.Entities.Transaction transaction)
        {
            throw new NotImplementedException();
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
    }
}
