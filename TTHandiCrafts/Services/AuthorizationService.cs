using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Interfaces;
using TTHandiCrafts.Models;
using TTHandiCrafts.Utils;


using Microsoft.AspNetCore.Mvc;


namespace TTHandiCrafts.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AuthorizationService> _logger;
        public AuthorizationService(SignInManager<IdentityUser> signInManager,
            ILogger<AuthorizationService> logger,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public async Task Auhtorization(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, true, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(login.UserName);


                _logger.LogInformation($"User with name {login.UserName} logged in at {SystemTime.Now()} ");

            }
            if (result.RequiresTwoFactor)
            {
                //return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");

            }
        }

        public Task PasswordReset(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Registration(Register register)
        {
            var user = new ApplicationUser
            {
                UserName = register.Name,

                Created = SystemTime.Now()
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, register.Password, true, false);
                return true;
            }
            return false;

        }



        public async Task<string> ResetPasswordByEmail(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Generates a password reset token for the user
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                
                return token;
            }
            return "CheckYourEmail";
        }       
    }
}
