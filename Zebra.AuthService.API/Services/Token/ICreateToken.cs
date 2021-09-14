using System.Collections.Generic;
using System.Security.Claims;

namespace Zebra.AuthService.API.Services.Token
{
    public interface ICreateToken
    {
        string Create(IList<Claim> claims);
    }
}
