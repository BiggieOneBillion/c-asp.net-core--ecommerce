
namespace Ecommerce.CORE.Enums
{
    public enum InventoryMovementType
    {
        StockIn = 1,        // restock
        StockOut = 2,       // manual deduction
        OrderReserved = 3,  // reserved for order
        OrderCancelled = 4, // unreserve
        OrderFulfilled = 5, // stock deducted after shipping
        Return = 6          // items returned back to inventory
    }
}
