using InventoryApp.Common;
using InventoryApp.Modules.Order.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace InventoryApp.Modules.Order
{
    public class OrderModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public OrderModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.OrdersRegion, nameof(OrderView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<OrderView>();
        }
    }
}