using System;

namespace Zebra.CustomerService.Domain.Models
{
    public class CouponApiModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Value { get; set; }
        public DateTime ValidityDate { get; set; }
    }
}
