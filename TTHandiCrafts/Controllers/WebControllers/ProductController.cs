using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TTHandiCrafts.Infrastructure.DevExpress.Options;
using TTHandiCrafts.UseCases.Modules.Products.Commands.CreateProduct;
using TTHandiCrafts.UseCases.Modules.Products.Queries.GetAllProducts;

namespace TTHandiCrafts.Controllers.WebControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(DataSourceLoadOptions loadOptions)
        {
            var dataSource = await mediator.Send(new GetAllProductsQuery());
            return Ok(await DataSourceLoader.LoadAsync(dataSource.AsQueryable(), loadOptions));
        }

        [HttpPost()]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
        {
            return Ok();
        }
    }
}