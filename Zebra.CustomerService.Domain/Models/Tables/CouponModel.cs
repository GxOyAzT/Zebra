using System;
using Zebra.CustomerService.Domain.Shared;

namespace Zebra.CustomerService.Domain.Models.Tables
{
    public class CouponModel : Entity
    {
        public Guid CustomerId { get; set; }
        public decimal Value { get; set; }
        public DateTime ValidityDate { get; set; }
    }
}
