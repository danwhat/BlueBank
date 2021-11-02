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
        public Person Update(Person person);
        public Person Read(string doc);
    }
}
