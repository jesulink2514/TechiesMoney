using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechiesMoney.Mobile.Services.Authentication;

namespace TechiesMoney.Mobile.ViewModels
{
    public class WelcomePageViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly INavigationService _navigationService;

        public WelcomePageViewModel (IAuthService authService, INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;

            LoginCommand = new DelegateCommand(Login);            
        }
        
        private async void Login()
        {
            var result = await _authService.LoginAsync(CancellationToken.None);            
            var token = result?.IdToken; // you can also get AccessToken if you need it
            if (token != null)
            {
                await _navigationService.CreateBuilder()
                    .UseAbsoluteNavigation()
                    .AddNavigationSegment<HomePageViewModel>().AddParameter("login",result)
                    .NavigateAsync(); 
            }
        }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
