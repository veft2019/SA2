﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exterminator.Repositories.Data;
using Exterminator.Repositories.Implementations;
using Exterminator.Repositories.Interfaces;
using Exterminator.Services.Implementations;
using Exterminator.Services.Interfaces;
using Exterminator.WebApi.ExceptionHandlerExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Exterminator.WebApi
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
            services.AddMvc();

            // TODO: Register dependencies
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IGhostbusterService, GhostbusterService>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IGhostbusterRepository, GhostbusterRepository>();
            services.AddSingleton<IGhostbusterDbContext, GhostbusterDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGlobalExceptionHandler();
            app.UseMvc();
        }
    }
}
