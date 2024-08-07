using System.Diagnostics.CodeAnalysis;
using CodingChallenge.Core.Builders;
using CodingChallenge.Core.Builders.Factory;
using CodingChallenge.Core.Cache;
using CodingChallenge.Core.Services.StoriesServices.Interface;
using CodingChallenge.Core.Services.StoriesServices.Service;
using CodingChallenge.Core.Services.ThirdPartyAPIService;
using CodingChallenge.Core.ThirdPartyAPIService;

namespace CodingChallenge.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class RegisterServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection ServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            //Register Cache service
            services.AddScoped<ICacheService, CacheService>();
            services.AddMemoryCache();

            //Configure Automapper
            services.AddAutoMapper(typeof(Program));

            //Register Services Dependency Injection
            services.AddSingleton<IResponseBuilderFactory, ResponseBuilderFactory>();
            services.AddSingleton(new HttpClient() 
            { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") }); //Use Base URI from Hacker News API

            services.AddScoped<IStoriesService, StoriesService>();
            services.AddTransient<IThirdPartyAPI, ThirdPartyAPI>();

            return services;
        }
    }
}
