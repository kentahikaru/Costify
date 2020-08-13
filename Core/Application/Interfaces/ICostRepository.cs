using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
    public interface ICostRepository : IGenericRepository<Cost>
    {
        Task<IEnumerable<Cost>> GetAllCostsAsync();
        Task<Cost> GetByIdAsync(Guid Id);
        void Delete(Guid Id);
        void Update(Cost entity);
    }
}