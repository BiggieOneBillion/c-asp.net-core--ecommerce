using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Enums;

namespace Ecommerce.APPLICATION.DTOs.Payment
{
    public record CreatePaymentDTO
    {
        public PaymentType PaymentType { get; init;}

        public required decimal Amount { get; init; }

        public DateTime PaymentDate { get; init; }

        public Guid OrderId { get; init; }
    }
}