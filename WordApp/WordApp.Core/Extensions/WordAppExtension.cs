using WordApp.Core.Services;
using WordApp.Data;
using WordApp.Entities;
using WordApp.Mongo;

namespace WordApp.Core.Extensions
{
    public static class WordAppExtension
    {
        public static IServiceCollection AddWordAppContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDbContext<MongoEntityTypeProvider>(configuration, typeof(Vocable));
            services.AddScoped<IDbContext, MongoDbContext>();
            services.AddScoped<IVocableService, VocableService>();
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            return services;
        }
    }
}
