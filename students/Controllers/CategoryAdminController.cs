using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using students.Data;
using students.Models;

namespace students.Controllers
{
    public class CategoryAdminController : Controller
    {
        // GET: CategoryAdminController
        private readonly StudentContext _context;

        public CategoryAdminController(StudentContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            List<Category> category = _context.Categories.ToList();
            return View(category);
        }

        // GET: CategoryAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category collection)
        {
            try
            {
                collection.CreatDate = DateTime.Now;
                _context.Categories.Add(collection);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = _context.Categories.Find(id);
            return View(category);
        }

        // POST: CategoryAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category collection)
        {
            try
            {
                collection.UpdateDate=DateTime.Now;
                _context.Categories.Update(collection);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: CategoryAdminController/Delete/5
        
    }
}
