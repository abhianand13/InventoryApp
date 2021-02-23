using InventoryApp.Common;
using InventoryApp.Modules.Inventory.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace InventoryApp.Modules.Inventory
{
    public class InventoryModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public InventoryModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.InventoryRegion, nameof(InventoryView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<InventoryView>();
        }
    }
}