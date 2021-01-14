using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SimplePages.Config;
using SimplePages.Models.AdventureWorks;
using SimplePages.Services;
using SimplePages.Services.Interfaces;

namespace SimplePages
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
            services.Configure<GymSettings>(Configuration.GetSection(nameof(GymSettings)));
            services.AddSingleton<IGymSettings>(sp => sp.GetRequiredService<IOptions<GymSettings>>().Value);
            
            services.AddDbContext<AdventureWorksDbContext>(options =>
            {
                var connectionString =
                    "Data Source=DESKTOP-0C3QBAG;Integrated Security=True;Initial Catalog=AdventureWorks2019;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                options.UseSqlServer(connectionString, configuration =>
                {
                    configuration.UseHierarchyId();
                }).EnableSensitiveDataLogging().LogTo(Console.WriteLine);
            });

            services.AddScoped<IGymService, GymService>();
            
            services.AddScoped<IZooService, ZooService>();
            services.AddRazorPages();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<IExerciseNames, ExerciseNames>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}