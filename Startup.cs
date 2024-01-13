﻿using System;
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

            app.UseStaticFiles();
			//app.UseMvcWithDefaultRoute();

			//conventional routing
			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "default", template: "api/{controller=Home}/{action=Index}/{id?}");
			}); //1st param: route name 2nd param: template


            //5th middleware. This is a terminal middleware
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("hello world");
            }); 
        }
    }
}
