using KayraExport.Microservices.BuildingBlocks.Shared.Application.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Commands = KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands;
using Queries = KayraExport.Microservices.Services.Product.Application.CQRS.Product.Queries;

namespace KayraExport.Microservices.Services.Product.API.Controllers
{
    [Route("api/product-service/products")]
    public class ProductsController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Commands.Insert.Command command)
            => await ExecuteAsync(command);

        [Authorize]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] Commands.Update.Command command)
        {
            command.Id = id;
            return await ExecuteAsync(command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
            => await ExecuteAsync(new Commands.Delete.Command { Id = id });

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] Queries.GetAll.Query query)
            => await ExecuteAsync(query);
    }
}
