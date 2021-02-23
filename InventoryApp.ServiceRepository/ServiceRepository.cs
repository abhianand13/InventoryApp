using AutoMapper;
using InventoryApp.Common;
using InventoryApp.Common.Models;
using InventoryApp.ServiceRepository.InventoryAppServiceReference;
using System.Collections.Generic;
using System.ServiceModel;

namespace InventoryApp.ServiceRepository
{
    public class ServiceRepository : IServiceRepository
    {
        public event QuantityUpdatedEventHandler ProductInventoryUpdated;

        public ServiceRepository()
        {

        }

        public List<ProductInventoryModel> LoadProductInventory()
        {
            var response = GetClient().LoadProductInventory();
            return GetMapper().Map<List<ProductInventoryModel>>(response);
        }

        public List<OrderModel> LoadOrders()
        {
            var response = GetClient().LoadOrders(UserSession.UserId);
            return GetMapper().Map<List<OrderModel>>(response);
        }

        public OrderModel CreateNewOrder()
        {
            var response = GetClient().CreateNewOrder(UserSession.UserId);
            return GetMapper().Map<OrderModel>(response);
        }

        public void AddToOrder(int productId, int orderId)
        {
            GetClient().AddToOrderAsync(UserSession.UserId, productId, orderId);
        }

        public IEnumerable<ProductInventoryModel> GetOrderProducts(int orderId)
        {
            var response = GetClient().GetOrderProducts(UserSession.UserId, orderId);
            return GetMapper().Map<List<ProductInventoryModel>>(response);
        }

        private void Callback_ProductInventoryUpdated(object sender, ProductInventoryUpdatedEventArgs e)
        {
            if (ProductInventoryUpdated != null)
            {
                ProductInventoryUpdated(this, e);
            }
        }

        private InventoryServiceClient GetClient()
        {
            var callback = new ServiceCallback();
            callback.ProductInventoryUpdated += Callback_ProductInventoryUpdated;
            return new InventoryServiceClient(new InstanceContext(callback));
        }

        private IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductInventoryDTO, ProductInventoryModel>();
                config.CreateMap<OrderDTO, OrderModel>();
            });
            return mapperConfig.CreateMapper();
        }
    }
}