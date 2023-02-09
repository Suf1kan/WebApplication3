using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;
using WebApplication3.ViewModels;
using WebApplication3.Services;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        private MemberService _memberService { get; }

        private static readonly HttpClient client = new HttpClient();

        [BindProperty]
        public Login LModel { get; set; }

        public LoginModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        MemberService memberService, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _memberService = memberService;

        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, true);
                if (identityResult.Succeeded)
                {
                    var userNRIC = _memberService.GetApplicationUserByEmail(LModel.Email).NRIC;

                    HttpContext.Session.SetString("NRIC", userNRIC.ToString());
                    return RedirectToPage("/Members/Details");
                }
                else if (identityResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is locked out. Kindly wait for 10 minutes and try again");
                }

                else
                {
                    ModelState.AddModelError("", "Username or Password incorrect");
                }
            }
            return Page();
        }
    }
}
