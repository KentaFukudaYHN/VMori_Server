using ApplicationCore.Interfaces;
using ApplicationCore.Services._RecommendVideos;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VMori.Interfaces;
using VMori.Workers;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configration { get; }
        readonly string LocalAllowSpecificOrigins = "_localAllowSpecificOrigins";

        public Startup(IConfiguration configration)
        {
            Configration = configration;
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

            app.UseCors(LocalAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: LocalAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(new string[] { "http://localhost", "https://vmireba-client-dev.azurewebsites.net" });
                    });
            });

            services.AddControllers();

            //DI�R���e�i��DbContext�o�^ ���ڑ��������VMoriContextFactory�Őݒ�
            string connectionString = Configration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<VMoriContext>( o => o.UseSqlServer(connectionString));

            //DI�R���e�i��EfgRepository�̓o�^
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //DI�R���e�i��Service�̓o�^
            services.AddScoped(typeof(IRecommendVideosService), typeof(RecommendVideosService));

            //DI�R���e�i��Worker�̓o�^
            services.AddScoped(typeof(IRecommendVideosWorker), typeof(RecommendVideosWorker));
        }
    }
}
