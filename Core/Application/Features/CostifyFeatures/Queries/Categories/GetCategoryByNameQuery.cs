using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Queries.Categories
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public Guid CategoryId { get; set; }
        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
        {
            private readonly ICostifyDbContext _context;
            public GetCategoryByIdQueryHandler(ICostifyDbContext context)
            {
                _context = context;
            }

            public async Task<Category> Handle(GetCategoryByIdQuery query , CancellationToken cancellationTOken)
            {
                var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == query.CategoryId);
                if(category == null)
                {
                    return null;
                }
                return category;
            }
        }
    }
}