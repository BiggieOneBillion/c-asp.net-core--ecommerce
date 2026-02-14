using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.DTOs.Payment
{
    /// <summary>
    /// DTO for initiating a payment
    /// </summary>
    public record CreatePaymentDTO
    {
        /// <summary>
        /// Method of payment (e.g., CreditCard, PayPal)
        /// </summary>
        public PaymentType PaymentType { get; init;}

        /// <summary>
        /// Total payment amount
        /// </summary>
        public required decimal Amount { get; init; }

        /// <summary>
        /// Date and time when the payment was made
        /// </summary>
        public DateTime PaymentDate { get; init; }

        /// <summary>
        /// Unique identifier of the associated order
        /// </summary>
        public Guid OrderId { get; init; }
    }
}