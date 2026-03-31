namespace Ecommerce.APPLICATION.DTOs.Product
{
    public record UpdateProductDTO
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
      
        public Guid CategoryId { get; init; }
        public string? ImageUrl { get; init; }
        public bool IsActive { get; init; }
    }
}