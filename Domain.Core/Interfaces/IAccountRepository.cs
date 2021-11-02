using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Requests
{
    interface IAccountRepository
    {
        public Account Create(Account acc);
        public Account Update(Account acc);
        public Account Read(int accountNumber);
        public Account Read(string ownerDoc);
        public bool Delete(Account acc);
        public Account UpdateContactList(List<string> list);
    }
}
