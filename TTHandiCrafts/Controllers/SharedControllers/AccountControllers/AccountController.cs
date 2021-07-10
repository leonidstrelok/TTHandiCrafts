using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TTHandiCrafts.Controllers.SharedControllers.AccountControllers
{
    /// <summary>
    /// Контроллер для авторизации и регистрации
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("API для авторизации в системе")]
    public class AccountController : ControllerBase
    {
       
    }
}