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
            //Cookie�F�؂̐ݒ�
            services.Configure<CookiePolicyOptions>(o =>
            {
                o.Secure = CookieSecurePolicy.Always;   //�N�b�L�[��HTTPS�ł̂ݑ��M
                o.HttpOnly = HttpOnlyPolicy.Always;     //�N���C�A���g���̃X�N���v�g����Cookie�͐G��Ȃ�
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
            
            //CORS�̐ݒ�
            services.AddCors(options =>
            {
                options.AddPolicy(name: LocalAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(new string[] { "https://localhost:3000" })
                            .AllowCredentials();
                    });
            });

            services.AddControllers();

            //DI�R���e�i��DbContext�o�^ ���ڑ��������VMoriContextFactory�Őݒ�
            string connectionString = Configration.GetConnectionString("DefaultConnection");

            services.AddEntityFrameworkSqlServer().AddDbContext<VMoriContext>(o => {
                o.UseSqlServer(Configration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IDbContext>(provider => provider.GetService<VMoriContext>());

            //DI�R���e�i��EfgRepository�̓o�^
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //DI�R���e�i��Utilitu�̓o�^
            services.AddScoped(typeof(IDateTimeUtility), typeof(DateTimeUtility));

            //DI�R���e�i��StorageService�̓o�^
            services.AddScoped(typeof(IStorageService), typeof(AzureBlobService));
            services.AddScoped(typeof(IAccountStorageService), typeof(AccountStorageService));

            //DI�R���e�i��DataService�̓o�^
            services.AddScoped(typeof(IAccountDataService), typeof(AccountDataService));
            services.AddScoped(typeof(IAppReqMailDataService), typeof(AppReqMailDataService));

            //DI�R���e�i��Service�̓o�^
            services.AddScoped(typeof(IRecommendVideosService), typeof(RecommendVideosService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IHashService), typeof(HashService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IMailService), typeof(SendGridService));

            //DI�R���e�i��Worker�̓o�^
            services.AddScoped(typeof(IRecommendVideosWorker), typeof(RecommendVideosWorker));
            services.AddScoped(typeof(IAuthWorker), typeof(AuthWorker));
            services.AddScoped(typeof(IAccountWorker), typeof(AccountWorker));

            //DI�R���e�i��Config�̐ݒ�
            services.Configure<MailConfig>(this.Configration.GetSection("Mail"));
            services.Configure<ClientConfig>(this.Configration.GetSection("Client"));
            services.Configure<StorageConfig>(this.Configration.GetSection("Storage"));
        }
    }
}