using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
        /// メールアドレス認証要求
        /// </summary>
        public DbSet<AppReqMail> AppReqMails { get; set; }

        /// <summary>
        /// Youtube動画
        /// </summary>
        public DbSet<YoutubeVideo> YoutubeVideos { get; set; }

        /// <summary>
        /// Youtube動画統計情報
        /// </summary>
        public DbSet<YoutubeVideoStatistics> YoutubeVideoStatistics { get; set; }

        /// <summary>
        /// Youtube動画のuploadリクエスト情報
        /// </summary>
        public DbSet<UpReqYoutubeVideo> UpReqYoutubeVideos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //アカウント情報
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>().HasKey(x => new { x.ID });

            //パスワード変更要求
            modelBuilder.Entity<ChangeReqPassword>().ToTable("ChangeReqPassword");
            modelBuilder.Entity<ChangeReqPassword>().HasKey(x => new { x.ID, x.AccountID });

            //メールアドレス認証要求
            modelBuilder.Entity<AppReqMail>().ToTable("AppReqMail");
            modelBuilder.Entity<AppReqMail>().HasKey(x => new { x.ID, x.Token });

            //Youtube動画
            var youtubeVideoBuilder = modelBuilder.Entity<YoutubeVideo>().ToTable("YoutubeVideo");
            //youtubeVideoBuilder
            //    .Property(x => x.Tags)
            //    .HasConversion(
            //        x => string.Join(',', x),
            //        x => x.Split(',', StringSplitOptions.RemoveEmptyEntries));
            ////.HasConversion(
            ////    x => this.ConvertEnumArrayToString(x),
            ////    x => this.ConvertStringToEnumArray<string>(x));
            //youtubeVideoBuilder
            //    .Property(x => x.Langes)
            //    .HasConversion(
            //        x => string.Join(',', x),
            //        x => x.Split(',').ToList().ConvertAll(j => (VideoLanguageKinds)Enum.Parse(typeof(VideoLanguageKinds),j));
            //youtubeVideoBuilder
            //    .Property(x => x.LangForTranslation)
            //    .HasConversion(this.ConvertEnumArrayToString<VideoLanguageKinds[]>(),
            //        x => this.ConvertStringToEnumArray<VideoLanguageKinds>(x));

            //Youtube動画統計情報
            var youtubeVideoStatisticsBuilder = modelBuilder.Entity<YoutubeVideoStatistics>().ToTable("YoutubeVideoStatistics");
            //リレーションの設定
            youtubeVideoStatisticsBuilder
                .HasOne(x => x.YoutubeVideo)
                .WithMany(x => x.Statistics);

           
            //Youtube動画のアップロードリクエスト情報
            modelBuilder.Entity<UpReqYoutubeVideo>().ToTable("UploadReqYoutubeVideo");
        }
        
        /// <summary>
        /// Enumの配列を文字列に変換
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        public Expression<Func<T, string>> ConvertEnumArrayToString<T>()
        {
            return x =>  string.Join(',', x);
        }

        /// <summary>
        /// データベースに格納されているEnum配列の文字列を、Enum配列に変換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public Expression<Func<string, T[]>> ConvertStringToEnumArray<T>(string target)
        {
            return x => this.aa<T>(target).ToArray();
        }

        private List<T> aa<T>(string target)
        {
            var list = target.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            var result = new List<T>();
            list.ForEach(l =>
            {
                result.Add((T)Enum.ToObject(typeof(T), int.Parse(l)));
            });
            return result;
        }
    }

}
