using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repo
{
    public interface ICategoryRepo
    {
        List<CategoryModel> GetCategories(int page, int pageSize);

        int GetCategoryCount();

        CategoryModel GetCategoryById(int id);

        void AddCategory(CategoryModel category);

        void UpdateCategory(CategoryModel category);

        void DeleteCategory(int id);
    }
}