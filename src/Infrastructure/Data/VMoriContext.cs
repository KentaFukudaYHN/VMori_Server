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
        /// Outsource動画
        /// </summary>
        public DbSet<OutsourceVideo> OutsourceVideos { get; set; }

        /// <summary>
        /// Outsource動画統計情報
        /// </summary>
        public DbSet<OutsourceVideoStatistics> OutsourceVideoStatistics { get; set; }

        /// <summary>
        /// Outsouceチャンネル情報
        /// </summary>
        public DbSet<OutsourceVideoChannel> OutsouceVideoChannels { get; set; }

        /// <summary>
        /// チャンネル推移データ
        /// </summary>
        public DbSet<ChannelTransition> ChannelTransitions { get; set; }

        /// <summary>
        /// Outsource動画のuploadリクエスト情報
        /// </summary>
        public DbSet<UpReqOutsourceVideo> UpReqOutsourceVideos { get; set; }

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

            //Outsource動画
            var OutsourceVideoBuilder = modelBuilder.Entity<OutsourceVideo>().ToTable("OutsourceVideo");
            //OutsourceVideoBuilder
            //    .Property(x => x.Tags)
            //    .HasConversion(
            //        x => string.Join(',', x),
            //        x => x.Split(',', StringSplitOptions.RemoveEmptyEntries));
            ////.HasConversion(
            ////    x => this.ConvertEnumArrayToString(x),
            ////    x => this.ConvertStringToEnumArray<string>(x));
            //OutsourceVideoBuilder
            //    .Property(x => x.Langes)
            //    .HasConversion(
            //        x => string.Join(',', x),
            //        x => x.Split(',').ToList().ConvertAll(j => (VideoLanguageKinds)Enum.Parse(typeof(VideoLanguageKinds),j));
            //OutsourceVideoBuilder
            //    .Property(x => x.LangForTranslation)
            //    .HasConversion(this.ConvertEnumArrayToString<VideoLanguageKinds[]>(),
            //        x => this.ConvertStringToEnumArray<VideoLanguageKinds>(x));

            //Outsource動画統計情報
            var OutsourceVideoStatisticsBuilder = modelBuilder.Entity<OutsourceVideoStatistics>().ToTable("OutsourceVideoStatistics");
            //リレーションの設定
            OutsourceVideoStatisticsBuilder
                .HasOne(x => x.OutsourceVideo)
                .WithMany(x => x.Statistics);

           
            //Outsource動画のアップロードリクエスト情報
            modelBuilder.Entity<UpReqOutsourceVideo>().ToTable("UploadReqOutsourceVideo");

            //Outsouceチャンネル情報
            modelBuilder.Entity<OutsourceVideoChannel>().ToTable("OutsourceVideoChannel");

            //チャンネル推移データ
            modelBuilder.Entity<ChannelTransition>().ToTable("ChannelTransition");
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
