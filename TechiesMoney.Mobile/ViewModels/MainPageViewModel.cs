using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TechiesMoney.Mobile.ViewModels
{
    public class MainPageViewModel
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoCommand = new DelegateCommand(Go);
        }
        
        private async void Go()
        {
            await _navigationService.NavigateAsync("HomePage");
        }

        public ICommand GoCommand { get; }
    }
}
