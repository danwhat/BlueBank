using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Domain.Entities.Person AddContact(string doc, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Person Get(string doc)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Person RemoveContact(string doc, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Person Update(Domain.Entities.Person person)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Person UpdateContactList(string doc, List<string> list)
        {
            throw new NotImplementedException();
        }
    }
}
