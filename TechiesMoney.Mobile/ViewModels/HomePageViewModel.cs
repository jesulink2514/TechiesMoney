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
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand SignOutCommand { get; }
        public DelegateCommand CallCommand { get; }
        public string Name { get; set; }
        public string Email { get; set; }
     
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
           
        }
    }
}
