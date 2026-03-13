namespace InventoryManagementSystem.Models
{
    public class InventoryItemModel
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
