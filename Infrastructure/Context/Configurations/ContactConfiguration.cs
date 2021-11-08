using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .Property(contact => contact.CreatedAt)
                .HasDefaultValueSql("getdate()");
            builder
                .HasOne<Person>(contact => contact.Person)
                .WithMany(person => person.Contacts)
                .HasForeignKey(contact => contact.PersonId);
        }
    }
}
