using System.Linq;

namespace Infrastructure.Shared
{
    internal static class GetPerson
    {
        internal static Person ByDocs(string docs, BlueBankContext context)
        {
            return context.People
                .Where(curr => curr.Doc == docs)
                .FirstOrDefault<Person>();
        }

        internal static Person IsActive(string docs, BlueBankContext context)
        {
            return context.People
                .Where(curr => curr.Doc == docs && curr.IsActive == true)
                .FirstOrDefault<Person>();
        }
    }
}
