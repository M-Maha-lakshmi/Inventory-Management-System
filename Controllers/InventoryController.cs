using InventoryManagementSystem.Models;
using InventoryManagementSystem.Filters;
using InventoryManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;

[SessionAuthorize]
public class InventoryController : Controller
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public IActionResult Index(int page = 1)
    {
        if (HttpContext.Session.GetString("Username") == null)
        {
            TempData["Message"] = "Please login first!";
            return RedirectToAction("Login", "Account");
        }

        int pageSize = 10;

        var items = _inventoryService.GetInventory(page, pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = _inventoryService.GetTotalPages(pageSize);

        return View(items);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _inventoryService.GetCategories();
        ViewBag.Products = _inventoryService.GetProducts();

        return View();
    }

    [HttpPost]
    public IActionResult Create(InventoryItemModel item)
    {
        var user = HttpContext.Session.GetString("Username");

        if (user != null)
            item.CreatedBy = user;

        _inventoryService.AddInventory(item);

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var item = _inventoryService.GetInventoryById(id);

        ViewBag.Categories = _inventoryService.GetCategories();
        ViewBag.Products = _inventoryService.GetProducts();

        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(InventoryItemModel item)
    {
        var user = HttpContext.Session.GetString("Username");

        if (user != null)
            item.CreatedBy = user;

        _inventoryService.UpdateInventory(item);

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        _inventoryService.DeleteInventory(id);

        return RedirectToAction("Index");
    }
}