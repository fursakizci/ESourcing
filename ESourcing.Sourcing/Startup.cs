using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ESourcing.Sourcing.Data.Interface;
using ESourcing.Sourcing.Repositories;
using ESourcing.Sourcing.Repositories.Interfaces;
using ESourcing.Sourcing.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ESourcing.Sourcing.Data;

namespace ESourcing.Sourcing
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
            services.AddRazorPages();

            services.Configure<ISourcingDatabaseSettings>(Configuration.GetSection(nameof(SourcingDatabaseSettings)));
            
            services.AddSingleton<ISourcingDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);

            #region ProjectDependencies

            services.AddTransient<ISourcingContext, SourcingContext>();

            services.AddTransient<IAuctionRepository, AuctionRepository>();

            services.AddTransient<IBidRepository, BidRepository>();

            #endregion

            #region Swagger Dependecies

            services.AddControllers();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1",new OpenApiInfo
                {
                    Title = "ESourcing.Sourcing",
                    Version = "V1"
                });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sourcing API V1");
                });
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            
        }
    }
}
