namespace Ecommerce.APPLICATION.DTOs.Product
{
    public record UpdateProductPriceDTO
    {
        public decimal NewPrice { get; init; }
    }
}
