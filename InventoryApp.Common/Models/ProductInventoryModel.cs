namespace InventoryApp.Common.Models
{
    public class ProductInventoryModel : NotificationObject
    {
        private int quantity;

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }
    }
}