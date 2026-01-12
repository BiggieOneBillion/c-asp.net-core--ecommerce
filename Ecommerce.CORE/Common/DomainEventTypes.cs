using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.CORE.Common
{
    public static class DomainEventTypes
    {
        public static string ProductCreated => "Product.created";
        public static string UserCreated => "User.created";
        public static string OrderCreated => "Order.created";
        public static string OrderConfirmed => "Order.confirmed";
        public static string InventoryReserved => "Inventory.reserved";
        public static string ProductPriceChanged => "Product.price_changed";

    }
}