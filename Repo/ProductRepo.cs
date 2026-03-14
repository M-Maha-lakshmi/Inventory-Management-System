using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repo;
using Microsoft.EntityFrameworkCore;

public class ProductRepo : IProductRepo
{
    private readonly InventoryDbContext _context;

    public ProductRepo(InventoryDbContext context)
    {
        _context = context;
    }

    public List<ProductModel> GetProducts(int page, int pageSize)
    {
        return _context.Products
            .Include(p => p.Category)
            .OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ProductModel
            {
                Id = x.Id,
                ProductName = x.ProductName,
                CategoryId = x.CategoryId,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                Category = new CategoryModel
                {
                    Id = x.Category.Id,
                    CategoryName = x.Category.CategoryName
                }
            })
            .ToList();
    }

    public int GetProductCount()
    {
        return _context.Products.Count();
    }

    public List<CategoryModel> GetCategories()
    {
        return _context.Categories
            .Select(c => new CategoryModel
            {
                Id = c.Id,
                CategoryName = c.CategoryName
            })
            .ToList();
    }

    public ProductModel GetProductById(int id)
    {
        var product = _context.Products
            .Include(p => p.Category)
            .FirstOrDefault(x => x.Id == id);

        if (product == null) return null;

        return new ProductModel
        {
            Id = product.Id,
            ProductName = product.ProductName,
            CategoryId = product.CategoryId,
            CreatedBy = product.CreatedBy,
            CreatedAt = product.CreatedAt
        };
    }

    public void AddProduct(ProductModel product)
    {
        var entity = new Product
        {
            ProductName = product.ProductName,
            CategoryId = product.CategoryId,
            CreatedBy = product.CreatedBy,
            CreatedAt = DateTime.Now
        };

        _context.Products.Add(entity);
        _context.SaveChanges();
    }

    public void UpdateProduct(ProductModel product)
    {
        var entity = _context.Products.Find(product.Id);

        if (entity != null)
        {
            entity.ProductName = product.ProductName;
            entity.CategoryId = product.CategoryId;
            entity.CreatedBy = product.CreatedBy;

            _context.Products.Update(entity);
            _context.SaveChanges();
        }
    }

    public void DeleteProduct(int id)
    {
        var entity = _context.Products.Find(id);

        if (entity != null)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }
    }
}