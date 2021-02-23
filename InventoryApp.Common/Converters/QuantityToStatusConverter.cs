using System;
using System.Globalization;
using System.Windows.Data;

namespace InventoryApp.Common.Converters
{
    public class QuantityToStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int quantity = (int)value;
            if (quantity == 0)
            {
                return "Out of Stock";
            }
            return quantity < 5 ? "Low Stock" : "In Stock";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}