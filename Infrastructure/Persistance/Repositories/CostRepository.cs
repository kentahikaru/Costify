using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class CostRepository : GenericRepository<Cost>, ICostRepository
    {
        public CostRepository(CostifyDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cost>> GetAllCostsAsync()
        {
            return await _context.Cost.Include(x => x.Category).ToListAsync();
        }

        public async Task<Cost> GetByIdAsync(Guid Id)
        {
            return await _context.Cost.Include(x => x.Category).FirstOrDefaultAsync(m => m.Id == Id);
        }

        public void Delete(Guid Id)
        {
            var cost = _context.Cost.Where(a => a.Id == Id).FirstOrDefault();
            if(cost != null)
            {
                _context.Cost.Remove(cost);     
            }
            else
            {
                // TODO: log
            }
        }

        public void Update(Cost entity)
        {
            var cost = _context.Cost.Where(a => a.Id == entity.Id).FirstOrDefault();
            if(cost != null)
            {
                cost.Price = entity.Price;
                cost.Category = entity.Category;
                cost.Date = entity.Date;    
            }
            else
            {
                // TODO: log
            }

            
 
        }
    }
}