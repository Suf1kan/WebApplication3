using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using WebApplication3.Services;
using WebApplication3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;


namespace WebApplication3.Pages.Members
{
    [Authorize(Roles = "Member")]

    public class DetailsModel : PageModel
    {
        
        public MemberService _memberService { get; set; }

        private SignInManager<ApplicationUser> signInManager { get; }
        public DetailsModel(SignInManager<ApplicationUser> SignInManager, MemberService memberService)
        {
            _memberService = memberService;
            signInManager = SignInManager;
        }


        public void OnGet()
        {
        }
    }
}
