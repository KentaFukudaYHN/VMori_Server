using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Change_TB_UploadReqYoutubeVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadReqYoutubeVideo_YoutubeVideo_ID",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.AddColumn<string>(
                name: "UpReqYoutubeVideoID",
                table: "YoutubeVideoStatistics",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChanelId",
                table: "UploadReqYoutubeVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChanelTitle",
                table: "UploadReqYoutubeVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UploadReqYoutubeVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDateTime",
                table: "UploadReqYoutubeVideo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VideoLink",
                table: "UploadReqYoutubeVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoTitle",
                table: "UploadReqYoutubeVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideoStatistics_UpReqYoutubeVideoID",
                table: "YoutubeVideoStatistics",
                column: "UpReqYoutubeVideoID");

            migrationBuilder.AddForeignKey(
                name: "FK_YoutubeVideoStatistics_UploadReqYoutubeVideo_UpReqYoutubeVideoID",
                table: "YoutubeVideoStatistics",
                column: "UpReqYoutubeVideoID",
                principalTable: "UploadReqYoutubeVideo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YoutubeVideoStatistics_UploadReqYoutubeVideo_UpReqYoutubeVideoID",
                table: "YoutubeVideoStatistics");

            migrationBuilder.DropIndex(
                name: "IX_YoutubeVideoStatistics_UpReqYoutubeVideoID",
                table: "YoutubeVideoStatistics");

            migrationBuilder.DropColumn(
                name: "UpReqYoutubeVideoID",
                table: "YoutubeVideoStatistics");

            migrationBuilder.DropColumn(
                name: "ChanelId",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.DropColumn(
                name: "ChanelTitle",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.DropColumn(
                name: "PublishDateTime",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.DropColumn(
                name: "VideoLink",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.DropColumn(
                name: "VideoTitle",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadReqYoutubeVideo_YoutubeVideo_ID",
                table: "UploadReqYoutubeVideo",
                column: "ID",
                principalTable: "YoutubeVideo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
