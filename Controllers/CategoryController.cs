using InventoryManagementSystem.Models;
using InventoryManagementSystem.Filters;
using InventoryManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [SessionAuthorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(int page = 1)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                TempData["Message"] = "Please login first!";
                return RedirectToAction("Login", "Account");
            }

            int pageSize = 10;

            var categories = _categoryService.GetCategories(page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = _categoryService.GetTotalPages(pageSize);

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryModel category)
        {
            var user = HttpContext.Session.GetString("Username");

            if (user != null)
                category.CreatedBy = user;

            _categoryService.AddCategory(category);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel category)
        {
            var user = HttpContext.Session.GetString("Username");

            if (user != null)
                category.CreatedBy = user;

            _categoryService.UpdateCategory(category);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return RedirectToAction("Index");
        }
    }
}