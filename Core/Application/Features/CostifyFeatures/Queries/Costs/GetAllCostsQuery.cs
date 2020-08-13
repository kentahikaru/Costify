using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Queries.Costs
{
    public class GetAllCostsQuery : IRequest<IEnumerable<Cost>>
    {
        public class GetAllCostsQueryHandler : IRequestHandler<GetAllCostsQuery, IEnumerable<Cost>>
        {
            private readonly ICostifyDbContext _context;
            private readonly ICostRepository _repository;
            public GetAllCostsQueryHandler(ICostRepository repository)
            {
                //_context = context;
                _repository = repository;
            }

            public async Task<IEnumerable<Cost>> Handle(GetAllCostsQuery query , CancellationToken cancellationTOken)
            {
                //var listCost = await _context.Cost.Include(x => x.Category).ToListAsync();
                var listCost = await _repository.GetAllCostsAsync();
                if(listCost == null)
                {
                    return null;
                }
                return listCost; // AsReadOnly();
            }
        }
    }
}