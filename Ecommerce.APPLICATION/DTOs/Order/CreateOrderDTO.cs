using System.Collections.Generic;

namespace Ecommerce.APPLICATION.DTOs.Order
{
    public record CreateOrderDTO
    {
        public Guid UserId { get; init; }
        public List<OrderItemRequestDTO> Items { get; init; } = new();
        public string? CouponCode { get; init; }
    }

    public record OrderItemRequestDTO
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
    }
}
