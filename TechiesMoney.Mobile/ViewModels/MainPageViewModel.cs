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
    public class MainPageViewModel: INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        private readonly INavigationService _navigationService;

        public MainPageViewModel(AuthService authService, INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;

            GoCommand = new DelegateCommand(Go);
            SignOutCommand = new DelegateCommand(SignOut);
        }

        public string Result { get; set; }
        private async void Go()
        {
            var result = await _authService.LoginAsync(CancellationToken.None);
            var token = result?.IdToken; // you can also get AccessToken if you need it
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var data = handler.ReadJwtToken(token);
                var claims = data.Claims.ToList();
                if (data != null)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"Name: {data.Claims.FirstOrDefault(x => x.Type.Equals("name"))?.Value}");
                    stringBuilder.AppendLine($"Email: {data.Claims.FirstOrDefault(x => x.Type.Equals("emails"))?.Value}");
                    Result = stringBuilder.ToString();
                }
            }


            //await _navigationService.CreateBuilder()
            //    .AddNavigationSegment<HomePageViewModel>()
            //    .NavigateAsync();
        }

        private async void SignOut()
        {
            await _authService.SignOutAsync();
            Result = String.Empty;
        }

        public ICommand GoCommand { get; }
        public ICommand SignOutCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
