namespace Ecommerce.APPLICATION.DTOs.Product
{
    public record CreateProductDTO
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public Guid CategoryId { get; init; }
        public string? ImageUrl { get; init; }
    }
}