using System;

namespace InventoryApp.Common
{
    public class RegionNames
    {
        public const string InventoryRegion = "InventoryRegion";
        public const string OrdersRegion = "OrdersRegion";
    }

    public class UserSession
    {
        // This should come from a session/login. Hardcoding here as a dummy value.
        public static Guid UserId = Guid.Parse("82870119-1366-4492-955E-B6A412ACA111");
    }
}