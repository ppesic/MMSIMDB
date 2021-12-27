using Microsoft.Extensions.Primitives;
using MMSIMDB.Application.DTOs.Account;
using MMSIMDB.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMSIMDB.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
    }
}
