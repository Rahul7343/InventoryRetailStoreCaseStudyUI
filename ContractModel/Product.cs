using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryUi.ContractModel
{
    public class Product
    {
        public long Id { get; set; }
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public long SupplierId { get; set; }
        public int StockQuantity { get; set; }

        [Required]
        public long CategoryId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
