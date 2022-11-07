using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using students.Data;
using students.Models;

namespace students.Controllers
{
    public class UserAdminController : Controller
    {
        private readonly StudentContext _context;
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;

        public UserAdminController(StudentContext context, SignInManager<userAuth> signInManager, UserManager<userAuth> UserManager) {
            _context = context;
            _signInManager = signInManager;
            _UserManager = UserManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = _UserManager.Users.ToList();
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActiveDeactive(string Id) {
            var user = await _UserManager.FindByIdAsync(Id);
            if (user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = null;
               
            }
            else {
                user.LockoutEnd = DateTime.Now.AddMinutes(30);
            }
           await _UserManager.UpdateAsync(user);
            return RedirectToAction("index");
        }
    }
}
