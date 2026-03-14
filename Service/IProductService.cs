using InventoryManagementSystem.Models;
namespace InventoryManagementSystem.Service
{
    public interface IProductService
    {
        List<ProductModel> GetProducts(int page, int pageSize);
        int GetTotalPages(int pageSize);
        List<CategoryModel> GetCategories();
        ProductModel GetProductById(int id);
        void AddProduct(ProductModel product);
        void UpdateProduct(ProductModel product);
        void DeleteProduct(int id);
    }
}
