using ApplicationCore.DataServices;
using ApplicationCore.Interfaces;
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
using System.Threading.Tasks;
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

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseCors(LocalAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });

        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Cookie認証の設定
            services.Configure<CookiePolicyOptions>(o =>
            {
                o.Secure = CookieSecurePolicy.Always;   //クッキーはHTTPSでのみ送信
                o.HttpOnly = HttpOnlyPolicy.Always;     //クライアント側のスクリプトからCookieは触れない
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options => {
                    options.SlidingExpiration = true;
                    options.Events.OnRedirectToLogin = cxt =>
                    {
                        cxt.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToAccessDenied = cxt =>
                    {
                        cxt.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToLogout = cxt => Task.CompletedTask;
                });
            
            //CORSの設定
            services.AddCors(options =>
            {
                options.AddPolicy(name: LocalAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(new string[] { "http://localhost:3000" })
                            .AllowCredentials();
                    });
            });

            services.AddControllers();

            //DIコンテナにDbContext登録 ※接続文字列はVMoriContextFactoryで設定
            string connectionString = Configration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<VMoriContext>( o => o.UseSqlServer(connectionString));

            //DIコンテナにEfgRepositoryの登録
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //DIコンテナにDataServiceの登録
            services.AddScoped(typeof(IAccountDataService), typeof(AccountDataService));

            //DIコンテナにServiceの登録
            services.AddScoped(typeof(IRecommendVideosService), typeof(RecommendVideosService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IHashService), typeof(HashService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));

            //DIコンテナにWorkerの登録
            services.AddScoped(typeof(IRecommendVideosWorker), typeof(RecommendVideosWorker));
            services.AddScoped(typeof(IAuthWorker), typeof(AuthWorker));
            services.AddScoped(typeof(IAccountWorker), typeof(AccountWorker));
        }
    }
}
