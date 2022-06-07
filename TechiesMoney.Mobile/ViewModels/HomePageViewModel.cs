using Microsoft.Identity.Client;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using TechiesMoney.Mobile.Models;
using TechiesMoney.Mobile.Services.Authentication;

namespace TechiesMoney.Mobile.ViewModels
{
    public class HomePageViewModel : INavigatedAware, INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly INavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand SignOutCommand { get; }
        public DelegateCommand CallCommand { get; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Weather { get; set; }

        private string _token;

        public HomePageViewModel(IAuthService authService, INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
            SignOutCommand = new DelegateCommand(SignOut);
            CallCommand = new DelegateCommand(CallAPI);
        }

        private async void CallAPI()
        {
            var httpclient = new HttpClient() { BaseAddress = new Uri("https://techiesmoneyapi.azurewebsites.net/") };
            httpclient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer",_token);            
            
            var result = await httpclient.GetStringAsync("/weatherforecast");
            var forecast = JsonExtensions.DeserializeFromJson<WeatherForecast[]>(result);
            Weather = forecast.FirstOrDefault()?.Summary;
        }

        private async void SignOut()
        {
            await _authService.SignOutAsync();
            await _navigationService.CreateBuilder().UseAbsoluteNavigation()
                .AddNavigationSegment<WelcomePageViewModel>()
                .NavigateAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var result = parameters.GetValue<AuthenticationResult>("login");
            var token = result.IdToken;
            var handler = new JwtSecurityTokenHandler();
            var data = handler.ReadJwtToken(token);
            var claims = data.Claims.ToList();
            if (data != null)
            {
                Name = $"{data.Claims.FirstOrDefault(x => x.Type.Equals("given_name"))?.Value} {data.Claims.FirstOrDefault(x => x.Type.Equals("family_name"))?.Value}";
                Email = data.Claims.FirstOrDefault(x => x.Type.Equals("emails"))?.Value;

                _token = result.AccessToken;
            }
        }
    }
}
