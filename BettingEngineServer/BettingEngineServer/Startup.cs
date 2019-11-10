using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingEngineServer.Classes;
using BettingEngineServer.Interfaces;
using BettingEngineServer.Repositories;
using BettingEngineServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BettingEngineServer
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
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IMarketService, MarketService>();
            services.AddScoped<IBetService, BetService>();

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IMarketRepository, MarketRepository>();
            services.AddScoped<IBetRepository, BetRepository>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}