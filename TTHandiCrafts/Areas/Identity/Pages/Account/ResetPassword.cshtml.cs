using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Resources;
using TTHandiCrafts.Resources.Areas.Identity.Pages.Account;
using TTHandiCrafts.Validation;

namespace TTHandiCrafts.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(DataAnotationResources),
                ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "UserName", ResourceType = typeof(ResetPassword))]
            public string UserName { get; set; }

            [Required(ErrorMessageResourceType = typeof(DataAnotationResources), ErrorMessageResourceName = ValidationLocalizationKeys.FieldRequired)]
            [Display(Name = "Password", ResourceType = typeof(ResetPassword))]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "ConfirmPassword", ResourceType = typeof(ResetPassword))]
            [Compare("Password", ErrorMessageResourceType = typeof(DataAnotationResources),
                ErrorMessageResourceName = ValidationLocalizationKeys.CompareError)]
            public string ConfirmPassword { get; set; }
            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest($"{IdentityResources.ACodeMustBeSuppliedForPasswordReset}.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Input.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
