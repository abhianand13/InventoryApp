using InventoryApp.Service.DataStore;
using InventoryApp.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace InventoryApp.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class InventoryService : IInventoryService
    {
        public IInventoryServiceCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IInventoryServiceCallback>();
            }
        }

        public IEnumerable<ProductInventoryDTO> LoadProductInventory()
        {
            Thread.Sleep(1500);
            return DataStorage.ProductInventory;
        }

        public IEnumerable<OrderDTO> LoadOrders(Guid userId)
        {
            Thread.Sleep(1500);
            return DataStorage.Orders.Where(x => x.UserId == userId);
        }

        public OrderDTO CreateNewOrder(Guid userId)
        {
            int sequentialId = DataStorage.Orders.Any() ? DataStorage.Orders.Max(x => x.Id) + 1 : 1;
            var newOrder = new OrderDTO
            {
                Id = sequentialId,
                Name = "Order " + sequentialId,
                UserId = userId,
                Products = new List<ProductInventoryDTO>()
            };
            DataStorage.Orders.Add(newOrder);
            Thread.Sleep(1500);
            return newOrder;
        }

        public void AddToOrder(Guid userId, int productId, int orderId)
        {
            var productToAdd = DataStorage.Products.FirstOrDefault(x => x.Id == productId);
            var order = DataStorage.Orders.FirstOrDefault(x => x.Id == orderId && x.UserId == userId);
            var productInInventory = DataStorage.ProductInventory.FirstOrDefault(x => x.Product.Id == productId);

            if (productToAdd != null && order != null && productInInventory != null && productInInventory.Quantity > 0)
            {
                lock (productInInventory)
                {
                    var productInOrder = order.Products.FirstOrDefault(x => x.Product.Id == productId);
                    if (productInOrder != null)
                    {
                        productInOrder.Quantity++;
                    }
                    else
                    {
                        order.Products.Add(new ProductInventoryDTO { Product = new ProductDTO { Id = productId, Name = productToAdd.Name }, Quantity = 1 });
                    }

                    productInInventory.Quantity--;
                }
                Callback.HandleProductInventoryUpdated(productId, productInInventory.Quantity);
            }

            Thread.Sleep(1500);
        }

        public IEnumerable<ProductInventoryDTO> GetOrderProducts(Guid userId, int orderId)
        {
            Thread.Sleep(1500);
            var order = DataStorage.Orders.FirstOrDefault(x => x.Id == orderId && x.UserId == userId);
            if (order != null)
            {
                return order.Products;
            }
            return null;
        }
    }
}