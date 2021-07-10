using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using TTHandiCrafts.Interfaces;
using TTHandiCrafts.Models;

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
        private readonly IMediator mediator;
        private readonly IAuthorizationService authorizationService;

        ///private UserContext db;
        public AccountController(IMediator mediator, IAuthorizationService authorizationService)
        {
            this.mediator = mediator;
            this.authorizationService = authorizationService;
        }
        /// <summary>
        /// Логин в портал
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            await authorizationService.Auhtorization(login);
            return Ok();
        }
        /// <summary>
        /// Регистрация в портал 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] Register register)
        {

            var result=await authorizationService.Registration(register);
            if (result)
            {
                
            }
            return Ok();
        }
        [HttpPost()]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}