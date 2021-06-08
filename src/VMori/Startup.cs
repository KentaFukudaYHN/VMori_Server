using ApplicationCore.Config;
using ApplicationCore.DataServices;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.Utility;
using Infrastructure.Data;
using Infrastructure.Mail;
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
                            .WithOrigins(new string[] { "https://" + this.Configration.GetSection("Client").GetValue(typeof(string), "Domain") as string })
                            .AllowCredentials();
                    });
            });

            services.AddControllers();

            //DIコンテナにDbContext登録 ※接続文字列はVMoriContextFactoryで設定
            string connectionString = Configration.GetConnectionString("DefaultConnection");

            services.AddEntityFrameworkSqlServer().AddDbContext<VMoriContext>(o => {
                o.UseSqlServer(Configration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IDbContext>(provider => provider.GetService<VMoriContext>());

            //DIコンテナにEfgRepositoryの登録
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //DIコンテナにUtilituの登録
            services.AddScoped(typeof(IDateTimeUtility), typeof(DateTimeUtility));

            //DIコンテナにStorageServiceの登録
            services.AddScoped(typeof(IStorageService), typeof(AzureBlobService));
            services.AddScoped(typeof(IAccountStorageService), typeof(AccountStorageService));

            //DIコンテナにDataServiceの登録
            services.AddScoped(typeof(IAccountDataService), typeof(AccountDataService));
            services.AddScoped(typeof(IAppReqMailDataService), typeof(AppReqMailDataService));
            services.AddScoped(typeof(IChangeReqPasswordDataService), typeof(ChangeReqPasswordDataService));
            services.AddScoped(typeof(IYoutubeVideoDataService), typeof(YoutubeVideoDataService));
            services.AddScoped(typeof(IYoutubeVideoStatisticsDataService), typeof(YoutubeVideoStatisticsDataService));
            services.AddScoped(typeof(IUpReqYoutubeVideoDataService), typeof(UpReqYoutubeVideoDataService));

            //DIコンテナにServiceの登録
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IHashService), typeof(HashService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IMailService), typeof(SendGridService));
            services.AddScoped(typeof(IYoutubeVideoService), typeof(YoutubeVideoService));

            //DIコンテナにWorkerの登録
            services.AddScoped(typeof(IAuthWorker), typeof(AuthWorker));
            services.AddScoped(typeof(IAccountWorker), typeof(AccountWorker));
            services.AddScoped(typeof(IUploadVideoWorker), typeof(UploadVideoWorker));

            //DIコンテナにConfigの設定
            services.Configure<MailConfig>(this.Configration.GetSection("Mail"));
            services.Configure<ClientConfig>(this.Configration.GetSection("Client"));
            services.Configure<StorageConfig>(this.Configration.GetSection("Storage"));
            services.Configure<YoutubeConfig>(this.Configration.GetSection("Youtube"));
        }
    }
}
