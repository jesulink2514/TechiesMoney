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
        public WelcomePageViewModel ()
        {           
            LoginCommand = new DelegateCommand(Login);            
        }
        
        private async void Login()
        {
            
        }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
