using Zebra.CustomerService.Application.Shared.ErrorCollector;
using Zebra.CustomerService.Domain.Models.Tables;

namespace Zebra.CustomerService.Application.Coupon.Interfaces
{
    public interface IInsertNewCoupon : IErrorCollector
    {
        void Execute(CouponModel couponModel);
    }
}
