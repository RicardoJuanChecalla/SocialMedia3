//using System.Xml.Xsl.Runtime;
using System.Runtime.Intrinsics.X86;
using System.Collections.Specialized;
using System.Text;
using System.Buffers;
using System.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SocialMedia3.Infrastructure.Repositories;
using SocialMedia3.Infrastructure.Data;
using SocialMedia3.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using AutoMapper;
using SocialMedia3.Infrastructure.Filters;
using FluentValidation.AspNetCore;
using SocialMedia3.Core.Services;
using SocialMedia3.Infrastructure.Services;
using SocialMedia3.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using SocialMedia3.Core.CustomEntities;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SocialMedia3.Infrastructure.Options;
using SocialMedia3.Infrastructure.Extensions;
using FluentValidation;

namespace SocialMedia3.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers( options =>{
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                }   
            );

            //resuleve la configuracion de paginacion a una clase
            //services.Configure<PaginationOption>(Configuration.GetSection("Pagination"));
            //resuleve mapeo de la configuracion de PasswordOptions
            //services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));
            services.AddOptions(Configuration);

            //resuelve cadena de conexion
            services.AddDbContexts(Configuration);
            // services.AddDbContext<SocialMediaContext>(options => 
            //     options.UseSqlServer(Configuration.GetConnectionString("SocialMedia"))
            // );


            //resuelve dependencias
            services.AddServices();
            // services.AddTransient<IPostService, PostService>();
            //  services.AddTransient<ISecurityService, SecurityService>();
            // services.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>));
            // services.AddTransient(typeof(IUnitOfWork),typeof(UnitOfWork));
            // services.AddSingleton(typeof(IPasswordService),typeof(PasswordService));
            // services.AddSingleton<IUriService>(provider=>
            // {
            //     var accesor = provider.GetRequiredService<IHttpContextAccessor>();
            //     var request = accesor.HttpContext.Request;
            //     var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            //     return new UriService(absoluteUri);
            // });

            //inicializa mapeo con dto
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
           //documentacion
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1",new OpenApiInfo {Title="Social Media API", Version ="v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                doc.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(options=>
            {
	            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>{
	            options.TokenValidationParameters =  new TokenValidationParameters{
	                ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]!))	
	            };
            });

           //inicializa validadores
           services.AddMvc(options =>{
               //options.Filters.Add<ValidationFilter>();
           });

           //inicializa fluent
            // services.AddFluentValidation(options =>{
            //     options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            // });
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI( options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json","Social Media API");	
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
