using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Application.Interfaces;

namespace Infrastructure.Persistance
{
    public class CostifyDbContext : DbContext, ICostifyDbContext
    {
        public CostifyDbContext (DbContextOptions<CostifyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cost> Cost { get; set; }
        public DbSet<Category> Category { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        public void Add(object obj)
        {
            base.Add(obj);
        }

          public void Update(object obj)
        {
            base.Update(obj);
        }
    }
}