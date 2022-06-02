using Foundation;
using UIKit;

namespace TechiesMoney.Mobile;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
    {
        AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
        return base.OpenUrl(app, url, options);
    }
}

public static class AuthenticationContinuationHelper
{
    public static void SetAuthenticationContinuationEventArgs(NSUrl url)
    {
    }
}
