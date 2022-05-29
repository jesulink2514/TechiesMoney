using TechiesMoney.Mobile.Pages;
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
                services.AddTransient<MainPageViewModel>();
                services.AddTransient<HomePageViewModel>();                
            })
            .RegisterTypes(containerRegistry =>
            {                
                containerRegistry.RegisterForNavigation<MainPage,MainPageViewModel>();
                containerRegistry.RegisterForNavigation<HomePage,HomePageViewModel>();
            })
            .OnAppStart(navigationService => navigationService.CreateBuilder()
                .AddNavigationSegment<MainPageViewModel>()
                .Navigate(HandleNavigationError));

		return builder.Build();
	}
    private static void HandleNavigationError(Exception ex)
    {
        Console.WriteLine(ex);
        System.Diagnostics.Debugger.Break();
    }
}
