using Microsoft.AspNetCore.Mvc;
using ProductManegmentSystem.Context;
using ProductManegmentSystem.Models;

namespace ProductManegmentSystem.Controllers
{
    public class CategoryController : Controller
    {
        ProductManegmentContext db = new ProductManegmentContext();

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Id") == null)
            {
                return RedirectToAction("Login", "User");
            }
            var categories = db.Categories;
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            ModelState.Remove("Products");
            if (!ModelState.IsValid)
            {
                return View();
            }

            db.Categories.Add(newCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var singleCategory = db.Categories.FirstOrDefault(c => c.Id == id);
            if (singleCategory == null)
            {
                return RedirectToAction("Index");
            }
            return View(singleCategory);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var singleCategory = db.Categories.Find(id);
            if (singleCategory == null)
            {
                return RedirectToAction("Index");
            }
            return View(singleCategory);
        }

        [HttpPost]
        public IActionResult Delete(int id, string confirm)
        {
            if (id == 0)
            {
                return RedirectToAction("index");
            }
            var singleCategory = db.Categories.Find(id);
            if (singleCategory == null)
            {
                return RedirectToAction("Index");
            }
            db.Remove(singleCategory);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("index");
            }
            var singleCategory = db.Categories.Find(id);
            if (singleCategory == null)
            {
                return RedirectToAction("Index");
            }
            return View(singleCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category newCategory)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var oldCategory=db.Categories.Find(newCategory.Id);
            oldCategory.Name= newCategory.Name;
            oldCategory.Description= newCategory.Description;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
