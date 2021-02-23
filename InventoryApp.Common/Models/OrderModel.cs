using System;
using System.Collections.ObjectModel;

namespace InventoryApp.Common.Models
{
    public class OrderModel : NotificationObject
    {
        private ObservableCollection<ProductInventoryModel> products;
        
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public ObservableCollection<ProductInventoryModel> Products
        {
            get { return products; }
            set
            {
                products = value;
                RaisePropertyChanged(nameof(Products));
            }
        }
    }
}