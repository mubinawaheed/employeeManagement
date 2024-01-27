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
using Microsoft.EntityFrameworkCore;

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
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
			services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddDbContextPool<AppDbContext>(options=>options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

		}
        //This method gets called by the runtime
        public void Configure(IApplicationBuilder app /*<--this is an interface*/, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsProduction())
            {
                //This is a middleware
                app.UseDeveloperExceptionPage();
            }

			app.UseStatusCodePagesWithReExecute("/Error/{0}"); //route = error/<statuscodevalue> it reexecutes the pipeline


			app.UseStaticFiles();
			//app.UseMvcWithDefaultRoute();

			//conventional routing

			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "default", template: "/{controller=Home}/{action=Index}/{id?}");
			}); //1st param: route name 2nd param: template


            //5th middleware. This is a terminal middleware
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("hello world");
            //}); 
        }
    }
}
