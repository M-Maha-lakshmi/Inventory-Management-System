using InventoryManagementSystem.Entities;

namespace InventoryManagementSystem.Models
{
    public class InventoryItemModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }
    }
}
