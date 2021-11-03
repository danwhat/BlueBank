using System.Linq;
using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Domain.Entities.Account Create(Domain.Entities.Account acc)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(Domain.Entities.Account acc)
        {
            throw new System.NotImplementedException();
        }

        public Domain.Entities.Account Get(int accountNumber)
        {
            throw new System.NotImplementedException();
        }

        public Domain.Entities.Account Get(string ownerDoc)
        {
            throw new System.NotImplementedException();
        }
    }
}
