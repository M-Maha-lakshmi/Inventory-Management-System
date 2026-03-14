using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repo;
using InventoryManagementSystem.Service;

public class ProductService : IProductService
{
    private readonly IProductRepo _productRepo;

    public ProductService(IProductRepo productRepo)
    {
        _productRepo = productRepo;
    }

    public List<ProductModel> GetProducts(int page, int pageSize)
    {
        return _productRepo.GetProducts(page, pageSize);
    }

    public int GetTotalPages(int pageSize)
    {
        int total = _productRepo.GetProductCount();
        return (int)Math.Ceiling((double)total / pageSize);
    }

    public List<CategoryModel> GetCategories()
    {
        return _productRepo.GetCategories();
    }

    public ProductModel GetProductById(int id)
    {
        return _productRepo.GetProductById(id);
    }

    public void AddProduct(ProductModel product)
    {
        _productRepo.AddProduct(product);
    }

    public void UpdateProduct(ProductModel product)
    {
        _productRepo.UpdateProduct(product);
    }

    public void DeleteProduct(int id)
    {
        _productRepo.DeleteProduct(id);
    }
}