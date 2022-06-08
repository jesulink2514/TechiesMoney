using Microsoft.Identity.Client;
using System.Diagnostics;

namespace TechiesMoney.Mobile.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthenticationResult> LoginSilently(CancellationToken cancellationToken);
        Task<AuthenticationResult> LoginAsync(CancellationToken cancellationToken);
        Task<AuthenticationResult> LoginInteractively(CancellationToken cancellationToken);
        Task SignOutAsync();
    }
    public class AuthService
    {
        
    }
}
