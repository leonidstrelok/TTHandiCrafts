using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Interfaces;
using TTHandiCrafts.Models;
using TTHandiCrafts.Utils;

namespace TTHandiCrafts.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthorizationService> _logger;
        public AuthorizationService(SignInManager<ApplicationUser> signInManager,
            ILogger<AuthorizationService> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public async Task Auhtorization(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, true,  lockoutOnFailure: true);
            
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
       

       public async Task<bool>  Registration(Register register)
        {
            var user = new ApplicationUser
            {
                UserName = register.Name,          
                
                Created = SystemTime.Now()
            };
            var result = await _userManager.CreateAsync(user,register.Password);
            if (result.Succeeded) 
            {
                return true;
            }
            return false;

        }
    }
}
