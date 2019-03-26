using ARMI.Data;
using ARMI.Services;
using ARMI.Services.Jobs;
using ARMI.Services.Scrapers;
using Coravel;
using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ARMI.Web
{
    public class Startup
    {
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<Config>(Configuration.GetSection("Config"));
            var connectionType = Configuration.GetSection("Config:ConnectionType").Value;
            services.AddDbContext<ApplicationDbContext>();
            //switch (connectionType)
            //{
            //    case "Sql":
            //        {
            //            services.AddDbContext<ApplicationDbContext>(options =>
            //            options.UseSqlServer(Configuration.GetConnectionString("SqlDatabase"), x =>
            //            {
            //                x.MigrationsAssembly("ARMI.SqlServer");
            //            }));
            //            break;
            //        }

            //    case "SqlLite":
            //    default:
            //        {
            //            services.AddDbContext<ApplicationDbContext>(options =>
            //            options.UseSqlite(Configuration.GetConnectionString("SqlLiteDatabase"), x =>
            //            {
            //                x.MigrationsAssembly("ARMI.SqlLiteServer");
            //            }));
            //            break;
            //        }
            //}
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            //Create a Coravel queue manager and SignalR for progressreporting
            services.AddQueue();
            _logger.LogInformation("Added Coravel Queue");
            services.AddScheduler();
            _logger.LogInformation("Added Coravel Scheduler");
            services.AddSignalR();
            _logger.LogInformation("Added SignalR Service");
            services.AddScoped<IJobService, JobService>();
            _logger.LogInformation("Added Internal JobService");

            // Add services
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IEmulatorService, EmulatorService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IFtpService, FtpService>();
            services.AddTransient<IImportService, ImportService>();
            services.AddTransient<IRomService, RomService>();
            services.AddTransient<IRomListService, RomListService>();

            // Add scraper services
            services.AddTransient<IArcadeDbScraperService, ArcadeDbScraperService>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var provider = app.ApplicationServices;
            provider.ConfigureQueue()
                .LogQueuedTaskProgress(provider.GetService<ILogger<IQueue>>());

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                DataSeed.SeedData(serviceScope);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSignalR(routes => { routes.MapHub<JobProgressHub>("/jobprogress"); });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
