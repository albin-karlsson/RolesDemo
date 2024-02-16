using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RolesDemo.Pages.Account
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ErrorMessage { get; set; }
        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!string.IsNullOrEmpty(Username?.Trim()) && !string.IsNullOrEmpty(Password?.Trim()))
            {
                // 1. Hitta användaren med rätt användarnamn

                IdentityUser? user = await _userManager.FindByNameAsync(Username);

                if (user != null)
                {
                    // 2. Testa att logga in med lösenordet

                    var signInResult = await _signInManager.PasswordSignInAsync(user, Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        // 3. Skicka vidare användaren till Member-sidan!

                        return RedirectToPage("/Member/Index");
                    }
                }
            }

            ErrorMessage = "Username or password was incorrect";

            return Page();
        }
    }
}
