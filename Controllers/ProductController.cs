using InventoryManagementSystem.Models;
using InventoryManagementSystem.Filters;
using InventoryManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;

[SessionAuthorize]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index(int page = 1)
    {
        if (HttpContext.Session.GetString("Username") == null)
        {
            TempData["Message"] = "Please login first!";
            return RedirectToAction("Login", "Account");
        }

        int pageSize = 10;

        var products = _productService.GetProducts(page, pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = _productService.GetTotalPages(pageSize);

        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _productService.GetCategories();
        return View();
    }

    [HttpPost]
    public IActionResult Create(ProductModel product)
    {
        var user = HttpContext.Session.GetString("Username");

        if (user != null)
            product.CreatedBy = user;

        _productService.AddProduct(product);

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        ViewBag.Categories = _productService.GetCategories();

        var product = _productService.GetProductById(id);

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(ProductModel product)
    {
        var user = HttpContext.Session.GetString("Username");

        if (user != null)
            product.CreatedBy = user;

        _productService.UpdateProduct(product);

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        _productService.DeleteProduct(id);

        return RedirectToAction("Index");
    }
}