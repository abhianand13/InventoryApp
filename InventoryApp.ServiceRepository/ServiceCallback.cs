using InventoryApp.ServiceRepository.InventoryAppServiceReference;
using System;

namespace InventoryApp.ServiceRepository
{
    public class ServiceCallback : IInventoryServiceCallback
    {
        public event QuantityUpdatedEventHandler ProductInventoryUpdated;

        void IInventoryServiceCallback.HandleProductInventoryUpdated(int productId, int quantity)
        {
            if (ProductInventoryUpdated != null)
            {
                ProductInventoryUpdated(this, new ProductInventoryUpdatedEventArgs(productId, quantity));
            }
        }
    }

    public delegate void QuantityUpdatedEventHandler(object sender, ProductInventoryUpdatedEventArgs e);

    public class ProductInventoryUpdatedEventArgs : EventArgs
    {
        private readonly int productId;
        private readonly int quantity;

        public ProductInventoryUpdatedEventArgs(int productId, int quantity)
        {
            this.productId = productId;
            this.quantity = quantity;
        }

        public int ProductId { get { return productId; } }
        public int Quantity { get { return quantity; } }
    }
}