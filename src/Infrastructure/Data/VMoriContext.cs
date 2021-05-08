using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// VMoriDbContext
    /// </summary>
    public class VMoriContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoInfo>().ToTable("VideoInfo");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>().HasKey(x => new { x.ID, x.Mail }); //Accountテーブルはmailとidで複合キー

        }
    }
}
