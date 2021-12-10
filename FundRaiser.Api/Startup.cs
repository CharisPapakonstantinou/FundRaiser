using FundRaiser.Common.Database;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Mappers.ConfigMappers;
using FundRaiser.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FundRaiser.Api
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
            var storageSettings = new StorageSettings();
            Configuration.Bind(StorageSettings.StorageSection, storageSettings);
            services.AddSingleton(storageSettings);

            services.AddControllers();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FundRaiser.Api", Version = "v1" });
            });

            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<IUpdateService, UpdateService>();
            services.AddScoped<IUserService, UserService>();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FundRaiser.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}