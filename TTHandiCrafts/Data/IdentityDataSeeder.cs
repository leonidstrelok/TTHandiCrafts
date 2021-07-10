using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Persistences;
using TTHandiCrafts.UseCases.Commons.Constants;

namespace TTHandiCrafts.Data
{
    public class IdentityDataSeeder
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityDataSeeder(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }

        public async Task SeedData()
        {
            await InitRoles();
            await CreateSystemUser();
        }

        private async Task InitRoles()
        {
            foreach (var role in Roles.GetBaseRoles())
            {
                if (!await roleManager.Roles.AnyAsync(p => p.Name == role))
                {
                    IdentityRole identityRole = new()
                    {
                        Name = role
                    };
                    await roleManager.CreateAsync(identityRole);
                }
            }
        }

        private async Task CreateSystemUser()
        {
            const string password = "Password1!";
            const string name = "Administrator";

            if (!await userManager.Users.AnyAsync(p => p.UserName == name))
            {
                var applicationUser = await CreateIdentityUser(name, password);

                await userManager.AddToRoleAsync(applicationUser, Roles.ADMIN);
            }
        }


        /// <summary>
        /// Создание пользователя для авторизации
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<IdentityUser> CreateIdentityUser(string name, string password)
        {
            var applicationUser = new IdentityUser()
            {
                Email = name,
                UserName = name
            };

            var result = await userManager.CreateAsync(applicationUser);

            if (!string.IsNullOrEmpty(password))
                await userManager.AddPasswordAsync(applicationUser, password);
            return applicationUser;
        }
    }
}