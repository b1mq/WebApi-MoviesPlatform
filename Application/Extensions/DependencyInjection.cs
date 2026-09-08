using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection sc)
        {
            sc.AddScoped<IFilmService, FilmService>();
            sc.AddValidatorsFromAssembly(typeof(ApplicationServiceExtension).Assembly);
            return sc;
        }
    }
}
