using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Commands
{
    public  class CreateCostCommand : IRequest<int>
    {
        public Cost cost { get;set; }
        public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, int>
        {
            ICostifyDbContext _context;
            public CreateCostCommandHandler(ICostifyDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCostCommand command, CancellationToken cancellationToken)
            {
                command.cost.Id = Guid.NewGuid();
                _context.Add(command.cost);
                return await _context.SaveChanges();
                //return command.cost.Id;
            }
        }
    }
}