namespace InventoryUi.Models
{
    public class SalesModel
    {
        public long Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public SupplierModel? Supplier { get; set; }
        public ProductModel? Product { get; set; }
        public CustomerModel? Customer { get; set; }

    }
}
