namespace InventoryUi.ContractModel
{
    public class Supplier
    {
        public long Id { get; set; }

        public string SupplierName { get; set; }

        public string MobileNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
