using System;
using System.Text.RegularExpressions;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces;
using Domain.Entities;

namespace Domain.Services.Validations
{
    public class Validations
    {
        public static void NameValidation(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z ]+$")) throw new InvalidInputException("Nome não pode conter numeros");
            if (String.IsNullOrEmpty(name)) throw new InvalidCastException("Nome não pode estar vazio");

        }

        public static void DocumentationValidation(string doc)
        {
            var DocOnlyNumbers = doc.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).Replace(" ", string.Empty);
            if (DocOnlyNumbers.Length != 11 && DocOnlyNumbers.Length != 14) throw new InvalidCastException("Documento Inválido");
            if (String.IsNullOrEmpty(DocOnlyNumbers)) throw new InvalidCastException("Documento não pode estar vazio");
        }

        public static void PhoneNumberValidation(string phoneNumber)
        {
            var phoneNumberOnlyNumbers = phoneNumber
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace(".", string.Empty)
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty);

            if (phoneNumberOnlyNumbers.Length != 10 && phoneNumberOnlyNumbers.Length != 11)
                throw new InvalidInputException("Numero de telefone Invalido. Padrão esperado: (00) 0000-0000");

        }

        public static void ThisAccountExistsValidation(IAccountRepository accountRepository, int accountNumber)
        {
            try
            {
                Account acc = accountRepository.Get(accountNumber);
            }
            catch (ServerException)
            {

                throw new InvalidInputException("Conta não encontrada.");
            }
        }

        public static void ThisPersonExistsValidation(IAccountRepository accountRepository, int accountNumber, out Person person)
        {
            try
            {
                person = accountRepository.Get(accountNumber).Person;
            }
            catch (ServerException)
            {
                throw new InvalidInputException("Pessoa não encontrada.");
            }
        }

        public static void ThisPersonExistsValidation(IPersonRepository personRepository, string doc, out Person person)
        {
            try
            {
                person = personRepository.Get(doc);
            }
            catch (ServerException)
            {
                throw new InvalidInputException("Pessoa não encontrada.");
            }
        }

        public static void ThisPersonHasThatPhoneNumberValidation(string phoneNumber, Person person)
        {
            if (!person.PhoneNumbers.Contains(phoneNumber)) throw new InvalidInputException("Número não encontrado.");
        }

        public static void ThisPersonHasNotThatPhoneNumberValidation(string phoneNumber, Person person)
        {
            if (person.PhoneNumbers.Contains(phoneNumber)) throw new InvalidInputException("Número já Cadastrado.");
        }

        public static void SufficientBalanceValidation(IAccountRepository accountRepository, int accountNumber, Decimal value)
        {
            Account acc = accountRepository.Get(accountNumber);
            if (acc.Balance < value) throw new InvalidInputException("Saldo insuficiente.");
        }
    }
}
