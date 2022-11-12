using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using students.Data;
using students.IServices;
using students.Models;
using students.Services;

namespace students.Controllers
{
    public class CoursesAdminController : Controller
    {
        private readonly StudentContext _context;
        private readonly IFile _file;
        public CoursesAdminController(StudentContext context,IFile files)
        {
            _context = context;
            _file = files;  
        }
        // GET: CoursesAdminController
        public ActionResult Index()
        {
            List<Courses> courses = _context.Courses.Include(x=>x.category).ToList();
            return View(courses);
        }

        // GET: CoursesAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CoursesAdminController/Create
        public ActionResult Create()
        {
            List<Category> category = _context.Categories.ToList();
            ViewBag.CategoryId = new SelectList(category, nameof(Category.Id), nameof(Category.Name));
            return View();
        }

       // POST: CoursesAdminController/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Create(Courses collection)
        {
            try
            {
                
                collection.CreatDate = DateTime.Now;
                var file = HttpContext.Request.Form.Files;
                collection.ImageUrl = _file.uploadfile(file);
                _context.Courses.Add(collection);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CoursesAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            Courses course = _context.Courses.FirstOrDefault(c => c.Id == id);
            List<Category> category = _context.Categories.ToList();
            ViewBag.CategoryId = new SelectList(category, nameof(Category.Id), nameof(Category.Name));
            return View(course);
        }

        // POST: CoursesAdminController/Edit/5
        [HttpPost]
       
        public ActionResult Edit(int id, Courses collection)
        {
            
                var file = HttpContext.Request.Form.Files;
                if (file.Count != 0) { 
                collection.ImageUrl= _file.uploadfile(file);
                }
                collection.UpdateDate = DateTime.Now;
                _context.Courses.Update(collection);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
           
        }

        // GET: CoursesAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            Courses courses = _context.Courses.FirstOrDefault(c => c.Id == id);
            _context.Courses.Remove(courses);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: CoursesAdminController/Delete/5
        
        
    }
}
