using Serilog;
using System.Web;

namespace WebApi
{
    public static class Logger
    {
        private static readonly ILogger _errorLogger;

        static Logger()
        {
            _errorLogger = new LoggerConfiguration()
                .WriteTo.Async(w => w.File(HttpContext.Current.Server.MapPath("~/Logs/log-.log"), rollingInterval: RollingInterval.Day))
                .MinimumLevel.Verbose()
                .CreateLogger();
        }

        public static void LogError(string error)
        {
            _errorLogger.Error(error);
        }

        public static void LogInfo(string info)
        {
            _errorLogger.Information(info);
        }
    }
}