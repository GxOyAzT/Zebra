using System;

namespace Zebra.OrderService.Domain.Models
{
    public class CustomerModel
    {
        public Guid CustomerId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
