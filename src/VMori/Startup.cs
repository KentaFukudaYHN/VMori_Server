using ApplicationCore.Config;
using ApplicationCore.DataServices;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces._DataServices;
using ApplicationCore.Interfaces._Services.Channel;
using ApplicationCore.Services;
using ApplicationCore.Services.Channel;
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
using VMori.Interfaces.Channel;
using VMori.Workers;
using VMori.Workers._Video;
using VMori.Workers.Channel;

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
                o.HttpOnly = HttpOnlyPolicy.Always;     //�N���C�A���g���̃X�N���v�g����Cookie�͐G��Ȃ�,
                o.MinimumSameSitePolicy = SameSiteMode.None;
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
                            .WithOrigins(new string[] { "https://" + this.Configration.GetSection("Client").GetValue(typeof(string), "Domain") as string })
                            .AllowCredentials();
                    });
            });

            services.AddControllers();

            //HTTpClient�̒ǉ�
            services.AddHttpClient();

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
            services.AddScoped(typeof(IChangeReqPasswordDataService), typeof(ChangeReqPasswordDataService));
            services.AddScoped(typeof(IVideoDataService), typeof(VideoDataService));
            services.AddScoped(typeof(IUpReqOutsourceVideoDataService), typeof(UpReqOutsourceVideoDataService));
            services.AddScoped(typeof(IOutsouceVideoChannelDataService), typeof(OutsourceVideoChannelDataService));
            services.AddScoped(typeof(IChannelTransitionDataService), typeof(ChannelTransitionDataService));
            services.AddScoped(typeof(IVideoCommentDataService), typeof(VideoCommentDataService));
            services.AddScoped(typeof(IVideoHistoryDataService), typeof(VideoHistoryDataService));
            services.AddScoped(typeof(IChannelDataService), typeof(ChannelDataService));

            //DI�R���e�i��Service�̓o�^
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IHashService), typeof(HashService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IMailService), typeof(SendGridService));
            services.AddScoped(typeof(IVideoService), typeof(VideoService));
            services.AddScoped(typeof(IYoutubeService), typeof(YoutubeService));
            services.AddScoped(typeof(INikoNikoService), typeof(NikoNikoService));
            services.AddScoped(typeof(IVideoCommentService), typeof(VideoCommentService));
            services.AddScoped(typeof(IChannelService), typeof(ChannelService));

            //DI�R���e�i��Worker�̓o�^
            services.AddScoped(typeof(IAuthWorker), typeof(AuthWorker));
            services.AddScoped(typeof(IAccountWorker), typeof(AccountWorker));
            services.AddScoped(typeof(IUploadVideoWorker), typeof(UploadVideoWorker));
            services.AddScoped(typeof(IVideoWorker), typeof(VideoWorker));
            services.AddScoped(typeof(IChannelWorker), typeof(ChannelWorker));

            //DI�R���e�i��Config�̐ݒ�
            services.Configure<MailConfig>(this.Configration.GetSection("Mail"));
            services.Configure<ClientConfig>(this.Configration.GetSection("Client"));
            services.Configure<StorageConfig>(this.Configration.GetSection("Storage"));
            services.Configure<YoutubeConfig>(this.Configration.GetSection("Youtube"));
        }
    }
}
