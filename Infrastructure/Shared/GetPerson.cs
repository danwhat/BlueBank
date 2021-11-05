﻿using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Shared
{
    internal static class GetPerson
    {
        internal static Person ByDocs(string docs, BlueBankContext context)
        {
            return context.People
                .Where(curr => curr.Doc == docs)
                .Include(person => person.Contacts)
                .FirstOrDefault<Person>();
        }

        internal static Person IfActive(string docs, BlueBankContext context)
        {
            return context.People
                .Where(curr => curr.Doc == docs && curr.IsActive == true)
                .Include(person => person.Contacts)
                .FirstOrDefault<Person>();
        }
    }
}
