using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addVMoriViewCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutsourceVideo_VideoId_ViewCount_CommentCount_LikeCount",
                table: "OutsourceVideo");

            migrationBuilder.AddColumn<decimal>(
                name: "VMoriLikeCount",
                table: "OutsourceVideo",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VMoriViewCount",
                table: "OutsourceVideo",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideo_VideoId_ViewCount_CommentCount_LikeCount_PublishDateTime_RegistDateTime",
                table: "OutsourceVideo",
                columns: new[] { "VideoId", "ViewCount", "CommentCount", "LikeCount", "PublishDateTime", "RegistDateTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutsourceVideo_VideoId_ViewCount_CommentCount_LikeCount_PublishDateTime_RegistDateTime",
                table: "OutsourceVideo");

            migrationBuilder.DropColumn(
                name: "VMoriLikeCount",
                table: "OutsourceVideo");

            migrationBuilder.DropColumn(
                name: "VMoriViewCount",
                table: "OutsourceVideo");

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceVideo_VideoId_ViewCount_CommentCount_LikeCount",
                table: "OutsourceVideo",
                columns: new[] { "VideoId", "ViewCount", "CommentCount", "LikeCount" });
        }
    }
}
