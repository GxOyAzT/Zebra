using System;
using System.Linq;
using Zebra.CustomerService.Application.Shared.ErrorCollector;
using Zebra.CustomerService.Domain.Models.ApiModels;

namespace Zebra.CustomerService.Application.Validation.Customer
{
    public class CustomerValidator : ErrorCollector, ICustomerValidator
    {
        public bool IsValid(CustomerApiModel customerModel)
        {
            if (String.IsNullOrEmpty(customerModel.FullName))
            {
                AddError("FullName property cannot be empty value.");
            }

            if (customerModel.Dob < DateTime.Now.AddYears(-100))
            {
                AddError($"Dob cannot be less then {DateTime.Now.AddYears(-100)}. Actual value: {customerModel.Dob}.");
            }

            if (String.IsNullOrEmpty(customerModel.City))
            {
                AddError("Address.City property cannot be empty value.");
            }
            if (String.IsNullOrEmpty(customerModel.Street))
            {
                AddError("Address.Street property cannot be empty value.");
            }

            return GetErrors().Any() ? false : true;
        }
    }
}
