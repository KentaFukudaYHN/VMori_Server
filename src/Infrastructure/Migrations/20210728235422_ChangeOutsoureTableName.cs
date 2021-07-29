using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeOutsoureTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComment_OutsourceVideo_VideoId",
                table: "VideoComment");

            migrationBuilder.DropTable(
                name: "OutsourceVideo");

            migrationBuilder.DropTable(
                name: "OutsourceVideoChannel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoHistorys",
                table: "VideoHistorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadReqOutsourceVideo",
                table: "UploadReqOutsourceVideo");

            migrationBuilder.RenameTable(
                name: "VideoHistorys",
                newName: "VideoHistory");

            migrationBuilder.RenameTable(
                name: "UploadReqOutsourceVideo",
                newName: "UploadReqVideo");

            migrationBuilder.RenameIndex(
                name: "IX_VideoHistorys_IpAddress_VideoId",
                table: "VideoHistory",
                newName: "IX_VideoHistory_IpAddress_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoHistory",
                table: "VideoHistory",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadReqVideo",
                table: "UploadReqVideo",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    CommentCount = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    SubscriverCount = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    VideoCount = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    GetRegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlatFormKinds = table.Column<int>(type: "int", nullable: false),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    VMoriViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CommentCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    VMoriLikeCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    LikeCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    TagsData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeakJP = table.Column<bool>(type: "bit", nullable: false),
                    SpeakEnglish = table.Column<bool>(type: "bit", nullable: false),
                    SpeakOther = table.Column<bool>(type: "bit", nullable: false),
                    IsTranslation = table.Column<bool>(type: "bit", nullable: false),
                    TranslationJP = table.Column<bool>(type: "bit", nullable: false),
                    TranslationEnglish = table.Column<bool>(type: "bit", nullable: false),
                    TranslationOther = table.Column<bool>(type: "bit", nullable: false),
                    ChannelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateCount = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.ID);
                    table.UniqueConstraint("AK_Video_VideoId", x => x.VideoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Video_VideoId_ViewCount_CommentCount_LikeCount_PublishDateTime_RegistDateTime",
                table: "Video",
                columns: new[] { "VideoId", "ViewCount", "CommentCount", "LikeCount", "PublishDateTime", "RegistDateTime" });

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComment_Video_VideoId",
                table: "VideoComment",
                column: "VideoId",
                principalTable: "Video",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComment_Video_VideoId",
                table: "VideoComment");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoHistory",
                table: "VideoHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadReqVideo",
                table: "UploadReqVideo");

            migrationBuilder.RenameTable(
                name: "VideoHistory",
                newName: "VideoHistorys");

            migrationBuilder.RenameTable(
                name: "UploadReqVideo",
                newName: "UploadReqOutsourceVideo");

            migrationBuilder.RenameIndex(
                name: "IX_VideoHistory_IpAddress_VideoId",
                table: "VideoHistorys",
                newName: "IX_VideoHistorys_IpAddress_VideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoHistorys",
                table: "VideoHistorys",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadReqOutsourceVideo",
                table: "UploadReqOutsourceVideo",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "OutsourceVideo",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChanelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    IsTranslation = table.Column<bool>(type: "bit", nullable: false),
                    LikeCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PlatFormKinds = table.Column<int>(type: "int", nullable: false),
                    PublishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpeakEnglish = table.Column<bool>(type: "bit", nullable: false),
                    SpeakJP = table.Column<bool>(type: "bit", nullable: false),
                    SpeakOther = table.Column<bool>(type: "bit", nullable: false),
                    TagsData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranslationEnglish = table.Column<bool>(type: "bit", nullable: false),
                    TranslationJP = table.Column<bool>(type: "bit", nullable: false),
                    TranslationOther = table.Column<bool>(type: "bit", nullable: false),
                    UpdateCount = table.Column<short>(type: "smallint", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VMoriLikeCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    VMoriViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutsourceVideo", x => x.ID);
                    table.UniqueConstraint("AK_OutsourceVideo_VideoId", x => x.VideoId);
                });

            migrationBuilder.CreateTable(
                name: "OutsourceVideoChannel",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentCount = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetRegistDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubscriverCount = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoCount = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    ViewCount = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutsourceVideoChannel", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideo_VideoId_ViewCount_CommentCount_LikeCount_PublishDateTime_RegistDateTime",
                table: "OutsourceVideo",
                columns: new[] { "VideoId", "ViewCount", "CommentCount", "LikeCount", "PublishDateTime", "RegistDateTime" });

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComment_OutsourceVideo_VideoId",
                table: "VideoComment",
                column: "VideoId",
                principalTable: "OutsourceVideo",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
