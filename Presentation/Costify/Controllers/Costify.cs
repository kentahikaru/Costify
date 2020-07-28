using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Application.Features.CostifyFeatures.Commands;
using Core.Application.Features.CostifyFeatures.Queries;
using Presentation.Costify.ViewModels.Costify;

namespace Costify.Controllers
{
    public class Costify : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        // public Costify(ICostifyDbContext context)
        // {
        //     _context = context;
        // }

        // GET: Costify
        public async Task<IActionResult> Index()
        {
            var costs = await Mediator.Send(new GetAllCostsQuery());
            List<CostifyViewModel> listOfCosts = new List<CostifyViewModel>();

            foreach(Cost cost in costs)
            {
                listOfCosts.Add(new CostifyViewModel() {
                    Id = cost.Id,
                    Date = cost.Date,
                    Price = cost.Price,
                    CategoryId = cost.Category.Id,
                    CategoryName = cost.Category.CategoryName
                });
            }

            return View(listOfCosts.ToArray());
        }

        // GET: Costify/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await Mediator.Send(new GetCostByIdQuery() { Id = (Guid)id });
            if (cost == null)
            {
                return NotFound();
            }

            CostifyViewModel costifyViewModel = new CostifyViewModel() {
                Id = cost.Id,
                Date = cost.Date,
                Price = cost.Price,
                CategoryId = cost.Category.Id,
                CategoryName = cost.Category.CategoryName
            };
            return View(costifyViewModel);
        }

        // GET: Costify/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Category> categories = await Mediator.Send(new GetAllCategoriesQuery());
            CreateViewModel createViewModel = new CreateViewModel() { Date = DateTime.Now };

            createViewModel.ListOfCategories = new SelectList(categories, "Id","CategoryName");

            return View(createViewModel);
        }

        // POST: Costify/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Price,CategoryId")] CreateViewModel create)
        {
            if (ModelState.IsValid)
            {
                Cost cost = new Cost() {Date = create.Date, Price = create.Price};
                cost.Category = await Mediator.Send(new GetCategoryByIdQuery() {CategoryId = Guid.Parse(create.CategoryId)});

                await Mediator.Send(new CreateCostCommand() { cost = cost  });
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<Category> categories = await Mediator.Send(new GetAllCategoriesQuery());
            create.ListOfCategories = new SelectList(categories, "Id","CategoryName");

            return View(create);
        }

        // GET: Costify/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await Mediator.Send(new GetCostByIdQuery() { Id = (Guid)id });
            if (cost == null)
            {
                return NotFound();
            }
            return View(cost);
        }

        // POST: Costify/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Date,Price")] Cost cost)
        {
            if (id != cost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Mediator.Send(new UpdateCostCommand(){cost = cost});
                return RedirectToAction(nameof(Index));
            }
            return View(cost);
        }

        // GET: Costify/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await Mediator.Send(new GetCostByIdQuery() { Id = (Guid)id });
            if (cost == null)
            {
                return NotFound();
            }

            CostifyViewModel costifyViewMode = new CostifyViewModel() {
                Id = cost.Id,
                Date = cost.Date,
                Price = cost.Price,
                CategoryId = cost.Category.Id,
                CategoryName = cost.Category.CategoryName
            };

            return View(costifyViewMode);
        }

        // POST: Costify/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            // var cost = await _context.Cost.FindAsync(id);
            // _context.Cost.Remove(cost);
            // await _context.SaveChanges();
            await Mediator.Send(new DeleteCostCommand() {Id = id });
            return RedirectToAction(nameof(Index));
        }

        // private bool CostExists(Guid id)
        // {
        //     return _context.Cost.Any(e => e.Id == id);
        // }
    }
}
