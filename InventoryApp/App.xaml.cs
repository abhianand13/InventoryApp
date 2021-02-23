using InventoryApp.Modules.Inventory;
using InventoryApp.Modules.Order;
using InventoryApp.ServiceRepository;
using InventoryApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IServiceRepository, InventoryApp.ServiceRepository.ServiceRepository>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<InventoryModule>();
            moduleCatalog.AddModule<OrderModule>();
        }
    }
}