using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Repository;
namespace Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection sc, IConfiguration configuration)
        {
            var path = configuration["FirebaseKeysPath"] ?? Path.Combine(AppContext.BaseDirectory, "Firebase-Key.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            // Получаем ProjectId из конфигурации или подставляем твой ID проекта из Firebase
            var projectId = configuration["FirebaseProjectId"] ?? "moviesplatform-d1d9b";

            // Регистрируем FirestoreDb как Singleton, чтобы он переиспользовался во всем приложении
            sc.AddSingleton(FirestoreDb.Create(projectId));

            // Регистрируем репозиторий
            sc.AddScoped<IFilmRepository, FirebaseFilmRepository>();

            return sc;
        }
    }
}
