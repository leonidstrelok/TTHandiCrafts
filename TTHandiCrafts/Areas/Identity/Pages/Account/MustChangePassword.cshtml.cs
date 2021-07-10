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
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Resources;
using TTHandiCrafts.Resources.Areas.Identity.Pages.Account;
using TTHandiCrafts.Utils;
using TTHandiCrafts.Validation;

namespace TTHandiCrafts.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class MustChangePasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<MustChangePasswordModel> _logger;
        public string ReturnUrl { get; set; }

        public MustChangePasswordModel(SignInManager<ApplicationUser> signInManager,
            ILogger<MustChangePasswordModel> logger,
            UserManager<ApplicationUser> userManager,
            IApplicationDbContext dbContext)
        {
            _userManager = userManager;
            this.dbContext = dbContext;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required(ErrorMessageResourceType = typeof(DataAnotationResources), ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "Password", ResourceType = typeof(MustChangePassword))]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "ConfirmPassword", ResourceType = typeof(MustChangePassword))]
            [Compare("Password", ErrorMessageResourceType = typeof(DataAnotationResources), ErrorMessageResourceName = ValidationLocalizationKeys.CompareError)]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            var userName = HttpContext.Session.GetString(SessionKeys.UserFirstTimeLogin);
            if (userName == null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userName = HttpContext.Session.GetString(SessionKeys.UserFirstTimeLogin);
            if (userName == null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound($"{IdentityResources.UnableToLoadUserWithID} '{_userManager.GetUserId(User)}'.");
            }

            var firstTimePassword = HttpContext.Session.GetString(SessionKeys.UserFirstTimePassword);
            return await dbContext.InvokeTransactionAsync<IActionResult>(async () =>
             {
                 var changePasswordResult = await _userManager.ChangePasswordAsync(user, firstTimePassword, Input.Password);
                 if (!changePasswordResult.Succeeded)
                 {
                     foreach (var error in changePasswordResult.Errors)
                     {
                         ModelState.AddModelError(string.Empty, error.Description);
                     }
                     return Page();
                 }

                 user.NeedChangePassword = false;
                 user.LastPasswordChangeDate = SystemTime.Now();
                 await _userManager.UpdateAsync(user);

                 bool rememberMe = bool.Parse(HttpContext.Session.GetString(SessionKeys.RememberMe));
                 _logger.LogInformation("User signed in first time");
                 await _signInManager.PasswordSignInAsync(userName, Input.Password, rememberMe, true);
                 HttpContext.Session.Clear();
                 await HttpContext.Session.CommitAsync();
                 return Redirect(returnUrl);
             });

        }
    }
}
