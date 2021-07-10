using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Resources;
using TTHandiCrafts.Resources.Areas.Identity.Pages.Account;
using TTHandiCrafts.Validation;

namespace TTHandiCrafts.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginWith2faModel> _logger;

        public LoginWith2faModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginWith2faModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(DataAnotationResources), ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "AuthenticatorCode", ResourceType = typeof(LoginWith2fa))]
            [StringLength(7, ErrorMessageResourceType = typeof(DataAnotationResources),
                ErrorMessageResourceName = ValidationLocalizationKeys.StringBetweenMinAndMax, MinimumLength = 6)]
            [DataType(DataType.Text)]
            public string TwoFactorCode { get; set; }

            public bool RememberMachine { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"{IdentityResources.UnableToLoadTwoFactorAuthenticationUser}.");
            }

            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"{IdentityResources.UnableToLoadTwoFactorAuthenticationUser}.");
            }

            var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
                return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning(IdentityResources.InvalidAuthenticatorCodeEnteredForUserWithID, user.Id);
                ModelState.AddModelError(string.Empty, IdentityResources.VerificationCodeIsInvalid);
                return Page();
            }
        }
    }
}
