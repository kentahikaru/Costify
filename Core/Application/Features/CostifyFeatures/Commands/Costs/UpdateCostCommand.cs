using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Commands.Costs
{
    public  class UpdateCostCommand : IRequest<int>
    {
        public Cost cost { get;set; }
        public class UpdateCostCommandHandler : IRequestHandler<UpdateCostCommand, int>
        {
            ICostifyDbContext _context;
            public UpdateCostCommandHandler(ICostifyDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateCostCommand command, CancellationToken cancellationToken)
            {
                var cost = _context.Cost.Where(a => a.Id == command.cost.Id).FirstOrDefault();
                if(cost == null)
                {
                    return default;
                }

                cost.Price = command.cost.Price;
                cost.Category = command.cost.Category;
                cost.Date = command.cost.Date;

                return await _context.MySaveChangesAsync();
            }
        }
    }
}