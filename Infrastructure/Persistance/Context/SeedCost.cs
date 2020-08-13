namespace Infrastructure.Persistance
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Core.Application.Features.CostifyFeatures.Commands.Categories;
    using Core.Application.Features.CostifyFeatures.Queries.Categories;
    using Core.Application.Interfaces;
    using Core.Domain.Entities;
    using MediatR;

    public class SeedCost
    {
        private IMediator _mediator;

        public SeedCost(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Seed()
        {
            var cats = await _mediator.Send(new GetAllCategoriesQuery());
            var enumer = cats.GetEnumerator();
            if(enumer.MoveNext() == true)
            {
                return;
            }

            List<Category> categories = new List<Category>() {
                new Category() {CategoryName = "Eating out"},
                new Category() {CategoryName = "Electronics"},
                new Category() {CategoryName = "Groceries"},
                new Category() {CategoryName = "House Equipment"},
                new Category() {CategoryName = "Shopping"},
                new Category() {CategoryName = "Other"},
            };

            foreach(Category category in categories)
            {
                await _mediator.Send(new CreateCategoryCommand() {category = category} );
            }
           
        }

    }
}