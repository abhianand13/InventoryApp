using InventoryApp.Common.Models;
using System.Collections.Generic;

namespace InventoryApp.ServiceRepository
{
    public interface IServiceRepository
    {
        event QuantityUpdatedEventHandler ProductInventoryUpdated;

        List<ProductInventoryModel> LoadProductInventory();

        List<OrderModel> LoadOrders();

        OrderModel CreateNewOrder();

        void AddToOrder(int productId, int orderId);

        IEnumerable<ProductInventoryModel> GetOrderProducts(int orderId);
    }
}