using InventoryApp.Common.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace InventoryApp.Common.Controls
{
    /// <summary>
    /// Interaction logic for OrdersControl.xaml
    /// </summary>
    public partial class OrdersControl : UserControl
    {
        public OrdersControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(OrdersControl), new PropertyMetadata("Orders"));

        public ObservableCollection<OrderModel> Orders
        {
            get { return (ObservableCollection<OrderModel>)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(ObservableCollection<OrderModel>), typeof(OrdersControl), new PropertyMetadata(new ObservableCollection<OrderModel>()));

        public OrderModel SelectedOrder
        {
            get { return (OrderModel)GetValue(SelectedOrderProperty); }
            set { SetValue(SelectedOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedOrderProperty =
            DependencyProperty.Register("SelectedOrder", typeof(OrderModel), typeof(OrdersControl), new PropertyMetadata(null));
    }
}