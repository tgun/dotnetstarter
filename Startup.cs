using AutoMapper;
using dotnetstarter.Common;
using dotnetstarter.Mapping;
using dotnetstarter.Persistence;
using dotnetstarter.Repositories;
using dotnetstarter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace dotnetstarter {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            string authSecret = Configuration[Constants.AuthSecretPreference];

            // -- Configure Jwt Auth
            services.UseJwtAuth(authSecret); // -- Uses our extention method to abstract and clean up the setup.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // -- Configure a database
            services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("Example-In-Memory"); });
            // -- Configure Dependency injection
            services.AddScoped<IExampleRepository, ExampleRepository>();
            services.AddScoped<IExampleService, ExampleService>();
            services.AddAutoMapper(typeof(ModelToResourceProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseMetricServer();
            app.UseHttpMetrics();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseExceptionHandler("/error"); // -- Directs any issues that occur into the error controller.
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
