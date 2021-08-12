using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using SocialMedia3.Infrastructure.Data;
using SocialMedia3.Infrastructure.Options;
using SocialMedia3.Core.CustomEntities;
using SocialMedia3.Core.Interfaces;
using SocialMedia3.Core.Services;
using SocialMedia3.Infrastructure.Interfaces;
using SocialMedia3.Infrastructure.Services;
using SocialMedia3.Infrastructure.Repositories;

namespace SocialMedia3.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services,IConfiguration configuration )
        {
             //resuelve cadena de conexion
            services.AddDbContext<SocialMediaContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("SocialMedia"))
            );
            return services;
        }
        public static IServiceCollection AddOptions(this IServiceCollection services,IConfiguration configuration )
        {
            //resuleve la configuracion de paginacion a una clase
            services.Configure<PaginationOption>(configuration.GetSection("Pagination"));

            //resuleve mapeo de la configuracion de PasswordOptions
            services.Configure<PasswordOptions>(configuration.GetSection("PasswordOptions"));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //resuelve dependencias
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>));
            services.AddTransient(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddSingleton(typeof(IPasswordService),typeof(PasswordService));
            services.AddSingleton<IUriService>(provider=>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
            return services;
        }
    }
}