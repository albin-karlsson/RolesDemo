using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RolesDemo.Pages.Member
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public bool IsAdmin { get; set; }

        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task OnGet()
        {
            // 1. H�mta nuvarande inloggade anv�ndaren
            IdentityUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            // 2. Kolla om den nuvarande inloggade anv�ndaren har en Admin-roll och s�tt IsAdmin-propertyn d�refter
            IsAdmin = await _userManager.IsInRoleAsync(loggedInUser, "Admin");
        }

        public async Task<IActionResult> OnPost()
        {
            bool adminRoleExists = await _roleManager.RoleExistsAsync("Admin");

            // Om Admin-rollen inte existerar redan...
            if (!adminRoleExists)
            {
                // 1. Skapa ett Admin-roll objekt
                IdentityRole adminRole = new()
                {
                    Name = "Admin"
                };

                // 2. L�gg till den Admin-rollen i databasen
                var createAdminRoleResult = await _roleManager.CreateAsync(adminRole);

                if (createAdminRoleResult.Succeeded)
                {
                    adminRoleExists = true;
                }
            }

            if (adminRoleExists)
            {
                // 3. H�mta den nuvarande inloggade anv�ndaren
                IdentityUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

                // 4. L�gg till Admin-rollen till den nuvarande inloggade anv�ndaren
                var addToAdminRoleResult = await _userManager.AddToRoleAsync(loggedInUser, "Admin");

                if (addToAdminRoleResult.Succeeded)
                {
                    return RedirectToPage("/Admin/Index");
                }
            }

            return Page();
        }
    }
}
