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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
			services.AddMvc(options => { options.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
			services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {  //overriding default password validations
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContextPool<AppDbContext>(options=>options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

		}
        //This method gets called by the runtime
        public void Configure(IApplicationBuilder app /*<--this is an interface*/, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                //This is a middleware
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); //route = error/<statuscodevalue> it reexecutes the pipeline
            }
			app.UseStaticFiles();
			//app.UseMvcWithDefaultRoute();

            app.UseAuthentication(); 
            app.UseAuthorization(); 
			//conventional routing
			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "default", template: "/{controller=Home}/{action=Index}/{id?}");
			}); //1st param: route name 2nd param: template


       
        }
    }
}
