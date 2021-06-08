using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeUpreqVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YoutubeVideoStatistics_YoutubeVideo_YoutubeVideoID",
                table: "YoutubeVideoStatistics");

            migrationBuilder.RenameColumn(
                name: "YoutubeVideoID",
                table: "YoutubeVideoStatistics",
                newName: "YoutubeVideoId");

            migrationBuilder.RenameIndex(
                name: "IX_YoutubeVideoStatistics_YoutubeVideoID",
                table: "YoutubeVideoStatistics",
                newName: "IX_YoutubeVideoStatistics_YoutubeVideoId");

            migrationBuilder.RenameColumn(
                name: "VideoLink",
                table: "YoutubeVideo",
                newName: "VideoId");

            migrationBuilder.AddColumn<string>(
                name: "VideoId",
                table: "YoutubeVideoStatistics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistDateTime",
                table: "YoutubeVideo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailLink",
                table: "YoutubeVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailLink",
                table: "UploadReqYoutubeVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoId",
                table: "UploadReqYoutubeVideo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_YoutubeVideoStatistics_YoutubeVideo_YoutubeVideoId",
                table: "YoutubeVideoStatistics",
                column: "YoutubeVideoId",
                principalTable: "YoutubeVideo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YoutubeVideoStatistics_YoutubeVideo_YoutubeVideoId",
                table: "YoutubeVideoStatistics");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "YoutubeVideoStatistics");

            migrationBuilder.DropColumn(
                name: "RegistDateTime",
                table: "YoutubeVideo");

            migrationBuilder.DropColumn(
                name: "ThumbnailLink",
                table: "YoutubeVideo");

            migrationBuilder.DropColumn(
                name: "ThumbnailLink",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "UploadReqYoutubeVideo");

            migrationBuilder.RenameColumn(
                name: "YoutubeVideoId",
                table: "YoutubeVideoStatistics",
                newName: "YoutubeVideoID");

            migrationBuilder.RenameIndex(
                name: "IX_YoutubeVideoStatistics_YoutubeVideoId",
                table: "YoutubeVideoStatistics",
                newName: "IX_YoutubeVideoStatistics_YoutubeVideoID");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "YoutubeVideo",
                newName: "VideoLink");

            migrationBuilder.AddForeignKey(
                name: "FK_YoutubeVideoStatistics_YoutubeVideo_YoutubeVideoID",
                table: "YoutubeVideoStatistics",
                column: "YoutubeVideoID",
                principalTable: "YoutubeVideo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
