using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using IMDB.Api.Core;
using IMDB.Api.Core.Interfaces;
using IMDB.Api.Filters;
using IMDB.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCore.AutoRegisterDi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IMDB.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container i.e configure dependency injection
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            //Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //Get App Settings
            var appSettings = appSettingsSection.Get<AppSettings>();

            //Configure JWT Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                    ValidateIssuer = false,
                    ValidIssuer = appSettings.Issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

            });

            services.AddMemoryCache();


            services.AddMvc(
                        options =>{
                            options.ReturnHttpNotAcceptable = true;
                            options.RespectBrowserAcceptHeader = true;
                            options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                            options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                            options.Filters.Add(typeof(ValidateModelAttribute));
                        }
                     )
                    .AddJsonOptions(options => {

                            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Ignores cycles in related data serialization
                            if (options.SerializerSettings.ContractResolver != null){

                                var castedResolver = options.SerializerSettings.ContractResolver as DefaultContractResolver;
                                castedResolver.NamingStrategy = null;
                            }
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(Configuration);

            var connectionString = "Server = localhost,1433; Database = IMDB; User ID= sa; Password = lexEME5195..";
            services.AddDbContext<DatabaseContext>(o => { o.UseSqlServer(connectionString); });


            var assemblyToScan = Assembly.Load("IMDB.Api");

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Register Services
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                     .Where(x => x.Name.EndsWith("Service"))
                     .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);


            //Register Repositories
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                     .Where(x => x.Name.EndsWith("Repository"))
                     .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);


            //Add AutoMapper
            services.AddAutoMapper();

            services.AddHttpCacheHeaders(

                (expirationModelOptions)=>
                {
                    expirationModelOptions.MaxAge = 60;
                },
                (validationModelOptions)=> 
                { 
                    validationModelOptions.MustRevalidate = true; 
                });


            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline i.e Middlewares, routing rules etc.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod()
                        );

            app.UseAuthentication();
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseMvc();
        }
    }
}
