using Serilog;

namespace WebApi.App_Start
{
    public static class LoggerConfig
    {
        public static void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Debug()
                .ReadFrom.AppSettings()
                .Enrich.WithHttpRequestUrl()
                .Enrich.WithHttpRequestType()
                .CreateLogger();
        }
    }
}