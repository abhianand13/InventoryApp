using InventoryApp.Common;
using InventoryApp.Common.Models;
using InventoryApp.Common.ViewModels;
using InventoryApp.ServiceRepository;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventoryApp.Modules.Order.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        #region Private Fields
        private readonly IServiceRepository serviceRepository;
        private OrderModel selectedOrder;
        private ProductInventoryModel selectedProduct;
        private ICommand newOrderCommand;
        #endregion

        #region Constructor
        public OrderViewModel(IServiceRepository serviceRepository, IRegionManager regionManager, IEventAggregator eventAggregator)
            : base(regionManager, eventAggregator)
        {
            this.serviceRepository = serviceRepository;
            EventAggregator.GetEvent<OrderUpdated>().Subscribe(OnOrderUpdated);

            OrdersList = new ObservableCollection<OrderModel>();
        }
        #endregion

        #region Properties
        public ObservableCollection<OrderModel> OrdersList { get; set; }

        public OrderModel SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                RaisePropertyChanged(nameof(SelectedOrder));
                RaisePropertyChanged(nameof(IsOrderSelected));
            }
        }

        public ProductInventoryModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                RaisePropertyChanged(nameof(SelectedProduct));
            }
        }

        public ICommand NewOrderCommand
        {
            get
            {
                if (newOrderCommand == null)
                {
                    newOrderCommand = new DelegateCommand(param => CreateNewOrder());
                }
                return newOrderCommand;
            }
        }

        public string Title => "Orders";

        public bool IsOrderSelected => SelectedOrder != null;
        #endregion

        #region Public Methods
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadOrders();
        }
        #endregion

        #region Private Methods
        private async void LoadOrders()
        {
            OrdersList.Clear();
            List<OrderModel> orders = null;
            await Task.Run(() =>
            {
                orders = serviceRepository.LoadOrders();
            });
            OrdersList.AddRange(orders);
        }

        private async void CreateNewOrder()
        {
            OrderModel newOrder = null;

            await Task.Run(() =>
            {
                newOrder = serviceRepository.CreateNewOrder();
            });

            if (newOrder != null)
            {
                OrdersList.Add(newOrder);
                SelectedOrder = newOrder;
                EventAggregator.GetEvent<OrderCreated>().Publish();
            }
        }

        private async void OnOrderUpdated(int orderId)
        {
            List<ProductInventoryModel> updatedProducts = null;

            await Task.Run(() =>
            {
                updatedProducts = serviceRepository.GetOrderProducts(orderId)?.ToList();
            });

            var currentOrder = OrdersList.FirstOrDefault(x => x.Id == orderId);

            if (updatedProducts != null && currentOrder != null)
            {
                currentOrder.Products = new ObservableCollection<ProductInventoryModel>(updatedProducts);
            }
        }
        #endregion
    }
}