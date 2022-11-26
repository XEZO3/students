using Microsoft.AspNetCore.Mvc;
using students.Data;

namespace students.Controllers
{
    public class CoursesVideoController : Controller
    {
        private readonly StudentContext _context;
        public CoursesVideoController(StudentContext context)
        {
            _context = context;
        }
        public IActionResult Index(int courseId)
        {
            var videos = _context.CoursesVideo.ToList();
            return View(videos);
        }
    }
}
