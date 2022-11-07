using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using students.Data;
using students.IServices;
using students.Models;

namespace students.Services
{
    public class DbInit : IDbInit
    {
        private readonly StudentContext _context;
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInit(RoleManager<IdentityRole> roleManager, StudentContext context, SignInManager<userAuth> signInManager, UserManager<userAuth> UserManager) { 
        
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _UserManager = UserManager;
        }
        public async void Init() {
            if (_context.Database.GetPendingMigrations().Count()>0)
            {
                _context.Database.Migrate();
            }
            if (!_UserManager.Users.Any()) 
            {
               await _roleManager.CreateAsync(new IdentityRole(utility.utility.Admin_role));
               await _roleManager.CreateAsync(new IdentityRole(utility.utility.User_role));
                
                userAuth user = new userAuth()
                {
                    Email = "admin@admin.com",
                    UserName = "superAdmin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PhoneNumber = "0788129088",
                    LockoutEnabled = true,
                };
              await  _UserManager.CreateAsync(user, "Test@1234");
              await  _UserManager.AddToRoleAsync(user,utility.utility.Admin_role);
            }
            if (!_context.Courses.Any()) {
                Courses courses = new Courses()
                {
                    CreatDate = DateTime.Now,
                    Price= 200,
                    Duration = 5,
                    Description = "full stack development",
                    Name ="web development"
                };
                _context.Courses.Add(courses);
                _context.SaveChanges();
            }
        }
    }
}
