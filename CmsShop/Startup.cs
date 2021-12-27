using CmsShop.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CmsShop
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
            services.AddRazorPages();
            #if DEBUG
                services.AddDbContext<ShopContext>(options => options.UseSqlServer
                    (Configuration.GetConnectionString("CmsShopContext")));
            #else
                string connectionString = Environment.GetEnvironmentVariable("JAWSDB_URL");
                connectionString = connectionString.Split("//")[1];
                string user = connectionString.Split(':')[0];
                connectionString = connectionString.Replace(user, "").Substring(1);
                string password = connectionString.Split('@')[0];
                connectionString = connectionString.Replace(password, "").Substring(1);
                string server = connectionString.Split(':')[0];
                connectionString = connectionString.Replace(server, "").Substring(1);
                string port = connectionString.Split('/')[0];
                string database = connectionString.Split('/')[1];
                connectionString = $"server={server};database={database};user={user};password={password};port={port}";
                services.AddDbContext<ShopContext>(options => options.UseMySQL
                    (connectionString));
            #endif
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
                app.UseExceptionHandler("/Error");
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
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}"
                 );
            });
        }
    }
}
