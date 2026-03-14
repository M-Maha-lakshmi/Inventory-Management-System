using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repo;
using InventoryManagementSystem.Service;
namespace InventoryManagementSystem.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public List<CategoryModel> GetCategories(int page, int pageSize)
        {
            return _categoryRepo.GetCategories(page, pageSize);
        }

        public int GetTotalPages(int pageSize)
        {
            int total = _categoryRepo.GetCategoryCount();
            return (int)Math.Ceiling((double)total / pageSize);
        }

        public CategoryModel GetCategoryById(int id)
        {
            return _categoryRepo.GetCategoryById(id);
        }

        public void AddCategory(CategoryModel category)
        {
            _categoryRepo.AddCategory(category);
        }

        public void UpdateCategory(CategoryModel category)
        {
            _categoryRepo.UpdateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            _categoryRepo.DeleteCategory(id);
        }
    }
}