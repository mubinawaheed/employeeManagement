using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using employeeManagement.Models;

namespace employeeManagement
{
    public class Startup
    {   
        private IConfiguration _config;
        //this is a constructor
        public Startup(IConfiguration config)
        {
            _config=config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();

        }
        //This method gets called by the runtime
        public void Configure(IApplicationBuilder app /*<--this is an interface*/, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                //This is a middleware
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW:1 incomming request");
                await next();
                logger.LogInformation("MW1: outgoing response");
            });

            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW:2 incomming request");
                await next();
                logger.LogInformation("MW:2 outgoing response");
            });

            //customizing default files middleware
            /*DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("home.html");
            app.UseDefaultFiles(defaultFilesOptions);*/

            //combining staticfiles and defaultfiles middlewares
            /*FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("home.html");
            app.UseFileServer(fileServerOptions);*/

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            //5th middleware. This is a terminal middleware
            app.Run(async (context) =>
            {
                /* await context.Response.WriteAsync($"Value of MyKey = {_config["MyKey"]}\nApp is running inside =" +
                     $" {System.Diagnostics.Process.GetCurrentProcess().ProcessName}\nEnvironment = " +
                     $"{env.EnvironmentName}\n" + $"Application url = {_config["profiles"]}");
                 logger.LogInformation("MW:3 Request completed response produced");*/
                await context.Response.WriteAsync("hello world");
            }); 
        }
    }
}
