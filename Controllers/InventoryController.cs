using InventoryManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class InventoryController : Controller
{
    private readonly InventoryDbContext _dbcontext;

    public InventoryController(InventoryDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public IActionResult Index(int page = 1)
    {
        int pageSize = 10;
        var totalItems = _dbcontext.InventoryItems.Count();
        var items = _dbcontext.InventoryItems
                    .Include(i => i.Product)
                    .ThenInclude(p => p.Category)
                    .OrderByDescending(r => r.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        return View(items);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _dbcontext.Categories.ToList();
        ViewBag.Products = _dbcontext.Products.ToList();

        return View();
    }

    [HttpPost]
    public IActionResult Create(InventoryItem item)
    {
        var user = HttpContext.Session.GetString("User");
        if (user != null)
            item.CreatedBy = user;
        _dbcontext.InventoryItems.Add(item);
        _dbcontext.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var item = _dbcontext.InventoryItems.Find(id);

        ViewBag.Categories = _dbcontext.Categories.ToList();
        ViewBag.Products = _dbcontext.Products.ToList();

        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(InventoryItem item)
    {
        var user = HttpContext.Session.GetString("User");
        if (user != null)
            item.CreatedBy = user;
        _dbcontext.InventoryItems.Update(item);
        _dbcontext.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var item = _dbcontext.InventoryItems.Find(id);

        _dbcontext.InventoryItems.Remove(item);
        _dbcontext.SaveChanges();

        return RedirectToAction("Index");
    }
}