using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddVideoCommentChangeRelattion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComment_OutsourceVideo_VideoId",
                table: "VideoComment");

            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "OutsourceVideo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_OutsourceVideo_VideoId",
                table: "OutsourceVideo",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComment_OutsourceVideo_VideoId",
                table: "VideoComment",
                column: "VideoId",
                principalTable: "OutsourceVideo",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComment_OutsourceVideo_VideoId",
                table: "VideoComment");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_OutsourceVideo_VideoId",
                table: "OutsourceVideo");

            migrationBuilder.AlterColumn<string>(
                name: "VideoId",
                table: "OutsourceVideo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComment_OutsourceVideo_VideoId",
                table: "VideoComment",
                column: "VideoId",
                principalTable: "OutsourceVideo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
