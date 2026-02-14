using System;

namespace Ecommerce.APPLICATION.ResponseDTOs;

/// <summary>
/// Response model for a category
/// </summary>
/// <param name="CategoryId">Unique identifier of the category</param>
/// <param name="CategoryName">Name of the category</param>
/// <param name="CategoryDescription">Detailed description of the category</param>
/// <param name="ActiveStatus">Status indicating if the category is active</param>
public record CategoryResponseDTO(
    Guid CategoryId,
    string CategoryName,
    string CategoryDescription,
    bool ActiveStatus);

/// <summary>
/// Response model for a product
/// </summary>
/// <param name="ProductId">Unique identifier of the product</param>
/// <param name="Name">Name of the product</param>
/// <param name="Description">Detailed description of the product</param>
/// <param name="CategoryId">Unique identifier of the category it belongs to</param>
/// <param name="CurrentPrice">Default/Initial price of the product</param>
/// <param name="DiscountedPrice">Price after applying any active discounts</param>
/// <param name="DiscountPercentage">Percentage of the discount applied (0-100)</param>
public record ProductResponseDTO(
    Guid ProductId,
    string Name,
    string Description,
    Guid CategoryId,
    decimal CurrentPrice,
    decimal? DiscountedPrice = null,
    decimal? DiscountPercentage = null);

/// <summary>
/// Response model for user details
/// </summary>
/// <param name="UserId">Unique identifier of the user</param>
/// <param name="Name">Full name of the user</param>
/// <param name="Email">Email address of the user</param>
public record UserResponseDTO(
    Guid UserId,
    string Name,
    string Email);
