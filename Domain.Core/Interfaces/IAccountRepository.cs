using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    interface IAccountRepository
    {
        public Account Create(Account acc);
        public Account Get(int accountNumber);
        public Account Get(string ownerDoc);
        public bool Delete(Account acc);
    }
}
