using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Repository;
using WebApi.Services;

namespace WebApi
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
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IExampleRepository, ExampleRepository>();
            services.AddTransient<IExampleService, ExampleService>();

            services.AddHealthChecks().AddSqlServer(Configuration.GetConnectionString("DefaultConnection"), name: "baseSql");
            services.AddHealthChecksUI();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("WebApiSpec", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Web API",
                    Description = "API .NetCore 3.1, utilizando HealthChecks, Swagger",
                    Version = "1"
                });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);

            });
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
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/WebApiSpec/swagger.json", "WebApiSpec");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Ativando o middlweare de Health Check
            //app.UseHealthChecks("/status");
            app.UseHealthChecks("/status",
               new HealthCheckOptions()
               {
                   ResponseWriter = async (context, report) =>
                   {
                       var result = JsonConvert.SerializeObject(
                           new
                           {
                               statusApplication = report.Status.ToString(),
                               healthChecks = report.Entries.Select(e => new
                               {
                                   check = e.Key,
                                   ErrorMessage = e.Value.Exception?.Message,
                                   status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                               })
                           });
                       context.Response.ContentType = MediaTypeNames.Application.Json;
                       await context.Response.WriteAsync(result);
                   }
               });

            // Gera o endpoint que retornará os dados utilizados no dashboard
            app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI();



        }
    }
}