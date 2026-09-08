using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Domain.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var path = configuration["FirebaseKeysPath"]
                ?? Path.Combine(
                    AppContext.BaseDirectory,
                    "Firebase-Key.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"Firebase key file not found: {path}");
            }

            Console.WriteLine($"Firebase key: {path}");

            var credential = GoogleCredential
                .FromFile(path)
                .CreateScoped("https://www.googleapis.com/auth/cloud-platform");

            Console.WriteLine(
                $"Firebase credential loaded successfully.");

            var projectId = "moviesplatform-d1d9b";

            var firestoreDb = new FirestoreDbBuilder
            {
                ProjectId = projectId,
                Credential = credential
            }.Build();

            services.AddSingleton(firestoreDb);
            services.AddScoped<IFilmRepository, FirebaseFilmRepository>();

            return services;
        }
    }
}