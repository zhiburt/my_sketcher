using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using lastVersionAuthe.Data;
using lastVersionAuthe.Models;
using lastVersionAuthe.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using WebSocketManager;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace lastVersionAuthe
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];

                twitterOptions.ClaimActions.MapJsonKey(ClaimTypes.Email, "Email");

            });

            services.AddAuthentication().AddVK(optionsVK =>
            {
                optionsVK.ClientId = Configuration["Authentication:VK:ClientId"];
                optionsVK.ClientSecret = Configuration["Authentication:VK:ClientSecret"];

                optionsVK.Fields.Add("uid");
                optionsVK.Fields.Add("first_name");
                optionsVK.Fields.Add("last_name");

                // In this case email will return in OAuthTokenResponse, 
                // but all scope values will be merged with user response
                // so we can claim it as field
                optionsVK.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "uid");
                optionsVK.ClaimActions.MapJsonKey(ClaimTypes.Email, "Email");
                optionsVK.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
                optionsVK.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
                optionsVK.ClaimActions.MapJsonKey(ClaimTypes.Country, "country");
            });
            
            services.AddWebSocketManager();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddTransient<IEmailSender, EmailSender>(); 

            services.Configure<RequestLocalizationOptions>(options =>
           {
               var supportedCultures = new[]
{
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("ru-RU"),
                new CultureInfo("ru")
                };

               options.DefaultRequestCulture = new RequestCulture("en-US");
               options.SupportedCultures = supportedCultures;
               options.SupportedUICultures = supportedCultures;

           });


            services.AddMvc()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseStaticFiles();

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
