using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Queries
{
    public class GetAllCostsQuery : IRequest<IEnumerable<Cost>>
    {
        public class GetAllCostsQueryHandler : IRequestHandler<GetAllCostsQuery, IEnumerable<Cost>>
        {
            private readonly ICostifyDbContext _context;
            public GetAllCostsQueryHandler(ICostifyDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Cost>> Handle(GetAllCostsQuery query , CancellationToken cancellationTOken)
            {
                var listCost = await _context.Cost.Include(x => x.Category).ToListAsync();
                if(listCost == null)
                {
                    return null;
                }
                return listCost.AsReadOnly();
            }
        }
    }
}