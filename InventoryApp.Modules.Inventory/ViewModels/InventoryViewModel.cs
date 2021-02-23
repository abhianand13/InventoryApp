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

namespace InventoryApp.Modules.Inventory.ViewModels
{
    public class InventoryViewModel : ViewModelBase
    {
        #region Private Fields
        private readonly IServiceRepository serviceRepository;
        private ProductInventoryModel selectedProduct;
        private OrderModel selectedOrder;
        private ICommand addToOrderCommand;
        private ICommand refreshCommand;
        #endregion

        #region Constructor
        public InventoryViewModel(IServiceRepository serviceRepository, IRegionManager regionManager, IEventAggregator eventAggregator)
            : base(regionManager, eventAggregator)
        {
            this.serviceRepository = serviceRepository;
            this.serviceRepository.ProductInventoryUpdated += ProductInventoryUpdated;
            EventAggregator.GetEvent<OrderCreated>().Subscribe(LoadOrders);

            ProductsList = new ObservableCollection<ProductInventoryModel>();
            OrdersList = new ObservableCollection<OrderModel>();
        }
        #endregion

        #region Properties
        public ObservableCollection<ProductInventoryModel> ProductsList { get; set; }

        public ObservableCollection<OrderModel> OrdersList { get; set; }

        public ProductInventoryModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                RaisePropertyChanged(nameof(SelectedProduct));
            }
        }

        public OrderModel SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                RaisePropertyChanged(nameof(SelectedOrder));
                RaisePropertyChanged(nameof(CanAddToOrder));
            }
        }

        public string Title => "Inventory";

        public ICommand AddToOrderCommand
        {
            get
            {
                if (addToOrderCommand == null)
                {
                    addToOrderCommand = new DelegateCommand(param => AddToOrder(), param => CanAddToOrder());
                }
                return addToOrderCommand;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new DelegateCommand(param => LoadProducts());
                }
                return refreshCommand;
            }
        }
        #endregion

        #region Public Methods
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadProducts();
            LoadOrders();
        }
        #endregion

        #region Private Methods
        private async void LoadProducts()
        {
            ProductsList.Clear();
            List<ProductInventoryModel> products = null;
            await Task.Run(() =>
            {
                products = serviceRepository.LoadProductInventory();
            });
            ProductsList.AddRange(products);
        }

        private async void LoadOrders()
        {
            OrdersList.Clear();
            List<OrderModel> orders = null;
            await Task.Run(() =>
            {
                orders = serviceRepository.LoadOrders();
            });
            OrdersList.AddRange(orders);
            SelectedOrder = OrdersList.LastOrDefault();
        }

        private bool CanAddToOrder()
        {
            return SelectedProduct != null && SelectedProduct.Quantity > 0 && SelectedOrder != null;
        }

        private async void AddToOrder()
        {
            if (CanAddToOrder())
            {
                await Task.Run(() =>
                {
                    serviceRepository.AddToOrder(SelectedProduct.ProductId, SelectedOrder.Id);
                });
                EventAggregator.GetEvent<OrderUpdated>().Publish(SelectedOrder.Id);
            }
        }

        private void ProductInventoryUpdated(object sender, ProductInventoryUpdatedEventArgs e)
        {
            if (e != null)
            {
                var product = ProductsList.FirstOrDefault(x => x.ProductId == e.ProductId);
                product.Quantity = e.Quantity;
            }
        }
        #endregion
    }
}