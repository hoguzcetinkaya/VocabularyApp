using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson.Serialization;
using System.Reflection;
using WordApp.Data;

namespace WordApp.Mongo
{
    public static class MongoDbExtensions
    {

        public static IServiceCollection AddMongoDbContext<TProviderType>(this IServiceCollection services, IConfiguration configuration, Type entityType) where TProviderType : class, IMongoEntityTypeProvider => AddMongoDbContext<TProviderType>(services, configuration, new MongoEntityTypeProviderFromEntityType(entityType));

        public static IServiceCollection AddMongoDbContext<TProviderType>(this IServiceCollection services, IConfiguration configuration, IMongoEntityTypeProviderAssemblyLoader assemblyLoader) where TProviderType : class, IMongoEntityTypeProvider
        {
            BsonSerializer.RegisterSerializationProvider(BsonSerializationProvider.Instance);
            services.AddSingleton(assemblyLoader);
            services.AddSingleton<IMongoEntityTypeProvider, TProviderType>();
            //services.Configure<MongoDbOptions>(o => configuration.GetSection(MongoDbOptions.SectionName).Bind(o));
            services.AddScoped<IDbContext, MongoDbContext>();
            services.TryAddScoped<IDbContext, MongoDbContext>();
            return services;
        }

    }
    public class MongoEntityTypeProviderExecutingAssemblyLoader : IMongoEntityTypeProviderAssemblyLoader
    {
#pragma warning disable S3902 // "Assembly.GetExecutingAssembly" should not be called
        public Assembly? Load() => Assembly.GetExecutingAssembly();
#pragma warning restore S3902 // "Assembly.GetExecutingAssembly" should not be called
    }

    public class MongoEntityTypeProviderFromEntityType(Type entityType) : IMongoEntityTypeProviderAssemblyLoader
    {
        private readonly Type entityType = entityType;
        public Assembly? Load() => entityType.Assembly;
    }

#if false
        //public static IServiceCollection AddMongoDbContext<ProviderType>(this IServiceCollection services, IConfiguration configuration) where ProviderType : class, IMongoEntityTypeProvider
        //{
        //    return services.AddMongoDbContext<MongoEntityTypeProviderEntryAssemblyLoader, ProviderType>(configuration);
        //}
        //public static IServiceCollection AddMongoDbContext<ProviderType>(this IServiceCollection services, IConfiguration configuration, string assemblyName) where ProviderType : class, IMongoEntityTypeProvider
        //{
        //    return AddMongoDbContext<ProviderType>(services, configuration, new MongoEntityTypeProviderFromAssemblyName(assemblyName));
        //}
    file class MongoEntityTypeProviderExecutingAssemblyLoader : IMongoEntityTypeProviderAssemblyLoader
    {
        public Assembly? Load() => Assembly.GetExecutingAssembly();
    } 
    //file class MongoEntityTypeProviderEntryAssemblyLoader : IMongoEntityTypeProviderAssemblyLoader
    //{
    //    public Assembly? Load() => Assembly.Load("IMS.Core.Model.Mongo"); // Geçici çözüm
    //}
        //public class MongoEntityTypeProviderFromAssemblyName : IMongoEntityTypeProviderAssemblyLoader
    //{
    //    private readonly string assemblyName;

    //    public MongoEntityTypeProviderFromAssemblyName(string assemblyName)
    //    {
    //        this.assemblyName = assemblyName;
    //    }
    //    public Assembly? Load() => Assembly.Load(assemblyName);
    //}
#endif




}
