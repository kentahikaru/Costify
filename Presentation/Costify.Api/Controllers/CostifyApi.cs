using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MediatR;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Application.Features.CostifyFeatures.Commands.Costs;
using Core.Application.Features.CostifyFeatures.Queries.Costs;
using Core.Application.Features.CostifyFeatures.Queries.Categories;

namespace Costify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostifyApi : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private ILogger Logger;

        public CostifyApi(ILogger<CostifyApi> logger)
        {
            Logger = logger;
        }

        // GET: api/CostifyApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cost>>> GetCost()
        {
            var costs = await Mediator.Send(new GetAllCostsQuery());
            return costs.ToList<Cost>();
        }

        // GET: api/CostifyApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cost>> GetCost(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cost = await Mediator.Send(new GetCostByIdQuery() { Id = id });
            if (cost == null)
            {
                return NotFound();
            }
            return cost;
        }

        // PUT: api/CostifyApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCost(Guid id, Cost cost)
        {
            if (id != cost.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await Mediator.Send(new UpdateCostCommand(){cost = cost});
            }

            return NoContent();
        }

        // POST: api/CostifyApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cost>> PostCost(Cost create)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            create.Id = await Mediator.Send(new CreateCostCommand() { cost = create  });
                       
            return CreatedAtAction(nameof(GetCost), new { id = create.Id }, create);
        }

        // DELETE: api/CostifyApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cost>> DeleteCost(Guid id)
        {
            var cost = await Mediator.Send(new GetCostByIdQuery() { Id = (Guid)id });

            if (cost == null)
            {
                return NotFound();
            }

            await Mediator.Send(new DeleteCostCommand() {Id = id });

            return cost;
        }
    }
}
