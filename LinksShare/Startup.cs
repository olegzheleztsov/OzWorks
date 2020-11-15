using AutoMapper;
using LinksShare.Models;
using LinksShare.Services;
using LinksShare.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;


namespace LinksShare
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
            services.Configure<BookstoreDatabaseSettings>(Configuration.GetSection(nameof(BookstoreDatabaseSettings)));
            services.AddSingleton<IBookstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);

            services.Configure<LinksDatabaseSettings>(Configuration.GetSection(nameof(LinksDatabaseSettings)));
            services.AddSingleton<ILinksDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<LinksDatabaseSettings>>().Value);

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));

            services.AddSingleton<BookService>();
            services.AddScoped<ILinkService, LinkService>();

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson(options => options.UseMemberCasing());

            services.AddRazorPages().AddMicrosoftIdentityUI();
            services.AddAutoMapper(typeof(Startup).Assembly);
            //services.Configure<AntiforgeryOptions>(options =>
            //{
            //    options.HeaderName = "X-XSRF-TOKEN";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.Use(async (context, next) =>
            //{
            //    if (!context.Request.Path.StartsWithSegments("/api"))
            //    {
            //        context.Response.Cookies.Append("XSRF-TOKEN",
            //            antiforgery.GetAndStoreTokens(context).RequestToken,
            //            new Microsoft.AspNetCore.Http.CookieOptions { HttpOnly = false });
            //    }
            //});

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Links}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
