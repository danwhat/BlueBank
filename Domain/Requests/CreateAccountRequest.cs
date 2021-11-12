using System;
using Domain.Core.DTOs;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;
using Domain.Services.Validations;

namespace Domain.Requests
{
    public class CreateAccountRequest
    {
        private readonly CreateAccountDto _dto;

        private readonly IAccountRepository _accountRepository;

        public CreateAccountRequest(CreateAccountDto dto, IAccountRepository accountRepository)
        {
            _dto = dto;
            _accountRepository = accountRepository;
        }

        public void Validation()
        {
            Validations.NameValidation(_dto.Name);
            Validations.DocumentationValidation(_dto.Doc);
            Validations.PhoneNumberValidation(_dto.PhoneNumber);
        }

        public bool IsNaturalPerson()
        {
            return _dto.Doc.Length == 11;
        }

        public AccountDto Create()
        {
            Validation();

            var account = new Account();

            if (IsNaturalPerson())
            {
                NaturalPerson person = new();
                person.Name = _dto.Name;
                person.Address = _dto.Address;
                person.Cpf = _dto.Doc;
                person.PhoneNumbers.Add(_dto.PhoneNumber);

                account.Person = person;
            }
            else
            {
                LegalPerson person = new();
                person.Name = _dto.Name;
                person.Address = _dto.Address;
                person.Cnpj = _dto.Doc;
                person.PhoneNumbers.Add(_dto.PhoneNumber);

                account.Person = person;
            }

            try
            {
                Account newAccount = _accountRepository.Create(account);
                AccountDto response = new(newAccount);
                return response;
            }
            catch (ServerException e)
            {
                throw new Exception("Ocorreu um erro: " + e.Message);
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro interno.");
            }
        }
    }
}