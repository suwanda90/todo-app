using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json.Serialization;
using Web.Helpers;
using Web.ViewModels;

namespace Web
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
            Action<AppSettingsViewModel> appSettings = (opt =>
            {
                opt.ApplicationCookiesName = Configuration["Application:CookiesName"];
                opt.ApplicationFolderApp = Configuration["Application:FolderApp"];
                opt.ApiUrl = Configuration["Api:Url"];
                opt.ApiClientId = Configuration["Api:ClientId"];
                opt.ApiClientSecret = Configuration["Api:ClientSecret"];
                opt.SecurityEncryptKey = Configuration["Security:EncryptKey"];
            });

            services.Configure(appSettings);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettingsViewModel>>().Value);

            services.AddControllersWithViews()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });

            //register service in here
            services.AddApplications();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettingsViewModel appSettingsViewModel)
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

            //if use sub folder/sub domain
            if (!string.IsNullOrEmpty(appSettingsViewModel.ApplicationFolderApp))
            {
                app.UsePathBase(appSettingsViewModel.ApplicationFolderApp);
            }

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
