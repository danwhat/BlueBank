using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    public interface IPersonRepository
    {
        public Person Update(Person person);
        public Person Get(string doc);
        public Person UpdateContactList(string doc, List<string> list);
        public Person RemoveContact(string doc, string phoneNumber);
        public Person AddContact(string doc, string phoneNumber);
    }
}
