using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Auth;
using ApplicationCore.Interfaces.Config;
using ApplicationCore.Services.Auth;
using ApplicationCore.Services.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ApplicationCore.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //config
            services.AddTransient<IClientApiService, ClientApiService>();

            services.AddTransient<IGroupTaskService, GroupTaskService>();

            services.AddTransient<ITasksService, TasksService>();

            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            return services;
        }
    }
}

