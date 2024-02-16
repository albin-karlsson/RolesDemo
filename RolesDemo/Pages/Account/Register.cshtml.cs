using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RolesDemo.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public string? Username { get; set; }
        public string? Password { get; set; }

        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!string.IsNullOrEmpty(Username.Trim()) && !string.IsNullOrEmpty(Password.Trim()))
            {
                // 1. Skapa ett user-objekt

                IdentityUser newUser = new()
                {
                    UserName = Username
                };

                // 2. Lägg till den usern i databasen med användarnamn och lösenord

                var createUserResult = await _userManager.CreateAsync(newUser, Password);

                if (createUserResult.Succeeded)
                {
                    return RedirectToPage("/Account/Login");
                }
            }

            return Page();
        }
    }
}
