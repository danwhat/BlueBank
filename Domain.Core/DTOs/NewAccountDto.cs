using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.DTOs
{
    public class NewAccountDto
    {
        public int AccountNumber { get; set; }
        public string Doc { get; set; }
        public string Name { get; set; }
    }
}
