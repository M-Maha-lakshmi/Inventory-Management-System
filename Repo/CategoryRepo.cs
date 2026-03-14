using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly InventoryDbContext _context;

        public CategoryRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public List<CategoryModel> GetCategories(int page, int pageSize)
        {
            return _context.Categories
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                })
                .ToList();
        }

        public int GetCategoryCount()
        {
            return _context.Categories.Count();
        }

        public CategoryModel GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null) return null;

            return new CategoryModel
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CreatedAt = category.CreatedAt,
                CreatedBy = category.CreatedBy
            };
        }

        public void AddCategory(CategoryModel category)
        {
            var entity = new Category
            {
                CategoryName = category.CategoryName,
                CreatedBy = category.CreatedBy,
                CreatedAt = DateTime.Now
            };

            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateCategory(CategoryModel category)
        {
            var entity = _context.Categories.Find(category.Id);

            if (entity != null)
            {
                entity.CategoryName = category.CategoryName;
                entity.CreatedBy = category.CreatedBy;

                _context.Categories.Update(entity);
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            var entity = _context.Categories.Find(id);

            if (entity != null)
            {
                _context.Categories.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}