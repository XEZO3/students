using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using students.Data;
using students.Models;
using students.Models.VM;

namespace students.Controllers
{
    public class UserController : Controller
    {
        private readonly StudentContext _context;
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(StudentContext context , SignInManager<userAuth> signInManager, UserManager<userAuth> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _UserManager = userManager;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(LoginVM login) 
        {
            var username = await _UserManager.FindByNameAsync(login.username);
            var signin = await _signInManager.PasswordSignInAsync(username, login.password,true,false);
            if (signin.Succeeded) {
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.error = "username or password is in correct";
                return View();
            }
            
        }
    }
}
