using CanWeFixItApi.Middleware;
using CanWeFixItRepository.Models;
using CanWeFixItRepository.Repositories;
using CanWeFixItService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace CanWeFixItApi
{
    /// <summary>
    /// Startup class 
    /// </summary>
    public class Startup
    {
        private readonly string _databaseName = "CanWeFixItDB";

        /// <summary>
        /// Constructor of the Startup class
        /// </summary>
        /// <param name="configuration"> Instance of the IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        /// <summary>
        /// Gets called by the runtime.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Initialize in-memory database
            services.AddDbContext<CanWeFixItContext>(e=>e.UseInMemoryDatabase(_databaseName));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CanWeFixItApi", Version = "v1" });
            });

            // Inject Repositories
            services.AddScoped<IMarketDataRepository, MarketDataRepository>();
            services.AddScoped<IInstrumentRepository, InstrumentRepository>();

            // Inject Services
            services.AddScoped<IDatabaseSeedService, DatabaseSeedService>();
            services.AddScoped<IMarketDataService, MarketDataService>();
            services.AddScoped<IInstrumentService, InstrumentService>();
            services.AddScoped<LogContextMiddleware>();
        }

        /// <summary>
        /// configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CanWeFixItApi v1"));
            }

            // Populate in-memory database with data
            var scope = app.ApplicationServices.CreateScope();

            var databaseSeedService = scope.ServiceProvider.GetService(typeof(IDatabaseSeedService)) as IDatabaseSeedService;

            databaseSeedService?.seedDatabase();
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware<LogContextMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}