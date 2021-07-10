using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TTHandiCrafts.Authorization;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Resources;
using TTHandiCrafts.Resources.Areas.Identity.Pages.Account;
using TTHandiCrafts.Utils;
using TTHandiCrafts.Validation;

namespace TTHandiCrafts.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(DataAnotationResources), ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "UserName", ResourceType = typeof(Login))]
            public string UserName { get; set; }

            [Required(ErrorMessageResourceType = typeof(DataAnotationResources), ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "Password", ResourceType = typeof(Login))]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Input.UserName);
                    if (user.NeedChangePassword)
                    {
                        await _signInManager.SignOutAsync();
                        HttpContext.Session.SetString(SessionKeys.UserFirstTimeLogin, Input.UserName);
                        HttpContext.Session.SetString(SessionKeys.UserFirstTimePassword, Input.Password);
                        HttpContext.Session.SetString(SessionKeys.RememberMe, Input.RememberMe.ToString());
                        await HttpContext.Session.CommitAsync();
                        return RedirectToPage("./MustChangePassword", new { ReturnUrl = returnUrl });
                    }

                    _logger.LogInformation($"User with name {Input.UserName} logged in at {SystemTime.Now()} ");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"{IdentityResources.InvalidLoginAttempt}.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();

        }
    }
}
