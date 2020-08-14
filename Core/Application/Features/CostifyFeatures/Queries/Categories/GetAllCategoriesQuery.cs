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
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
        {
            private readonly ICategoryRepository _repository;
            public GetAllCategoriesQueryHandler(ICategoryRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query , CancellationToken cancellationTOken)
            {
                
                var listCategories = await _repository.GetAllAsync();
                if(listCategories == null)
                {
                    return null;
                }
                return listCategories;
            }
        }
    }
}