using Ecommerce.CORE.Common;
using Ecommerce.CORE.DomainEvents;
using Ecommerce.CORE.Enums;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItems> _orderItems = new();
    
    public UserId UserId { get; private set; } = default;
    public PaymentId? PaymentId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public IReadOnlyCollection<OrderItems> OrderItems => _orderItems.AsReadOnly();
    
    // Private constructor for EF Core
    private Order() { }
    
    // Factory method
    public static Order Create(UserId userId, PaymentId paymentId, List<OrderItems> items, decimal discountAmount = 0)
    {
        if (items == null || !items.Any())
            throw new DomainException("Order must have at least one item");
        
        var order = new Order
        {
            Id = OrderId.Create(Guid.NewGuid()),
            UserId = userId,
            PaymentId = paymentId,
            Status = OrderStatus.Pending,
            DiscountAmount = discountAmount,
            CreatedAt = DateTime.UtcNow
        };
        
        foreach (var item in items)
        {
            order._orderItems.Add(item);
        }
        
        order.CalculateTotalAmount();
        
        // Raise domain event
        var itemsData = items.Select(i => new OrderItemData(
            i.ProductId.Id, // Using Value() method from ProductId
            i.Quantity,
            i.PricePerUnitAtPurchaseTime
        )).ToList();
        
        order.RaiseDomainEvent(new OrderCreatedDomainEvent(
            order.Id.Id, // Using Value() method from OrderId
            order.UserId.Id, // Using Value() method from UserId
            order.TotalAmount,
            itemsData
        ));
        
        return order;
    }
    
    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException("Only pending orders can be confirmed");
        
        Status = OrderStatus.Confirmed;
        RaiseDomainEvent(new OrderConfirmedDomainEvent(
            Guid.Parse(Id.Value()), 
            Guid.Parse(UserId.Value())
        ));
    }
    
    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
            throw new DomainException("Cannot cancel delivered orders");
        
        Status = OrderStatus.Cancelled;
    }
    
    private void CalculateTotalAmount()
    {
        var subTotal = _orderItems.Sum(i => i.PricePerUnitAtPurchaseTime * i.Quantity);
        TotalAmount = subTotal - DiscountAmount;
    }
}
