using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TTHandiCrafts.Infrastructure.DevExpress.Options;
using TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.CreateAdvertising;
using TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.DeleteAdvertising;
using TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.UpdateAdvertising;
using TTHandiCrafts.UseCases.Modules.Admins.Queries.AdvertisingQueries.GetAdvertisingById;
using TTHandiCrafts.UseCases.Modules.Admins.Queries.AdvertisingQueries.GetAllAdvertisings;

namespace TTHandiCrafts.Controllers.WebControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("API для работы с админом")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator mediator;

        public AdminController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetAllAdvertising")]
        public async Task<IActionResult> GetAllAdvertising(DataSourceLoadOptions loadOptions)
        {
            var dataSource = await mediator.Send(new GetAllAdvertisingsQuery());
            return Ok(await DataSourceLoader.LoadAsync(dataSource.AsQueryable(), loadOptions));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdvertisingById(int id)
        {
            var advertising = await mediator.Send(new GetAdvertisingByIdQuery(id));
            return Ok(advertising);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvertising([FromForm] CreateAdvertisingCommand command)
        {
            var advertising = await mediator.Send(command);
            return Ok(advertising);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdvertising(int id, [FromForm] UpdateAdvertisingCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var advertising = await mediator.Send(command);

            return Ok(advertising);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvertising(int id)
        {
            await mediator.Send(new DeleteAdvertisingCommand(id));
            return NoContent();
        }
        
    }
}