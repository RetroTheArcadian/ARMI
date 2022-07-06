using ARMI.Helpers;
using ARMI.Data;

namespace ARMI;

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
			});

		string dbPath = FileAccessHelper.GetLocalFilePath("armi.db3");
    	builder.Services.AddSingleton<DataRepository>(s => ActivatorUtilities.CreateInstance<DataRepository>(s, dbPath));

		return builder.Build();
	}
}
