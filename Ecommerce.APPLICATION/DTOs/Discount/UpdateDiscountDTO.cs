namespace Ecommerce.APPLICATION.DTOs.Discount
{
    public record UpdateDiscountDTO
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public bool IsActive { get; init; }
    }
}
