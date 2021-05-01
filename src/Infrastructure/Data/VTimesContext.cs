﻿using ApplicationCore.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoInfo>().ToTable("VideoInfo");
        }
    }
}
