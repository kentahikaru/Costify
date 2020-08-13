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
            ICostRepository _repository;
            public UpdateCostCommandHandler(ICostRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(UpdateCostCommand command, CancellationToken cancellationToken)
            {
                _repository.Update(command.cost);
                return await _repository.SaveChangesAsync();
            }
        }
    }
}