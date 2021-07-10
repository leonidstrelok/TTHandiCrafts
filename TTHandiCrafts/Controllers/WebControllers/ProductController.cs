using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TTHandiCrafts.Infrastructure.DevExpress.Options;
using TTHandiCrafts.UseCases.Modules.Products.Commands.CreateProduct;
using TTHandiCrafts.UseCases.Modules.Products.Commands.DeletProduct;
using TTHandiCrafts.UseCases.Modules.Products.Commands.UpdateProduct;
using TTHandiCrafts.UseCases.Modules.Products.Queries.GetAllProducts;

namespace TTHandiCrafts.Controllers.WebControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("API для работы над изделиями")]
    public class ProductController : Controller
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        
        /// <summary>
        /// Получение всех изделий
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(DataSourceLoadOptions loadOptions)
        {
            var dataSource = await mediator.Send(new GetAllProductsQuery());
            return Ok(await DataSourceLoader.LoadAsync(dataSource.AsQueryable(), loadOptions));
        }

        /// <summary>
        /// Добавления нового изделия
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
        {
            var product = await mediator.Send(command);
            return Ok(product);
        }

        /// <summary>
        /// Обновление изделия
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            var product = await mediator.Send(command);
            return Ok(product);
        }

        /// <summary>
        /// Удаление изделия
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await mediator.Send(new DeletProductCommand(id));
            return Ok();
        }
        
        
    }
}