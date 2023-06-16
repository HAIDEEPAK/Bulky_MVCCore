using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //_db.Categories.Add(obj);
            //_db.SaveChanges();
            if (obj.Name != null && obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder can't exactly match the Name");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value.");
            }
            if (ModelState.IsValid) //Check the server side validations inside the Category model class file
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData[("success")] = "Category created succesfully.";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.CategoryId==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.CategoryId==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            //var cat=_db.Categories.FirstOrDefault(u => u.CategoryId == obj.CategoryId);
            //if (cat!=null)
            //{
            //    cat.Name = obj.Name;
            //    cat.DisplayOrder=obj.DisplayOrder;
            //    _db.SaveChanges();
            //    return RedirectToAction("Index", "Category");
            //}
            //obj.CategoryId = 1014;
            if (ModelState.IsValid) //Check the server side validations inside the Category model class file
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData[("success")] = "Category updated succesfully.";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.CategoryId==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.CategoryId==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData[("success")] = "Category deleted succesfully.";
            return RedirectToAction("Index", "Category");
        }
    }
}
