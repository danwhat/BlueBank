using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    class Account
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public int BalanceId { get; set; }
        public List<Balance> Balances { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}

//ContaBB
//- Id_ContaBB
//- Id_Pessoa
//- UpdatedAt
//- CreatedAt