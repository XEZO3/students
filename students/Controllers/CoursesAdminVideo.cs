using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using students.Data;
using students.Models;

namespace students.Controllers
{
    public class CoursesAdminVideo : Controller
    {
        // GET: CoursesAdminVideo
        private readonly StudentContext _context;
        public CoursesAdminVideo(StudentContext context) { 
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var videos = _context.CoursesVideo.ToList();
            return View(videos);
        }

        // GET: CoursesAdminVideo/Details/5
       

        // GET: CoursesAdminVideo/Create
        public ActionResult Create()
        {
            var courses = _context.Courses.ToList();
            ViewBag.CoursesId = new SelectList(courses, nameof(Courses.Id), nameof(Courses.Name));
            return View();
        }

        // POST: CoursesAdminVideo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoursesVideo collection)
        {
            collection.CreatDate = DateTime.Now;
             _context.CoursesVideo.Add(collection);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CoursesAdminVideo/Edit/5
        public ActionResult Edit(int id)
        {
            var courses = _context.Courses.ToList();

            ViewBag.CoursesId = new SelectList(courses, nameof(Courses.Id), nameof(Courses.Name));
            var coursesVideo = _context.CoursesVideo.FirstOrDefault(x => x.Id == id);
            return View(coursesVideo);
        }

        // POST: CoursesAdminVideo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CoursesVideo collection)
        {
            var coursevideo = _context.CoursesVideo.Update(collection);
            return RedirectToAction("Index");
        }

        // GET: CoursesAdminVideo/Delete/5
        public ActionResult Delete(int id)
        {
            var video = _context.CoursesVideo.FirstOrDefault(x => x.Id == id);
            _context.CoursesVideo.Remove(video);
            return RedirectToAction("Index");
        }

        // POST: CoursesAdminVideo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        
    }
}
