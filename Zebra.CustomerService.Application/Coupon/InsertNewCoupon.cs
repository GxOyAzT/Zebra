using Zebra.CustomerService.Application.Coupon.Interfaces;
using Zebra.CustomerService.Application.Shared.ErrorCollector;
using Zebra.CustomerService.Domain.Models.Tables;
using Zebra.CustomerService.Persistance.Repository.Coupon;
using Zebra.CustomerService.Persistance.Repository.Customer;

namespace Zebra.CustomerService.Application.Coupon
{
    public class InsertNewCoupon : ErrorCollector, IInsertNewCoupon
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ICustomerRepository _customerRepository;

        public InsertNewCoupon(
            ICouponRepository couponRepository,
            ICustomerRepository customerRepository)
        {
            _couponRepository = couponRepository;
            _customerRepository = customerRepository;
        }

        public void Execute(CouponModel couponModel)
        {
            if (_customerRepository.Get(couponModel.CustomerId) == null)
            {
                AddError($"Cannot create coupon for not existing customer. Actual customer ID: {couponModel.CustomerId}");
                return;
            }

            _couponRepository.Insert(couponModel);

            return;
        }
    }
}
