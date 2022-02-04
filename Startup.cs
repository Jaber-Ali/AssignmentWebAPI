using AssignmentWebAPI.Models;
using AssignmentWebAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AssignmentWebAPI.ServiceRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AssignmentWebAPI
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
            //Adding services which uses IServiceRepositories
            services.AddScoped(typeof(ICharacterService), typeof(CharacterService));
            services.AddScoped(typeof(IMovieService), typeof(MovieService));
            services.AddScoped(typeof(IFranchiseService), typeof(FranchiseService));

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));


            services.AddDbContext<MovieCharacterDbContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Swagger doc config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AssignmentWebAPI",
                    Version = "v1",
                    Description = "Assignment 3 ASP.NET Core Web API for movies-characters-& Franchises",
                    Contact = new OpenApiContact
                    {
                        Name = "Jaber Ali, Experis",
                        Url = new Uri("https://github.com/Jaber-Ali/AssignmentWebAPI"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://choosealicense.com/licenses/mit/"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);


            });
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssignmentWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
