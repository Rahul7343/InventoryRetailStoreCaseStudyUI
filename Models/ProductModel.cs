using System.ComponentModel.DataAnnotations;

namespace InventoryUi.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }
        public int StockQuantity { get; set; }
        public long SupplierId { get; set; }
        public SupplierModel? Supplier { get; set; }
        public long CategoryId { get; set; }
        public CategoryModel? Category { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
