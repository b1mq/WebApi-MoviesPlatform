
using Application.Extensions;
using Infrastructure.Extensions;
namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
          

            // Configure the HTTP request pipeline.
           

            try
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowReactApp", policy =>
                    {
                        policy.WithOrigins("http://localhost:3000") // Замени на актуальный порт твоего React-приложения
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
                });
                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.UseCors("AllowReactApp");
                app.MapControllers(); 

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CRITICAL ERROR: {ex.Message}");
                Console.WriteLine($"INNER: {ex.InnerException?.Message}");
                throw;
            }
        }
    }
}
