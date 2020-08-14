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
            private readonly ICategoryRepository _repository;
            public GetCategoryByIdQueryHandler(ICategoryRepository repository)
            {
                _repository = repository;
            }

            public async Task<Category> Handle(GetCategoryByIdQuery query , CancellationToken cancellationTOken)
            {
                var category = await _repository.GetByIdAsync(query.CategoryId);
                if(category == null)
                {
                    return null;
                }
                return category;
            }
        }
    }
}