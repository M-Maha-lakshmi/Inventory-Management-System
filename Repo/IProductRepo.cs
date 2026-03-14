using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repo
{
    public interface IProductRepo
    {
        List<ProductModel> GetProducts(int page, int pageSize);
        List<CategoryModel> GetCategories();
        ProductModel GetProductById(int id);
        void AddProduct(ProductModel product);
        void UpdateProduct(ProductModel product);
        void DeleteProduct(int id);
        int GetProductCount();
    }
}
