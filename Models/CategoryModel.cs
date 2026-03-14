namespace InventoryManagementSystem.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<ProductModel> Products { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }
    }
}
