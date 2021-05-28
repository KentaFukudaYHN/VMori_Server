﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(VMoriContext))]
    [Migration("20210528182607_Add_TB_UploadReqYoutubeVideo")]
    partial class Add_TB_UploadReqYoutubeVideo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Entities.Account", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("AppMail")
                        .HasColumnType("bit");

                    b.Property<string>("Birthday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("StorageID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Mail");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("ApplicationCore.Entities.AppReqMail", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID", "Token");

                    b.ToTable("AppReqMail");
                });

            modelBuilder.Entity("ApplicationCore.Entities.ChangeReqPassword", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RegistDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID", "AccountID");

                    b.HasIndex("Code");

                    b.ToTable("ChangeReqPassword");
                });

            modelBuilder.Entity("ApplicationCore.Entities.VideoInfo", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("VideoInfo");
                });

            modelBuilder.Entity("ApplicationCore.Entities.YoutubeVideo", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChanelId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChanelTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("VideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("YoutubeVideo");
                });

            modelBuilder.Entity("ApplicationCore.Entities.YoutubeVideoStatistics", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("CommentCount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<DateTime>("GetDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("LikeCount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("ViewCount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("YoutubeVideoID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("YoutubeVideoID");

                    b.ToTable("YoutubeVideoStatistics");
                });

            modelBuilder.Entity("ApplicationCore.Entities.UpReqYoutubeVideo", b =>
                {
                    b.HasBaseType("ApplicationCore.Entities.YoutubeVideo");

                    b.ToTable("UploadReqYoutubeVideo");
                });

            modelBuilder.Entity("ApplicationCore.Entities.YoutubeVideoStatistics", b =>
                {
                    b.HasOne("ApplicationCore.Entities.YoutubeVideo", "YoutubeVideo")
                        .WithMany("Statistics")
                        .HasForeignKey("YoutubeVideoID");

                    b.Navigation("YoutubeVideo");
                });

            modelBuilder.Entity("ApplicationCore.Entities.UpReqYoutubeVideo", b =>
                {
                    b.HasOne("ApplicationCore.Entities.YoutubeVideo", null)
                        .WithOne()
                        .HasForeignKey("ApplicationCore.Entities.UpReqYoutubeVideo", "ID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationCore.Entities.YoutubeVideo", b =>
                {
                    b.Navigation("Statistics");
                });
#pragma warning restore 612, 618
        }
    }
}
