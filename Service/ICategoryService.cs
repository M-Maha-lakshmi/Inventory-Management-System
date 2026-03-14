using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Service
{
    public interface ICategoryService
    {
        List<CategoryModel> GetCategories(int page, int pageSize);

        int GetTotalPages(int pageSize);

        CategoryModel GetCategoryById(int id);

        void AddCategory(CategoryModel category);

        void UpdateCategory(CategoryModel category);

        void DeleteCategory(int id);
    }
}
