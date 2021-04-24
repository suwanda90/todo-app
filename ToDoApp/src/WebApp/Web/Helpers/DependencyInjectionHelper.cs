using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Interfaces.Config;
using Web.Services.Config;

namespace Web.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IClientApiService, ClientApiService>();
            
            return services;
        }
    }
}

