﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(VMoriContext))]
    partial class VMoriContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ApplicationCore.Entities.OutsourceVideo", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChanelId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChanelTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<bool>("IsTranslation")
                        .HasColumnType("bit");

                    b.Property<int>("PlatFormKinds")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RegistDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("SpeakEnglish")
                        .HasColumnType("bit");

                    b.Property<bool>("SpeakJP")
                        .HasColumnType("bit");

                    b.Property<bool>("SpeakOther")
                        .HasColumnType("bit");

                    b.Property<string>("TagsData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbnailLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TranslationEnglish")
                        .HasColumnType("bit");

                    b.Property<bool>("TranslationJP")
                        .HasColumnType("bit");

                    b.Property<bool>("TranslationOther")
                        .HasColumnType("bit");

                    b.Property<string>("VideoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("OutsourceVideo");
                });

            modelBuilder.Entity("ApplicationCore.Entities.OutsourceVideoChannel", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChannelId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("CommentCount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PublishAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("SubscriverCount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("VideoCount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal?>("ViewCount")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("ID");

                    b.ToTable("OutsouceVideoChannel");
                });

            modelBuilder.Entity("ApplicationCore.Entities.OutsourceVideoStatistics", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("CommentCount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<DateTime>("GetDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("LikeCount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("OutsourceVideoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UpReqOutsourceVideoID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VideoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ViewCount")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("ID");

                    b.HasIndex("OutsourceVideoId");

                    b.HasIndex("UpReqOutsourceVideoID");

                    b.ToTable("OutsourceVideoStatistics");
                });

            modelBuilder.Entity("ApplicationCore.Entities.UpReqOutsourceVideo", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChanelId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChanelTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlatFormKinds")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ThumbnailLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("UploadReqOutsourceVideo");
                });

            modelBuilder.Entity("ApplicationCore.Entities.OutsourceVideoStatistics", b =>
                {
                    b.HasOne("ApplicationCore.Entities.OutsourceVideo", "OutsourceVideo")
                        .WithMany("Statistics")
                        .HasForeignKey("OutsourceVideoId");

                    b.HasOne("ApplicationCore.Entities.UpReqOutsourceVideo", null)
                        .WithMany("Statistics")
                        .HasForeignKey("UpReqOutsourceVideoID");

                    b.Navigation("OutsourceVideo");
                });

            modelBuilder.Entity("ApplicationCore.Entities.OutsourceVideo", b =>
                {
                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("ApplicationCore.Entities.UpReqOutsourceVideo", b =>
                {
                    b.Navigation("Statistics");
                });
#pragma warning restore 612, 618
        }
    }
}
