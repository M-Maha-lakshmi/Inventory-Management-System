using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repo
{
    public interface IInventoryRepo
    {
        List<InventoryItemModel> GetInventory(int page, int pageSize);

        int GetInventoryCount();

        InventoryItemModel GetInventoryById(int id);

        List<CategoryModel> GetCategories();

        List<ProductModel> GetProducts();

        void AddInventory(InventoryItemModel item);

        void UpdateInventory(InventoryItemModel item);

        void DeleteInventory(int id);
    }
}
