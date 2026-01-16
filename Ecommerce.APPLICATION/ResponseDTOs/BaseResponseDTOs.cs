using System;

namespace Ecommerce.APPLICATION.ResponseDTOs;

public record CategoryResponseDTO(
    Guid CategoryId,
    string CategoryName,
    string CategoryDescription,
    bool ActiveStatus);

public record ProductResponseDTO(
    Guid ProductId,
    string Name,
    string Description,
    Guid CategoryId,
    decimal CurrentPrice,
    decimal? DiscountedPrice = null,
    decimal? DiscountPercentage = null);

public record UserResponseDTO(
    Guid UserId,
    string Name,
    string Email);
