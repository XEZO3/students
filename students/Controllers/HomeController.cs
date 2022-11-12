using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using students.Data;
using students.Models;
using System.Diagnostics;

namespace students.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentContext _context;
        private readonly SignInManager<userAuth> _signInManager;
        private readonly UserManager<userAuth> _UserManager;

        public HomeController(ILogger<HomeController> logger, StudentContext context, SignInManager<userAuth> signInManager, UserManager<userAuth> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _UserManager = userManager;
        }
        
        public IActionResult Index()
        {
            var count = 0;
            if (_signInManager.IsSignedIn(User))
            {
                var caritem = _context.Cart.Include(x => x.cartItem).FirstOrDefault(x => x.UserId == _UserManager.GetUserId(User));
                 count = (caritem == null) ? 0 :caritem.cartItem.Count();
            }
            List<Courses> courses = _context.Courses.OrderByDescending(X => X.Id).Take(3).ToList();
            
            HttpContext.Session.SetInt32("cart", (int)count);
            return View(courses);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}