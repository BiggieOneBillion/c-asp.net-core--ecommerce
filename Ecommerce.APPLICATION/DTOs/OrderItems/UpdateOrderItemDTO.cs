namespace Ecommerce.APPLICATION.DTOs.OrderItems
{
    public record UpdateOrderItemDTO
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; } = 1;
        public DateTime CreateAt { get; init; }
    }
}