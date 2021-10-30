using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.DocumentDB.Interfaces;
using MP.DocumentDB.Models;

namespace MP.DocumentDB
{
    public static class Dependencies
    {
        public static IServiceCollection AddCollection<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            var collectionConfigSection = configuration.GetSection($"DocumentDB:{typeof(T).Name}");

            services.Configure<CollectionConfiguration<T>>(collectionConfigSection);
            services.AddSingleton<IDocumentCollection<T>, DocumentCollection<T>>();

            return services;
        }
    }
}
