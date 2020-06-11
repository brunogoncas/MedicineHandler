namespace MedicineHandler.Application.DependencyInjection
{
    using FluentValidation;
    using MedicineHandler.Application.Configuration;
    using MedicineHandler.Application.Repositories;
    using MedicineHandler.Application.Services;
    using MedicineHandler.Application.Validation;
    using MedicineHandler.DTO.Entities;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Driver;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ISettings settings)
        {
            services
                .AddValidation()
                .AddServices()
                .AddRepositories()
                .AddMongoDb(settings.Databases);

            return services;
        }

        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<Medication>, MedicationValidator>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IMedicationService, MedicationService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IMedicationRepository, MedicationRepository>();

            return services;
        }

        private static IServiceCollection AddMongoDb(
            this IServiceCollection services,
            DatabasesSettings mongoDbSettings)
        {
            services.AddSingleton<IMongoClient>(_ =>
            {
                var mongoClientSettings = new MongoClientSettings
                {
                    Servers = new[] { MongoServerAddress.Parse(mongoDbSettings.MongoDBConnectionString) }
                };

                return new MongoClient(mongoClientSettings);
            });

            services.AddSingleton(p => p.GetRequiredService<IMongoClient>().GetDatabase(mongoDbSettings.MongoDBName));

            return services;
        }
    }
}
