using Commom.Services.PDFServices.Interfaces;
using Commom.Services.PDFServices.Providers;
using DinkToPdf;
using DinkToPdf.Contracts;
using Domains.Repositories;
using Infra.Data.Contexts;
using Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "OKEntrega",
                        ValidAudience = "OKEntrega",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OKEntrega-b71e507ae8f44b4396530166279942af"))
                    };
                });

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddTransient<IRazorPageRenderer, RazorPageRendererServices>();
            services.AddTransient<IPDFGenerator, PDFGeneratorServices>();

            services.AddRazorPages();

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

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
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
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

            //app.UseHttpsRedirection();

            //app.UseCors("CustomPermission");

            app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseStaticFiles();

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
