using ApplicationCore.DataServices;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces._DataServices;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            app.UseCookiePolicy();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Cookie�F�؂̐ݒ�
            services.Configure<CookiePolicyOptions>(o =>
            {
                o.Secure = CookieSecurePolicy.Always;   //�N�b�L�[��HTTPS�ł̂ݑ��M
                o.HttpOnly = HttpOnlyPolicy.Always;     //�N���C�A���g���̃X�N���v�g����Cookie�͐G��Ȃ�
            });

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            
            //CORS�̐ݒ�
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

            //DI�R���e�i��DataService�̓o�^
            services.AddScoped(typeof(IAccountDataService), typeof(AccountDataService));

            //DI�R���e�i��Service�̓o�^
            services.AddScoped(typeof(IRecommendVideosService), typeof(RecommendVideosService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            //DI�R���e�i��Worker�̓o�^
            services.AddScoped(typeof(IRecommendVideosWorker), typeof(RecommendVideosWorker));
            services.AddScoped(typeof(IAuthWorker), typeof(AuthWorker));
        }
    }
}
