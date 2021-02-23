using System.Runtime.Serialization;

namespace InventoryApp.Service.DTO
{
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}