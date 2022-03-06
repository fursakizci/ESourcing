using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESourcing.Sourcing.Data.Interface;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories;
using ESourcing.Sourcing.Repositories.Interfaces;
using ESourcing.Sourcing.Settings;
using Microsoft.Extensions.Options;

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

            services.AddTransient<ISourcingContext, ISourcingContext>();

            services.AddTransient<IAuctionRepository, AuctionRepository>();
            
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
