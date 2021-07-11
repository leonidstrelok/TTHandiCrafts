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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        ///private UserContext db;
        public AccountController(IMediator mediator, Interfaces.IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
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
        [ValidateAntiForgeryToken]
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPassword(string email)
        //{
        //    var token = await authorizationService.ResetPasswordByEmail(email);
        //    string resetUrl = Url.Action(nameof(AccountController.PasswordReset), nameof(AccountController),  token, Request.Scheme);

        //    // Creates and sends the password reset email to the user's address
        //    await emailSender.SendEmailAsync(email, "Password reset request",
        //        $"To reset your account's password, click <a href=\"{resetUrl}\">here</a>");
        //    return Ok();
        //}
        //[HttpGet]
        //public async Task<IActionResult> PasswordReset(int? userId, string token)
        //{
        //    if (String.IsNullOrEmpty(token))
        //    {
        //        return NotFound();
        //    }

        //   var user = await userManager.FindByIdAsync(userId.ToString());

        //    try
        //    {                
        //        if (await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token))
        //        {       

        //            return Ok();
        //        }

        //        return BadRequest();
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}