using System;
using Zebra.CustomerService.Domain.Enums;
using Zebra.CustomerService.Domain.Shared;
using Zebra.CustomerService.Domain.ValueObjects;

namespace Zebra.CustomerService.Domain.Models
{
    public class CustomerModel : Entity
    {
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public int Points { get; set; }
        public Address Address { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
