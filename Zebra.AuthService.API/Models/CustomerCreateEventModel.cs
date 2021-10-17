using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zebra.AuthService.API.Models
{
    public class CustomerCreateEventModel
    {
        public string UserId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
