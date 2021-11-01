using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NaturalPerson : Person
    {
        public string Cpf
        {
            get
            {
                return base.Doc;
            }
            set
            {
                base.Doc = value;
            }
        }
    }
}
