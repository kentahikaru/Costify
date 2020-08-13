using System;
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
    public class GetCostByIdQuery : IRequest<Cost>
    {
        public Guid Id { get; set; }
        public class GetCostByIdQueryHandler : IRequestHandler<GetCostByIdQuery, Cost>
        {
            private readonly ICostRepository _repository;
            public GetCostByIdQueryHandler(ICostRepository repository)
            {
                _repository = repository;
            }

            public async Task<Cost> Handle(GetCostByIdQuery query , CancellationToken cancellationTOken)
            {
                var cost = await _repository.GetByIdAsync(query.Id);
                if(cost == null)
                {
                    return null;
                }
                return cost;
            }
        }
    }
}