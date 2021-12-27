using Microsoft.AspNetCore.Http;
using MMSIMDB.Application.Interfaces;
using MMSIMDB.Persistence.Contexts;
using MMSIMDB.WebAPI.Middlewares;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MMSIMDB.WebAPI.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");            
        }

        public string UserId { get; }
    }
}
