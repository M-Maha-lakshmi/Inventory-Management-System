using InventoryManagementSystem.Models;
namespace InventoryManagementSystem.Service
{
    public interface IInventoryService
    {
        List<InventoryItemModel> GetInventory(int page, int pageSize);

        int GetTotalPages(int pageSize);

        InventoryItemModel GetInventoryById(int id);

        List<CategoryModel> GetCategories();

        List<ProductModel> GetProducts();

        void AddInventory(InventoryItemModel item);

        void UpdateInventory(InventoryItemModel item);

        void DeleteInventory(int id);
    }
}
