using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Model;
using WebApplication3.Services;

namespace WebApplication3.Services
{
    public class MemberService
    {

        private readonly AuthDbContext _context;

        public MemberService(AuthDbContext context)
        {
            _context = context;
        }

        public List<ApplicationUser> GetAll()
        {
            return _context.Memberships.OrderBy(x => x.NRIC).ToList();
        }

        public ApplicationUser? GetApplicationUserByEmail(string email)
        {
            ApplicationUser? ApplicationUser = _context.Memberships.FirstOrDefault(x => x.Email.Equals(email));
            return ApplicationUser;
        }

        public ApplicationUser? GetApplicationUserByNRIC(string NRIC)
        {
            ApplicationUser? ApplicationUser = _context.Memberships.FirstOrDefault(x => x.NRIC.Equals(NRIC));
            return ApplicationUser;
        }

        public void AddMember(ApplicationUser member)
        {
            _context.Memberships.Add(member);
            _context.SaveChanges();
        }

        public void UpdateMember(ApplicationUser Member)
        {
            _context.Memberships.Update(Member);
            _context.SaveChanges();
        }

        public void DeleteMember(ApplicationUser Member)
        {
            _context.Memberships.Remove(Member);
            _context.SaveChanges();
        }

    }


}
