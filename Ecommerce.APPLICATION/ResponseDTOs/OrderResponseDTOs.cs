using System;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.ResponseDTOs;

/// <summary>
/// Response model for an order
/// </summary>
/// <param name="OrderId">Unique identifier of the order</param>
/// <param name="UserId">Unique identifier of the user who placed the order</param>
/// <param name="PaymentId">Unique identifier of the payment used</param>
public record OrderResponseDTO(
    Guid OrderId,
    Guid UserId,
    Guid PaymentId);

/// <summary>
/// Response model for an individual order item
/// </summary>
/// <param name="OrderItemsId">Unique identifier of the order item</param>
/// <param name="OrderId">Unique identifier of the order it belongs to</param>
/// <param name="ProductId">Unique identifier of the product</param>
/// <param name="Quantity">Quantity purchased</param>
/// <param name="PricePerUnitAtPurchaseTime">Price per unit at the time of purchase</param>
/// <param name="CreateAt">Timestamp when the order item was created</param>
public record OrderItemResponseDTO(
    Guid OrderItemsId,
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal PricePerUnitAtPurchaseTime,
    DateTime CreateAt);

/// <summary>
/// Response model for a payment record
/// </summary>
/// <param name="PaymentId">Unique identifier of the payment</param>
/// <param name="OrderId">Unique identifier of the order associated with this payment</param>
/// <param name="Amount">Total amount paid</param>
/// <param name="PaymentType">Method of payment (e.g., CreditCard, Cash)</param>
/// <param name="PaymentDate">Timestamp when the payment was processed</param>
public record PaymentResponseDTO(
    Guid PaymentId,
    Guid OrderId,
    decimal Amount,
    PaymentType PaymentType,
    DateTime PaymentDate);
