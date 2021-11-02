using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.DTOs
{
    public class NewContactDto
    {
        public string Doc { get; set; }
        public int AccountNumber { get; set; }

        public string PhoneNumber { get; set; }
    }
}
