using Ingeco.Intranet.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartB1t.Security.WebSecurity.Local.Interfaces;
using Ingeco.Intranet.Data.Repositories;
using Ingeco.Intranet.Data.Models;

namespace Ingeco.Intranet
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
            services.AddDbContext<WebDataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddScoped<IAccountSecurityRepository, AccountSecurityRepository>();
            services.AddAuthentication(Constants.AUTH_SCHEME)
                    .AddCookie(Constants.AUTH_SCHEME, config => 
                    {
                        config.Cookie.Name = "IngecoIntranetCookie";
                        config.LoginPath = "/Account/Login";
                        config.AccessDeniedPath = "/Account/AccessDenied";
                        config.LogoutPath = "/Account/Logout";
                    });

            services.AddControllersWithViews();
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

            app.UseAuthentication();
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
