using Microsoft.Extensions.Logging;
using Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace SzttVoting
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .Services.AddSingleton<UserServices>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            
            return builder.Build();
        }
    }
}
