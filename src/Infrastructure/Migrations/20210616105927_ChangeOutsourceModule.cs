using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeOutsourceModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoInfo");

            migrationBuilder.DropTable(
                name: "YoutubeVideoStatistics");

            migrationBuilder.DropTable(
                name: "UploadReqYoutubeVideo");

            migrationBuilder.DropTable(
                name: "YoutubeVideo");

            migrationBuilder.CreateTable(
                name: "OutsourceVideo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlatFormKinds = table.Column<int>(type: "int", nullable: false),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    TagsData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeakJP = table.Column<bool>(type: "bit", nullable: false),
                    SpeakEnglish = table.Column<bool>(type: "bit", nullable: false),
                    SpeakOther = table.Column<bool>(type: "bit", nullable: false),
                    IsTranslation = table.Column<bool>(type: "bit", nullable: false),
                    TranslationJP = table.Column<bool>(type: "bit", nullable: false),
                    TranslationEnglish = table.Column<bool>(type: "bit", nullable: false),
                    TranslationOther = table.Column<bool>(type: "bit", nullable: false),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutsourceVideo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UploadReqOutsourceVideo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlatFormKinds = table.Column<int>(type: "int", nullable: false),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadReqOutsourceVideo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OutsourceVideoStatistics",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    LikeCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CommentCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    OutsourceVideoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GetDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpReqOutsourceVideoID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutsourceVideoStatistics", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OutsourceVideoStatistics_OutsourceVideo_OutsourceVideoId",
                        column: x => x.OutsourceVideoId,
                        principalTable: "OutsourceVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutsourceVideoStatistics_UploadReqOutsourceVideo_UpReqOutsourceVideoID",
                        column: x => x.UpReqOutsourceVideoID,
                        principalTable: "UploadReqOutsourceVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideoStatistics_OutsourceVideoId",
                table: "OutsourceVideoStatistics",
                column: "OutsourceVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideoStatistics_UpReqOutsourceVideoID",
                table: "OutsourceVideoStatistics",
                column: "UpReqOutsourceVideoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutsourceVideoStatistics");

            migrationBuilder.DropTable(
                name: "OutsourceVideo");

            migrationBuilder.DropTable(
                name: "UploadReqOutsourceVideo");

            migrationBuilder.CreateTable(
                name: "UploadReqYoutubeVideo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChanelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThumbnailLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadReqYoutubeVideo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VideoInfo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeVideo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChanelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThumbnailLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeVideo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeVideoStatistics",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    GetDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LikeCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UpReqYoutubeVideoID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VideoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    YoutubeVideoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeVideoStatistics", x => x.ID);
                    table.ForeignKey(
                        name: "FK_YoutubeVideoStatistics_UploadReqYoutubeVideo_UpReqYoutubeVideoID",
                        column: x => x.UpReqYoutubeVideoID,
                        principalTable: "UploadReqYoutubeVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YoutubeVideoStatistics_YoutubeVideo_YoutubeVideoId",
                        column: x => x.YoutubeVideoId,
                        principalTable: "YoutubeVideo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideoStatistics_UpReqYoutubeVideoID",
                table: "YoutubeVideoStatistics",
                column: "UpReqYoutubeVideoID");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideoStatistics_YoutubeVideoId",
                table: "YoutubeVideoStatistics",
                column: "YoutubeVideoId");
        }
    }
}
