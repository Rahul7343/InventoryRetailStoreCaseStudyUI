using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryUi.ContractModel
{
    public class Sales
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
