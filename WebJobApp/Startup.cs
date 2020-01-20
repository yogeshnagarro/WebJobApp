namespace WebJobApp
{
    using System;
    using System.Threading.Tasks.Dataflow;
    using Hangfire;
    using Hangfire.SqlServer;
    using Hangfire.Storage;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using WebJobApp.BAL;
    using WebJobApp.Contracts;
    using WebJobApp.Filters;
    using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

    public class Startup
    {
        #region Constructors & Destructors

        public Startup(IConfiguration configuration) => Configuration = configuration;

        #endregion

        #region Properties

        #region Public Properties

        public IConfiguration Configuration { get; }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
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

            app.UseHangfireDashboard();
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBackgroundJobManager, BackgroundJobManager>();

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"),
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        UsePageLocksOnDequeue = true,
                        DisableGlobalLocks = true
                    })
                .UseFilter(new JobTransitionFilter())
            );

            // Add the processing server as IHostedService with Default settings
            services.AddHangfireServer();

            services.AddRazorPages();
            services.AddControllers();
        }

        #endregion

        #endregion
    }
}