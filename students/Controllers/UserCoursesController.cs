using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using students.Data;
using students.Models;

namespace students.Controllers
{
    public class UserCoursesController : Controller
    {
        private readonly StudentContext _context;
        private readonly UserManager<userAuth> _UserManager;
        public UserCoursesController(StudentContext context, UserManager<userAuth> UserManager) { 
        _context = context;
            _UserManager = UserManager;
        }
        public IActionResult Index()
        {
            var courses = _context.UserCourses.Include(x => x.courses).Where(x => x.UserId == _UserManager.GetUserId(User)).ToList();
            return View(courses);
        }
    }
}
