using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using WebApplication3.Model;
using Microsoft.AspNetCore.DataProtection;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private RoleManager<IdentityRole> roleManager { get; }


        [BindProperty]
        public Register RModel { get; set; }

        [BindProperty]
        public IFormFile? Resume { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, IWebHostEnvironment environment,

        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _environment = environment;
            this.roleManager = roleManager;
        }



        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Resume != null)
                {
                    if (Resume.Length > (2 * 1024 * 1024))
                    {
                        ModelState.AddModelError("Resume", "File size cannot exceed 2MB");
                    }

                    var uploadsFolder = "resumes";
                    var resumeFile = Guid.NewGuid() + Path.GetExtension(Resume.FileName);
                    var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, resumeFile);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await Resume.CopyToAsync(fileStream);
                    RModel.Resume = string.Format("/{0}/{1}", uploadsFolder, resumeFile);
                }

                IdentityRole role = await roleManager.FindByIdAsync("Member");
                if (role == null)
                {
                    IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Member"));
                    if (!result2.Succeeded)
                    {
                        ModelState.AddModelError("", "Create member role failed");
                    }
                }

                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("mykey");
                var user = new ApplicationUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FirstName = protector.Protect(RModel.FirstName),
                    LastName = protector.Protect(RModel.LastName),
                    Gender = protector.Protect(RModel.Gender),
                    NRIC = protector.Protect(RModel.NRIC),
                    Resume = protector.Protect(RModel.Resume),
                    BirthDate = protector.Protect(RModel.BirthDate.ToString()),
                    WhoamI = protector.Protect(RModel.WhoamI) 
                };

                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, "Member");
                    return RedirectToPage("/Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }







    }
}
