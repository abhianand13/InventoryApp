using Prism.Events;
using Prism.Regions;

namespace InventoryApp.Common.ViewModels
{
    public class ViewModelBase : NotificationObject, INavigationAware
    {
        protected IRegionManager RegionManager { get; private set; }

        public ViewModelBase(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
        }

        protected IEventAggregator EventAggregator { get; private set; }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}