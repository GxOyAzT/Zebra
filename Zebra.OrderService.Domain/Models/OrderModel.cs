using System;
using System.Collections.Generic;
using Zebra.OrderService.Domain.Enums;

namespace Zebra.OrderService.Domain.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public List<ProductModel> Products { get; set; }
        public CustomerModel Customer { get; set; }
        public decimal TransactionValue { get; set; }
        public 
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
