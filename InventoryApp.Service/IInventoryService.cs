using InventoryApp.Service.DTO;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace InventoryApp.Service
{
    [ServiceContract(CallbackContract = typeof(IInventoryServiceCallback))]
    public interface IInventoryService
    {
        [OperationContract]
        IEnumerable<ProductInventoryDTO> LoadProductInventory();

        [OperationContract]
        IEnumerable<OrderDTO> LoadOrders(Guid userId);

        [OperationContract]
        OrderDTO CreateNewOrder(Guid userId);

        [OperationContract(IsOneWay = true)]
        void AddToOrder(Guid userId, int productId, int orderId);

        [OperationContract]
        IEnumerable<ProductInventoryDTO> GetOrderProducts(Guid userId, int orderId);
    }

    public interface IInventoryServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void HandleProductInventoryUpdated(int productId, int quantity);
    }
}