using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InventoryApp.Service.DTO
{
    [DataContract]
    public class OrderDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<ProductInventoryDTO> Products { get; set; }
    }
}