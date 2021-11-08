using Domain.Core.DTOs;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Core.DTOs
{
    public class PersonRequestDtoTest
    {
        [Fact(DisplayName = "Create Person Request success")]
        public void CreatePersonRequest()
        {
            var personRequestDto = new PersonRequestDto(42314, "4353-5432");
            personRequestDto.AccountNumber.Should().Be(42314);
            personRequestDto.PhoneNumber.Should().Be("4353-5432");
        }
    }
}
