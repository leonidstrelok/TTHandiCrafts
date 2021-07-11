using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
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
        private readonly Interfaces.IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEmailSender emailSender;

        ///private UserContext db;
        public AccountController(IMediator mediator, Interfaces.IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            this.mediator = mediator;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.emailSender = emailSender;
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
        [AllowAnonymous]
        [HttpPost("Register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] Register register)
        {

            var result = await authorizationService.Registration(register);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost()]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}