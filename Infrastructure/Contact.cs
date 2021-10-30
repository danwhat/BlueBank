using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    class Contact
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
