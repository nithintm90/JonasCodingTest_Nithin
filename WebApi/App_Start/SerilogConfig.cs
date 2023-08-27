using System.Diagnostics;
using Serilog;

namespace WebApi.App_Start
{
	public static class SerilogConfig
	{
		public static void Setup()
		{
			Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Debug()
				.ReadFrom.AppSettings()
				.Enrich.WithHttpRequestUrl()
				.Enrich.WithHttpRequestType()
				.Enrich.WithHttpRequestClientHostIP()
				//.WriteTo.File()
				.CreateLogger();

			Log.ForContext(typeof(SerilogConfig)).Information("Application starting.");
		}
	}
}
