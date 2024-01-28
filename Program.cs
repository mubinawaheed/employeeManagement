using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace employeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
           return WebHost.CreateDefaultBuilder(args)
				.ConfigureLogging((hostingContext, loggingBuilder) =>
				{
					//loggingBuilder.Configure(options =>
					//{
					//	options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
					//										| ActivityTrackingOptions.TraceId
					//										| ActivityTrackingOptions.ParentId;
					//});
					loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					loggingBuilder.AddConsole();
					loggingBuilder.AddDebug();
					loggingBuilder.AddEventSourceLogger();
					loggingBuilder.AddNLog();
				})
				.UseStartup<Startup>();
        }
    }
}