using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.CostifyFeatures.Commands
{
    public  class CreateCategoryCommand : IRequest<int>
    {
        public Category category { get;set; }
        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
        {
            ICostifyDbContext _context;
            public CreateCategoryCommandHandler(ICostifyDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
            {
                command.category.Id = Guid.NewGuid();
                _context.Category.Add(command.category);
                return await _context.SaveChanges();
            }
        }
    }
}