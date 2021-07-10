using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace TTHandiCrafts.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string ChangePassword => "ChangePassword";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string UserProfile => "Profile";

        public static string UserProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserProfile);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
