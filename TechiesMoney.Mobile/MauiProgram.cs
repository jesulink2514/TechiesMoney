using TechiesMoney.Mobile.Pages;
using TechiesMoney.Mobile.Services.Authentication;
using TechiesMoney.Mobile.ViewModels;

namespace TechiesMoney.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UsePrismApp<App>()
            .ConfigureServices(services =>
            {
                //services.AddSingleton<IAuthService, AuthService>();
                services.AddTransient<WelcomePageViewModel>();
                services.AddTransient<HomePageViewModel>();
            })
            .RegisterTypes(containerRegistry =>
            {
                containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
                containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            })
            .OnAppStart((container, navigationService) =>
            {  
                navigationService.CreateBuilder()
                  .AddNavigationSegment<WelcomePageViewModel>()
                  .Navigate(HandleNavigationError);                
            });

        return builder.Build();
    }
    private static void HandleNavigationError(Exception ex)
    {
        Console.WriteLine(ex);
        System.Diagnostics.Debugger.Break();
    }
}
