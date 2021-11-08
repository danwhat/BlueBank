using Domain.Entities;

namespace Domain.Core.DTOs
{
    public class PersonRequestDto
    {
        public PersonRequestDto(int accountNumber, string phoneNumber)
        {
            AccountNumber = accountNumber;
            PhoneNumber = phoneNumber;
        }

        public int AccountNumber { get; set; }

        public string PhoneNumber { get; set; }
    }
}
