using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class CategoryReposoitory : GenericRepository<Category>, ICategoryRepository
    {
         public CategoryReposoitory(CostifyDbContext context) : base(context)
        {
        }

        public async Task<Category> GetByIdAsync(Guid Id)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}