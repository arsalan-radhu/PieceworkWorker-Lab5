/*
 * Name: Arsalan Arif Radhu
 * Date: 4 December 2021
 * Student ID: 100813965
 * Description: StartUp file
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieceworkWorker_Lab5.Context;
using Microsoft.EntityFrameworkCore;

namespace PieceworkWorker_Lab5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //Allow .NET Core Version 3.0 to be considered compatible
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            // Here, we disable endpoint routing. It was an issue in upgrading from .NET Core 2.0
            services.AddMvc(options => options.EnableEndpointRouting = false);
            //Adding the connection string! A;ways just like this for our purposes.
            string connection =
                @"Server=(localdb)\mssqllocaldb;Database=Lab5Database2;Trusted_Connection=true";
            //Adding the DB Context
            services.AddDbContext<PieceworkWorkerContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
