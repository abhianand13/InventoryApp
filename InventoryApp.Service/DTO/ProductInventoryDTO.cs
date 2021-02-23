using System.Runtime.Serialization;

namespace InventoryApp.Service.DTO
{
    [DataContract]
    public class ProductInventoryDTO
    {
        [DataMember]
        public ProductDTO Product { get; set; }
        [DataMember]
        public int Quantity { get; set; }
    }
}