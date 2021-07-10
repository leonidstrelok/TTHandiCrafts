using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DSEU.UI.Areas.Identity.IdentityHostingStartup))]
namespace DSEU.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}