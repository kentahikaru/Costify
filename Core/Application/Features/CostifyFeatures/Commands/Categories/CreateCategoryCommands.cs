using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Commands.Categories
{
    public  class CreateCategoryCommand : IRequest<int>
    {
        public Category category { get;set; }
        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
        {
            ICategoryRepository _repository;
            public CreateCategoryCommandHandler(ICategoryRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
            {
                command.category.Id = Guid.NewGuid();
                _repository.Add(command.category);
                return await _repository.SaveChangesAsync();
            }
        }
    }
}