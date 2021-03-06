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
        public DbSet<Video> Videos { get; set; }

        ///// <summary>
        ///// Outsource動画統計情報
        ///// </summary>
        //public DbSet<OutsourceVideoStatistics> OutsourceVideoStatistics { get; set; }

        /// <summary>
        /// 動画コメント
        /// </summary>
        public DbSet<VideoComment> VideoComments { get; set; }

        /// <summary>
        /// Outsouceチャンネル情報
        /// </summary>
        public DbSet<Channel> VideoChannels { get; set; }

        /// <summary>
        /// チャンネル推移データ
        /// </summary>
        public DbSet<ChannelTransition> ChannelTransitions { get; set; }

        /// <summary>
        /// Outsource動画のuploadリクエスト情報
        /// </summary>
        public DbSet<UpReqOutsourceVideo> UpReqVideos { get; set; }

        /// <summary>
        /// 動画視聴履歴
        /// </summary>
        public DbSet<VideoHistory> VideoHistorys { get; set; }

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
            var OutsourceVideoBuilder = modelBuilder.Entity<Video>().ToTable("Video");
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

            ////Outsource動画統計情報
            //var outsourceVideoStatisticsBuilder = modelBuilder.Entity<OutsourceVideoStatistics>().ToTable("OutsourceVideoStatistics");
            ////リレーションの設定
            //outsourceVideoStatisticsBuilder
            //    .HasOne(x => x.OutsourceVideo)
            //    .WithMany(x => x.Statistics);

           
            //Outsource動画のアップロードリクエスト情報
            modelBuilder.Entity<UpReqOutsourceVideo>().ToTable("UploadReqVideo");

            //Outsouceチャンネル情報
            modelBuilder.Entity<Channel>().ToTable("Channel");

            //チャンネル推移データ
            modelBuilder.Entity<ChannelTransition>().ToTable("ChannelTransition");

            //動画コメント
            var videoCommentBuilder = modelBuilder.Entity<VideoComment>().ToTable("VideoComment");
            videoCommentBuilder
                .HasOne(x => x.Video)
                .WithMany(x => x.VideoComments)
                .HasForeignKey(x => x.VideoId)
                .HasPrincipalKey(x => x.VideoId);

            //動画視聴履歴
            modelBuilder.Entity<VideoHistory>().ToTable("VideoHistory");


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
