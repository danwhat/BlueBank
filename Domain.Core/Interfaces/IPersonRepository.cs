using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    interface IPersonRepository
    {
        public Person Update(Person acc);
        public Account Read(int AccountNumber);
        public bool Delete(Account acc);
    }
}
