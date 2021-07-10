using DSEU.UI.Resources.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Resources;
using TTHandiCrafts.Utils;
using TTHandiCrafts.Validation;

namespace TTHandiCrafts.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(DataAnotationResources),
               ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "OldPassword", ResourceType = typeof(ChangePassword))]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }

            [Required(ErrorMessageResourceType = typeof(DataAnotationResources),
              ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "NewPassword", ResourceType = typeof(ChangePassword))]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [Required(ErrorMessageResourceType = typeof(DataAnotationResources), ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "ConfirmPassword", ResourceType = typeof(ChangePassword))]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessageResourceType = typeof(DataAnotationResources),
                                    ErrorMessageResourceName = ValidationLocalizationKeys.CompareError)]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"{IdentityResources.UnableToLoadUserWithID} '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            user.LastPasswordChangeDate = SystemTime.Now();
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = IdentityResources.YourPasswordHasBeenChanged;

            return RedirectToPage();
        }
    }
}
