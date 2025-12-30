using System;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.ResponseDTOs;

public record OrderResponseDTO(
    Guid OrderId,
    Guid UserId,
    Guid PaymentId);

public record OrderItemResponseDTO(
    Guid OrderItemsId,
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal PricePerUnitAtPurchaseTime,
    DateTime CreateAt);

public record PaymentResponseDTO(
    Guid PaymentId,
    Guid OrderId,
    decimal Amount,
    PaymentType PaymentType,
    DateTime PaymentDate);
