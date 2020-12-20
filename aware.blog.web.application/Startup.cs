using System.Reflection;

using AutoMapper;

using Aware.Blog.Domain;
using Aware.Blog.Domain.EF;
using Aware.Blog.Mapping.Profiles;
using Aware.Blog.Validation;
using Aware.Blog.Web.Application.Middlewares;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aware.Blog.Web.Application
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
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                    // Set default settings
                    JsonSerializerSettings defaultSettings = null;
                    if (JsonConvert.DefaultSettings != null)
                        defaultSettings = JsonConvert.DefaultSettings();

                    if (defaultSettings == null)
                        defaultSettings = new JsonSerializerSettings();

                    defaultSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    defaultSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    defaultSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                    JsonConvert.DefaultSettings = () => defaultSettings;
                })
                .AddFluentValidation(options =>
                {
                });

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("AwareBlogDb"));
                });

            services.AddTransient<IGenericValidator, GenericValidator>()
                .AddTransient<IValidatorInterceptor, FluentValidationInterceptor>();

            // Auto register validators that are not marked with "ExcludeAttribute"
            FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining(typeof(GenericValidator))
                .ForEach(pair =>
                {
                    var excludeAttribute = pair.ValidatorType.GetCustomAttribute<ExcludeAttribute>(false);
                    if (excludeAttribute != null)
                        return;

                    services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
                });

            // Add AutoMapper and Profiles in this assembly
            services.AddAutoMapper(options =>
            {
            },
            new Assembly[] { typeof(BaseProfile).Assembly });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
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

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}