using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Model
{
    public class ProductModel
    {
            public int Id { get; set; }

            public string ProductName { get; set; }

            public int CategoryId { get; set; }

            public CategoryModel Category { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;

            public string CreatedBy { get; set; }
    }
}
