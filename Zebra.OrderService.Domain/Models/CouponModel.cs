using System;

namespace Zebra.OrderService.Domain.Models
{
    public class CouponModel
    {
        public Guid CouponId { get; set; }
        public decimal Value { get; set; }
    }
}
