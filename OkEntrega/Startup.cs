using Domains.Repositories;
using Infra.Data.Contexts;
using Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace OkEntrega
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CustomPermission", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(
                            Configuration.GetSection("Origins")["WebSystem"], 
                            Configuration.GetSection("Origins")["Mobile"]
                        )
                        .AllowCredentials();
                });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddDbContext<DataContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OK-Entrega API",
                    Description = "API gateway do SaaS OK Entrega.",
                    TermsOfService = new Uri("https://github.com/OK-Entrega/OK-Entrega-API"),
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Amaral",
                        Email = "daniel.amaral720@gmail.com",
                        Url = new Uri("https://github.com/DanielMendesdoAmaral"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "LICX",
                        Url = new Uri("https://github.com/OK-Entrega/OK-Entrega-API"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            var assembly = AppDomain.CurrentDomain.Load("Domains");
            services.AddMediatR(assembly);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IShipperRepository, ShipperRepository>();
            services.AddTransient<IDelivererRepository, DelivererRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OK-Entrega API V1");
            });

            app.UseCors("CustomPermission");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
