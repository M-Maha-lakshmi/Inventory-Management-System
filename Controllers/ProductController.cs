using InventoryManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class ProductController : Controller
{
    private readonly InventoryDbContext _dbcontext;

    public ProductController(InventoryDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public IActionResult Index(int page = 1)
    {
        int pageSize = 10;
        var totalItems = _dbcontext.Products.Count();
        var products = _dbcontext.Products
                        .Include(p => p.Category)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        return View(products);
    }
    

    public IActionResult Create()
    {
        ViewBag.Categories = _dbcontext.Categories.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        var user = HttpContext.Session.GetString("User");
        if (user != null)
            product.CreatedBy = user;
        _dbcontext.Products.Add(product);
        _dbcontext.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        ViewBag.Categories = _dbcontext.Categories.ToList();

        var product = _dbcontext.Products.Find(id);

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        var user = HttpContext.Session.GetString("User");
        if (user != null)
            product.CreatedBy = user;
        _dbcontext.Products.Update(product);
        _dbcontext.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var product = _dbcontext.Products.Find(id);

        _dbcontext.Products.Remove(product);
        _dbcontext.SaveChanges();

        return RedirectToAction("Index");
    }
}