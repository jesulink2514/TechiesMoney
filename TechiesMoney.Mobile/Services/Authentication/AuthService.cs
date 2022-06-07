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
    public class AuthService: IAuthService
    {
        private readonly IPublicClientApplication authenticationClient;
        public AuthService()
        {
            authenticationClient = PublicClientApplicationBuilder.Create(Constants.ClientId)
                .WithB2CAuthority(Constants.AuthoritySignIn)
                .WithRedirectUri($"msal{Constants.ClientId}://auth")                
                .Build();
        }

        public async Task<AuthenticationResult> LoginAsync(CancellationToken cancellationToken)
        {
            AuthenticationResult result;
            try
            {
                IEnumerable<IAccount> accounts = await authenticationClient.GetAccountsAsync();
                
                result = await authenticationClient
                    .AcquireTokenSilent(Constants.Scopes,GetAccountByEnvironment(accounts, Constants.AccountEnvironment))
                    .ExecuteAsync(cancellationToken);
                
                return result;
            }
            catch(MsalUiRequiredException rex)
            {
                Debug.WriteLine(rex);
                return await LoginInteractively(cancellationToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public async Task<AuthenticationResult> LoginSilently(CancellationToken cancellationToken)
        {            
            try
            {
                IEnumerable<IAccount> accounts = await authenticationClient.GetAccountsAsync();
                
                var result = await authenticationClient
                .AcquireTokenSilent(Constants.Scopes, GetAccountByEnvironment(accounts, Constants.AccountEnvironment))
                .ExecuteAsync(cancellationToken);
                
                return result;
            }
            catch (MsalUiRequiredException ex)
            {
                return null;
            }                        
        }
        public async Task<AuthenticationResult> LoginInteractively(CancellationToken cancellationToken)
        {
            try
            {               
                var result = await authenticationClient
                    .AcquireTokenInteractive(Constants.Scopes)
                    .WithPrompt(Prompt.ForceLogin)
#if ANDROID
                    .WithParentActivityOrWindow(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity)
#endif
                    .ExecuteAsync(cancellationToken);
                return result;
            }
            catch (MsalClientException msalEx)
            {
                if (msalEx.ErrorCode == "authentication_canceled")
                {
                    return null;
                }
                Debug.Write(msalEx);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        private IAccount GetAccountByEnvironment(IEnumerable<IAccount> accounts, string environment)
        {
            foreach (var account in accounts)
            {                
                if (account.Environment.ToLower() == environment.ToLower()) return account;
            }

            return null;
        }

        public async Task SignOutAsync()
        {
            IEnumerable<IAccount> accounts = await authenticationClient.GetAccountsAsync();
            while (accounts.Any())
            {
                await authenticationClient.RemoveAsync(accounts.FirstOrDefault());
                accounts = await authenticationClient.GetAccountsAsync();
            }                        
        }
    }
}
