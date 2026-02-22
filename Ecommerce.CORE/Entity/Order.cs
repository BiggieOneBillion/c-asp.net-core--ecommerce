using Ecommerce.CORE.Common;
using Ecommerce.CORE.DomainEvents;
using Ecommerce.CORE.Enums;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Entity;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<Ecommerce.CORE.Entity.OrderItems> _orderItems = new();
    
    public UserId UserId { get; private set; } = default!;
    public PaymentId? PaymentId { get; private set; }
    public Guid? AppliedDiscountId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public IReadOnlyCollection<Ecommerce.CORE.Entity.OrderItems> OrderItems => _orderItems.AsReadOnly();
    
    // Private constructor for EF Core
    private Order() { }
    
    // Factory method
    public static Order Create(UserId userId, PaymentId paymentId, List<Ecommerce.CORE.Entity.OrderItems> items, decimal discountAmount = 0, Guid? appliedDiscountId = null)
    {
        if (items == null || !items.Any())
            throw new DomainException("Order must have at least one item");
        
        var order = new Order
        {
            Id = OrderId.Create(Guid.NewGuid()),
            UserId = userId,
            PaymentId = paymentId,
            AppliedDiscountId = appliedDiscountId,
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
            i.ProductId.Id, 
            i.Quantity,
            i.PricePerUnitAtPurchaseTime
        )).ToList();
        
        order.RaiseDomainEvent(new OrderCreatedDomainEvent(
            order.Id.Id, 
            order.UserId.Id, 
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
            Id.Id, 
            UserId.Id
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

    // Adding missing property if needed by handlers
    public DateTime OrderDate { get; set; }
    public decimal Subtotal { get; set; }
}
