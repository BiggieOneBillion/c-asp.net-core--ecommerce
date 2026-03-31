namespace Ecommerce.APPLICATION.DTOs.Discount
{
    public record ValidateCouponDTO
    {
        public string CouponCode { get; init; } = string.Empty;
        public decimal OrderTotal { get; init; }
    }
}
