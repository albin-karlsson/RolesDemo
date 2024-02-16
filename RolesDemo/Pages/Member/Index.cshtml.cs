using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RolesDemo.Pages.Member
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            bool adminRoleExists = !await _roleManager.RoleExistsAsync("Admin");

            // Om Admin-rollen inte existerar redan...
            if (!adminRoleExists)
            {
                // 1. Skapa ett Admin-roll objekt
                IdentityRole adminRole = new()
                {
                    Name = "Admin"
                };

                // 2. Lägg till den Admin-rollen i databasen
                var createAdminRoleResult = await _roleManager.CreateAsync(adminRole);

                if (createAdminRoleResult.Succeeded)
                {
                    adminRoleExists = true;
                }
            }

            if (adminRoleExists)
            {

            }
        }
    }
}
