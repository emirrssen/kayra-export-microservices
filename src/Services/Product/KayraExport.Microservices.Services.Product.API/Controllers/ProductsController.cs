using KayraExport.Microservices.BuildingBlocks.Shared.Application.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Commands = KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands;

namespace KayraExport.Microservices.Services.Product.API.Controllers
{
    [Route("api/product-service/products")]
    public class ProductsController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Commands.Insert.Command command)
            => await ExecuteAsync(command);

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Commands.Update.Command command)
            => await ExecuteAsync(command);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
            => await ExecuteAsync(new Commands.Delete.Command { Id = id });
    }
}
