using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Commands.Costs
{
    public  class CreateCostCommand : IRequest<Guid>
    {
        public Cost cost { get;set; }
        public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, Guid>
        {
            //ICostifyDbContext _context;
            ICostRepository _repository;
            public CreateCostCommandHandler(ICostRepository repository)
            {
                //_context = context;
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateCostCommand command, CancellationToken cancellationToken)
            {
                command.cost.Id = Guid.NewGuid();
                _repository.Add(command.cost);
                await _repository.SaveChangesAsync();
                return command.cost.Id;
            }
        }
    }
}