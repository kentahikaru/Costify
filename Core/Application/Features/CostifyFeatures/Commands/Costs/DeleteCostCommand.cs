using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Application.Interfaces;

namespace Core.Application.Features.CostifyFeatures.Commands.Costs
{
    public  class DeleteCostCommand : IRequest<int>
    {
        public Guid Id { get;set; }
        public class DeleteCostCommandHandler : IRequestHandler<DeleteCostCommand, int>
        {
            ICostifyDbContext _context;
            public DeleteCostCommandHandler(ICostifyDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteCostCommand command, CancellationToken cancellationToken)
            {
                var cost = _context.Cost.Where(a => a.Id == command.Id).FirstOrDefault();
                if(cost == null)
                {
                    return default;
                }

               _context.Cost.Remove(cost);

                return await _context.MySaveChangesAsync();
            }
        }
    }
}