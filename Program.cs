using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFProjectionRepro
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var dbContext = new Context();

            dbContext.Entities.Add(new Entity());

            await dbContext.Entities.AsNoTracking().Select(e => new
            {
                Id = e.Id,
                FirstChild = e.Children
                    .Where(c => c.Type == 1)
                    .AsQueryable()
                    .Select(Project)
                    .FirstOrDefault(),

                SecondChild = e.Children // Comment this projection for success 
                    .Where(c => c.Type == 2)
                    .AsQueryable()
                    .Select(Project)
                    .FirstOrDefault(),
            }).ToListAsync();
        }

        private static readonly Expression<Func<Child, object>> Project = x => new
        {
            x.Id,
            x.Owned, // Comment this line for success
            x.Type,
        };
    }
}
