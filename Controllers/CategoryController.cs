using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    using InventoryManagementSystem.Entities;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        private readonly InventoryDbContext _dbcontext;

        public CategoryController(InventoryDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;
            var totalItems = _dbcontext.Categories.Count();
            var categories = _dbcontext.Categories.OrderByDescending(r => r.CreatedAt)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            var user = HttpContext.Session.GetString("User");
            if (user != null)
                category.CreatedBy = user;
            _dbcontext.Categories.Add(category);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var cat = _dbcontext.Categories.Find(id);
            return View(cat);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var user = HttpContext.Session.GetString("User");
            if (user != null)
                category.CreatedBy = user;
            _dbcontext.Categories.Update(category);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var cat = _dbcontext.Categories.Find(id);
            _dbcontext.Categories.Remove(cat);
            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
