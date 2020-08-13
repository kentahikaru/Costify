using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Commands.Costs
{
    public  class CreateCostCommand : IRequest<int>
    {
        public Cost cost { get;set; }
        public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, int>
        {
            //ICostifyDbContext _context;
            ICostRepository _repository;
            public CreateCostCommandHandler(ICostRepository repository)
            {
                //_context = context;
                _repository = repository;
            }

            public async Task<int> Handle(CreateCostCommand command, CancellationToken cancellationToken)
            {
                command.cost.Id = Guid.NewGuid();
                _repository.Add(command.cost);
                return await _repository.SaveChangesAsync();
            }
        }
    }
}