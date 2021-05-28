using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// VMoriDbContext
    /// </summary>
    public class VMoriContext : DbContext, IDbContext
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="options"></param>
        public VMoriContext(DbContextOptions options) : base(options){}

        /// <summary>
        /// 動画情報
        /// </summary>
        public DbSet<VideoInfo> VideoInfos { get; set; }
        
        /// <summary>
        /// アカウント情報
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// パスワード変更要求
        /// </summary>
        public DbSet<ChangeReqPassword> ChangeReqPasswords { get; set; }

        /// <summary>
        /// Youtube動画
        /// </summary>
        public DbSet<YoutubeVideo> YoutubeVideos { get; set; }

        /// <summary>
        /// Youtube動画統計情報
        /// </summary>
        public DbSet<YoutubeVideoStatistics> YoutubeVideoStatistics { get; set; }

        /// <summary>
        /// メールアドレス認証要求
        /// </summary>
        public DbSet<AppReqMail> AppReqMails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoInfo>().ToTable("VideoInfo");

            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>().HasKey(x => new { x.ID });

            modelBuilder.Entity<ChangeReqPassword>().ToTable("ChangeReqPassword");
            modelBuilder.Entity<ChangeReqPassword>().HasKey(x => new { x.ID, x.AccountID });

            modelBuilder.Entity<AppReqMail>().ToTable("AppReqMail");
            modelBuilder.Entity<AppReqMail>().HasKey(x => new { x.ID, x.Token });

            modelBuilder.Entity<YoutubeVideo>().ToTable("YoutubeVideo");

            //リレーションの設定
            modelBuilder.Entity<YoutubeVideoStatistics>()
                .HasOne(x => x.YoutubeVideo)
                .WithMany(x => x.Statistics);

            modelBuilder.Entity<YoutubeVideoStatistics>().ToTable("YoutubeVideoStatistics");
        }
    }
}
