using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repo;
namespace InventoryManagementSystem.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepo _inventoryRepo;

        public InventoryService(IInventoryRepo inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        public List<InventoryItemModel> GetInventory(int page, int pageSize)
        {
            return _inventoryRepo.GetInventory(page, pageSize);
        }

        public int GetTotalPages(int pageSize)
        {
            int total = _inventoryRepo.GetInventoryCount();
            return (int)Math.Ceiling((double)total / pageSize);
        }

        public InventoryItemModel GetInventoryById(int id)
        {
            return _inventoryRepo.GetInventoryById(id);
        }

        public List<CategoryModel> GetCategories()
        {
            return _inventoryRepo.GetCategories();
        }

        public List<ProductModel> GetProducts()
        {
            return _inventoryRepo.GetProducts();
        }

        public void AddInventory(InventoryItemModel item)
        {
            _inventoryRepo.AddInventory(item);
        }

        public void UpdateInventory(InventoryItemModel item)
        {
            _inventoryRepo.UpdateInventory(item);
        }

        public void DeleteInventory(int id)
        {
            _inventoryRepo.DeleteInventory(id);
        }
    }
}
