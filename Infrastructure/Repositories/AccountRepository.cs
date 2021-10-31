using Domain.Core.DTOs;

namespace Infrastructure.Repositories
{
    public class AccountRepository
    {
        private readonly BlueBankContext _context;

        public AccountRepository()
        {
            _context = new BlueBankContext();
        }
        public NewAccountDto Create(NewAccountDto dto)
        {
            var person = new Person() { Doc = dto.Doc, Name = dto.Name, Type = 1 };
            var account = new Account() { Person = person };

            var result = _context.Accounts.Add(account);
            _context.SaveChanges();

            return new NewAccountDto() { Doc = dto.Doc, Name = dto.Name, AccountNumber = account.Id };
        }
    }
}
