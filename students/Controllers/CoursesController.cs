using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using students.Data;
using students.Models;
using students.Models.VM;

namespace students.Controllers
{
    public class CoursesController : Controller
    {
        private readonly StudentContext _context;
        public CoursesController(StudentContext context) { 
        _context = context;
        }
        public IActionResult Index(string name, bool isajax)
        {
            List<Category> category = _context.Categories.ToList();
            ViewBag.CategoryId = new SelectList(category, nameof(Category.Id), nameof(Category.Name));
            List<Courses> courses = _context.Courses.Include(x=>x.category).Where(x => (string.IsNullOrEmpty(name) || x.Name.Contains(name))).ToList();
            if (isajax)
            {
                return PartialView("_partialcourses", courses);
            }
            else {
                return View(courses);
            }

            
        }
        public ActionResult Details(int id) {
            Courses course = _context.Courses.FirstOrDefault(c => c.Id == id);
            return View(course);
        }
        
    }
}
