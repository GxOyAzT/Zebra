using Refit;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiCalls.ProductService.Commands;

namespace Zebra.Gateway.API.ApiCalls.ProductService
{
    public interface IPriceManagementFetch
    {
        [Delete("/api/PriceManagement/deleteprice")]
        Task DeletePrice([Body] DeletePriceCommand deletePriceCommand);

        [Post("/api/PriceManagement/updateproductprice")]
        Task UpdatePrice([Body] AddPriceCommand addPriceCommand);
    }
}
