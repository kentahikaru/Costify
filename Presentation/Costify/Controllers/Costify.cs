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

namespace Costify.Controllers
{
    public class Costify : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        //private readonly ICostifyDbContext _context;

        // public Costify(ICostifyDbContext context)
        // {
        //     _context = context;
        // }

        // GET: Costify
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Cost.ToListAsync());
            return View(await Mediator.Send(new GetAllCostsQuery()));
        }

        // GET: Costify/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var cost = await _context.Cost.FirstOrDefaultAsync(m => m.Id == id);
            var cost = await Mediator.Send(new GetCostByIdQuery() { Id = (Guid)id });
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // GET: Costify/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Costify/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Price")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                // cost.Id = Guid.NewGuid();
                // _context.Add(cost);
                // await _context.SaveChanges();
                await Mediator.Send(new CreateCostCommand() { cost = cost });
                return RedirectToAction(nameof(Index));
            }
            return View(cost);
        }

        // GET: Costify/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var cost = await _context.Cost.FindAsync(id);
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
                // try
                // {
                //     // _context.Update(cost);
                //     // await _context.SaveChanges();
                //     await Mediator.Send(new UpdateCostCommand(){cost = cost});
                // }
                // catch (DbUpdateConcurrencyException)
                // {
                //     if (!CostExists(cost.Id))
                //     {
                //         return NotFound();
                //     }
                //     else
                //     {
                //         throw;
                //     }
                // }
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

            //var cost = await _context.Cost.FirstOrDefaultAsync(m => m.Id == id);
            var cost = await Mediator.Send(new GetCostByIdQuery() { Id = (Guid)id });
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
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
