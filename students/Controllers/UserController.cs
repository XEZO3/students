using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using students.Data;
using students.Models;
using students.Models.VM;
using System.Security.Claims;

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
            if (username != null)
            {
                var signin = await _signInManager.PasswordSignInAsync(username, login.password, true, false);
                if (signin.Succeeded)
                {
                   
                    return RedirectToAction("Index", "Home");  
                }
                else
                {
                    ViewBag.error = "username or password is in correct";
                    return View("Login", "User");
                }
            }
            else {
                ViewBag.error = "username or password is in correct";
                return View("Login", "User");
            }
            
        }
        [HttpGet]
        public IActionResult Register() {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(registerVM registerVM) {
            userAuth user = new userAuth()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
                EmailConfirmed = true,
                LockoutEnabled= true
            };
            var result = await _UserManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                await _UserManager.AddToRoleAsync(user, students.utility.utility.User_role);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("CustomError", error.Description);
                }
                return View();
            }
            return View();
        }
        public async Task<IActionResult> Logout() {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
