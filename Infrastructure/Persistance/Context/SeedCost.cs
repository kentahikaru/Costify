namespace Infrastructure.Persistance
{
    using System;
    using System.Collections.Generic;
    using Core.Application.Features.CostifyFeatures.Commands;
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
        public void Seed()
        {
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
                _mediator.Send(new CreateCategoryCommand() {category = category} );
            }
        }

    }
}