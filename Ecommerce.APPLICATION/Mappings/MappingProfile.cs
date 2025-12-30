using AutoMapper;
using Ecommerce.APPLICATION.ResponseDTOs;
using Ecommerce.CORE.Entity;

namespace Ecommerce.APPLICATION.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category Mappings
        CreateMap<Category, CategoryResponseDTO>()
            .ForCtorParam("CategoryId", opt => opt.MapFrom(src => src.CategoryId.Id));

        // Product Mappings
        CreateMap<Product, ProductResponseDTO>()
            .ForCtorParam("ProductId", opt => opt.MapFrom(src => src.ProductId.Id))
            .ForCtorParam("CategoryId", opt => opt.MapFrom(src => src.CategoryId.Id));

        // User Mappings
        CreateMap<Users, UserResponseDTO>()
            .ForCtorParam("UserId", opt => opt.MapFrom(src => src.Id.Id));

        // Order Mappings
        CreateMap<Order, OrderResponseDTO>()
            .ForCtorParam("OrderId", opt => opt.MapFrom(src => src.OrderId.Id))
            .ForCtorParam("UserId", opt => opt.MapFrom(src => src.UserId.Id))
            .ForCtorParam("PaymentId", opt => opt.MapFrom(src => src.PaymentId.Id));

        // Inventory Mappings
        CreateMap<Inventory, InventoryResponseDTO>()
            .ForCtorParam("InventoryId", opt => opt.MapFrom(src => src.InventoryId.Id))
            .ForCtorParam("ProductId", opt => opt.MapFrom(src => src.ProductId.Id))
            .ForMember(dest => dest.AvailableQuantity, opt => opt.MapFrom(src => src.AvaliableQuantity()));

        // OrderItems Mappings
        CreateMap<OrderItems, OrderItemResponseDTO>()
            .ForCtorParam("OrderItemsId", opt => opt.MapFrom(src => src.OrderItemsId.Id))
            .ForCtorParam("OrderId", opt => opt.MapFrom(src => src.OrderId.Id))
            .ForCtorParam("ProductId", opt => opt.MapFrom(src => src.ProductId.Id));

        // Payment Mappings
        CreateMap<Payment, PaymentResponseDTO>()
            .ForCtorParam("PaymentId", opt => opt.MapFrom(src => src.PaymentId.Id))
            .ForCtorParam("OrderId", opt => opt.MapFrom(src => src.OrderId.Id));

        // ProductPriceHistory Mappings
        CreateMap<ProductPriceHistory, ProductPriceHistoryResponseDTO>()
            .ForCtorParam("ProductPriceHistoryId", opt => opt.MapFrom(src => src.ProductPriceHistoryId.Id))
            .ForCtorParam("ProductId", opt => opt.MapFrom(src => src.ProductId.Id));

        // InventoryMovement Mappings
        CreateMap<InventoryMovement, InventoryMovementResponseDTO>()
            .ForCtorParam("InventoryMovementId", opt => opt.MapFrom(src => src.InventoryMovementId.Id))
            .ForCtorParam("ProductId", opt => opt.MapFrom(src => src.ProductId.Id));
    }
}
