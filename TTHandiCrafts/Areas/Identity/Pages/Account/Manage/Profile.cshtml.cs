using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models;
using TTHandiCrafts.Resources;

namespace TTHandiCrafts.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IApplicationDbContext dbContext;
        //private readonly IEmployeePhotoService employeePhotoService;

        public ProfileModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IApplicationDbContext dbContext
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            public string UserName { get; set; }
            [DataType(DataType.PhoneNumber)]
            public string Phone { get; set; }
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            public string PersonalPhoto { get; set; }
            public IFormFile Photo { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Input = new InputModel();

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"{IdentityResources.UnableToLoadUserWithID} '{userManager.GetUserId(User)}'.");
            }

            var employee = await dbContext.Set<User>().FirstOrDefaultAsync(p => p.IdentityUserId == user.Id);

            Input.UserName = user.UserName;
            Input.Phone = employee?.Phone ?? user.PhoneNumber;
            Input.Email = employee?.Email ?? user.Email;
            //if (employee?.PersonalPhoto != default)
            //{
            //    Input.PersonalPhoto = Convert.ToBase64String(employee?.PersonalPhoto);
            //}

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var identityUser = await userManager.GetUserAsync(User);
            if (identityUser == null)
            {
                return NotFound($"{IdentityResources.UnableToLoadUserWithID} '{userManager.GetUserId(User)}'.");
            }

            var user = await dbContext.Set<User>().FirstOrDefaultAsync(p => p.IdentityUserId == identityUser.Id);

            if (user is User employee)
            {
                employee.Email = Input.Email;
                employee.Phone = Input.Phone;
            }

            identityUser.PhoneNumber = Input.Phone;
            identityUser.Email = Input.Email;

            //await UpdateEmployeePersonalPhoto(Input.Photo, user);

            await userManager.UpdateAsync(identityUser);
            await signInManager.RefreshSignInAsync(identityUser);

            await dbContext.SaveChangesAsync();

            StatusMessage = IdentityResources.YourProfileHasBeenUpdated;

            return RedirectToPage();
        }

        //private async Task UpdateEmployeePersonalPhoto(IFormFile personalPhoto, User user)
        //{
        //    if (personalPhoto != null)
        //    {
        //        var photoStream = personalPhoto.OpenReadStream();

        //        var newPhotoHash = await employeePhotoService.CreateThumbnail(photoStream);

        //        if (user.PersonalPhotoHash != null)
        //        {
        //            employeePhotoService.RemoveThumbnail(user.PersonalPhotoHash);
        //        }

        //        user.PersonalPhotoHash = newPhotoHash;
        //        photoStream.Position = 0;
        //        using var ms = new MemoryStream();
        //        await photoStream.CopyToAsync(ms);
        //        user.PersonalPhoto = ms.ToArray();
        //    }
        //}
    }
}
