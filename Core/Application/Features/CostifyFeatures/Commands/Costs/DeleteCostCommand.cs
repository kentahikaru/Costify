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
            ICostRepository _repository;
            public DeleteCostCommandHandler(ICostRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(DeleteCostCommand command, CancellationToken cancellationToken)
            {
                _repository.Delete(command.Id);
                return await _repository.SaveChangesAsync();

            }
        }
    }
}