using Serilog;
using System.Web;

namespace WebApi
{
    public static class SerilogClass
    {
        public static Serilog.ILogger _logger;
        static SerilogClass()
        {
            _logger = new LoggerConfiguration()
                                                .MinimumLevel.Information(). 
                                                WriteTo.File(HttpContext.Current.Server.MapPath("~/logger.txt")).CreateLogger();
            
           
        }
    }
}