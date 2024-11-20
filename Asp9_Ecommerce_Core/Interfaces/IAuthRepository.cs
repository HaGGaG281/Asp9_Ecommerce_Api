using Asp9_Ecommerce_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(Users user, string password);
        Task<string> LoginAsync(string username, string password);
        Task<string> SendPasswordResetLinkAsync(string email);
        Task<string> ResetPasswordAsync(string token, string newPassword);
        Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword);
        Task<string> ConfirmEmailAsync(string userId, string token);
    }
}
