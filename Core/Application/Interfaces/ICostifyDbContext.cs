using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
    public interface ICostifyDbContext
    {
        DbSet<Cost> Cost { get; set; }
        DbSet<Category> Category { get; set; }

        Task<int> MySaveChangesAsync();

        void Add(Object obj);
        void Update(Object obj);
    }
}