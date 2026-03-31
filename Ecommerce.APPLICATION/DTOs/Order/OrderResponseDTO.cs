namespace Ecommerce.APPLICATION.DTOs.Order
{
    /// <summary>
    /// DTO representing the result of a created order
    /// </summary>
    public record OrderResponseDTO
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public Guid PaymentId { get; init; } 
    }
}