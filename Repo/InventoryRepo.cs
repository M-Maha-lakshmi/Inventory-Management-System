using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repo
{
    public class InventoryRepo : IInventoryRepo
    {
        private readonly InventoryDbContext _dbcontext;

        public InventoryRepo(InventoryDbContext context)
        {
            _dbcontext = context;
        }

        public List<InventoryItemModel> GetInventory(int page, int pageSize)
        {
            return _dbcontext.InventoryItems
                .Include(i => i.Product)
                .ThenInclude(p => p.Category)
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new InventoryItemModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Product = x.Product,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                })
                .ToList();
        }

        public int GetInventoryCount()
        {
            return _dbcontext.InventoryItems.Count();
        }

        public InventoryItemModel GetInventoryById(int id)
        {
            var item = _dbcontext.InventoryItems
                .Include(i => i.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefault(x => x.Id == id);

            if (item == null) return null;

            return new InventoryItemModel
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                CreatedBy = item.CreatedBy,
                CreatedAt = item.CreatedAt
            };
        }

        public List<CategoryModel> GetCategories()
        {
            return _dbcontext.Categories
                .Select(c => new CategoryModel
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                })
                .ToList();
        }

        public List<ProductModel> GetProducts()
        {
            return _dbcontext.Products
                .Select(p => new ProductModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    CategoryId = p.CategoryId
                })
                .ToList();
        }

        public void AddInventory(InventoryItemModel item)
        {
            var entity = new InventoryItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
                CreatedBy = item.CreatedBy,
                CreatedAt = DateTime.Now
            };

            _dbcontext.InventoryItems.Add(entity);
            _dbcontext.SaveChanges();
        }

        public void UpdateInventory(InventoryItemModel item)
        {
            var entity = _dbcontext.InventoryItems.Find(item.Id);

            if (entity != null)
            {
                entity.ProductId = item.ProductId;
                entity.Quantity = item.Quantity;
                entity.Price = item.Price;
                entity.CreatedBy = item.CreatedBy;

                _dbcontext.InventoryItems.Update(entity);
                _dbcontext.SaveChanges();
            }
        }

        public void DeleteInventory(int id)
        {
            var entity = _dbcontext.InventoryItems.Find(id);

            if (entity != null)
            {
                _dbcontext.InventoryItems.Remove(entity);
                _dbcontext.SaveChanges();
            }
        }
    }
}