using System;
using Zebra.CustomerService.Domain.Enums;

namespace Zebra.CustomerService.Domain.Models.ApiModels
{
    public class CustomerApiModel 
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public int Points { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
