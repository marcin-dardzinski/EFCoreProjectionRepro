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
            await dbContext.SaveChangesAsync();
            await dbContext.Entities.ToListAsync();
        }
    }
}
