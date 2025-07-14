namespace Teste.Sales.Domain.Entities
{
    public class Sale
    {
        public Sale(string customerId, string customerName, string branchId, string branchName)
        {
            Id = Guid.NewGuid();
            SaleNumber = GenerateSaleNumber();
            Date = DateTime.UtcNow;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
            Items = new List<SaleItem>();
            Cancelled = false;
        }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string SaleNumber { get; private set; }
        public DateTime Date { get; private set; } = DateTime.UtcNow;
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string BranchId { get; private set; }
        public string BranchName { get; private set; }
        public List<SaleItem> Items { get; private set; } = new();
        public bool Cancelled { get; private set; }

        public decimal TotalAmount => Items.Where(i => !i.Cancelled).Sum(i => i.Total);

        public void AddItem(SaleItem item)
        {
            if (item.Quantity > 20)
                throw new Exception("Maximum 20 units per product");

            item.ApplyDiscount();
            Items.Add(item);
        }

        public void Cancel() => Cancelled = true;
        private string GenerateSaleNumber()
        {
            return $"SALE-{DateTime.UtcNow.Ticks}";
        }
        public void ClearItems()
        {
            Items.Clear();
        }
    }

}
