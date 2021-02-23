using Prism.Events;

namespace InventoryApp.Common
{
    public class OrderCreated : PubSubEvent
    {
    }

    public class OrderUpdated : PubSubEvent<int>
    {
    }
}