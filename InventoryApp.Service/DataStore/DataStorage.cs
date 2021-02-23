using InventoryApp.Service.DTO;
using System.Collections.Generic;

namespace InventoryApp.Service.DataStore
{
    public class DataStorage
    {
        private static List<ProductDTO> products;
        public static List<ProductDTO> Products
        {
            get
            {
                if (products == null)
                {
                    products = new List<ProductDTO>
                    {
                        new ProductDTO { Id = 1, Name = "Product 1" },
                        new ProductDTO { Id = 2, Name = "Product 2" },
                        new ProductDTO { Id = 3, Name = "Product 3" },
                        new ProductDTO { Id = 4, Name = "Product 4" }
                    };
                }
                return products;
            }
        }

        public static List<ProductInventoryDTO> ProductInventory = new List<ProductInventoryDTO>
        {
            new ProductInventoryDTO { Product = Products[0], Quantity = 5 },
            new ProductInventoryDTO { Product = Products[1], Quantity = 3 },
            new ProductInventoryDTO { Product = Products[2], Quantity = 10 },
            new ProductInventoryDTO { Product = Products[3], Quantity = 7 }
        };

        public static List<OrderDTO> Orders = new List<OrderDTO>();
    }
}